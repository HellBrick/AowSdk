using System;
using System.IO;
using System.Linq;
using Aow2.Serialization;
using Ionic.Zlib;
using Utils.IO;

namespace Aow2.Maps.Internal
{
	internal class MapFormatHelper : IDisposable
	{
		private const int _signature1 = 0x00000018;
		private const int _signature2 = 0x00584d48;
		private static readonly byte[] _signatureCFS = { 0x43, 0x46, 0x53, 0x00, 0x02 };

		private static AowSerializer<AowMap> _mapSerializer = new AowSerializer<AowMap>( hasRootWrapper: true );
		private static AowSerializer<MapHeaderBase> _headerSerializer = new AowSerializer<MapHeaderBase>( hasRootWrapper: true );

		public static MapFormatHelper FromFile( string filename ) => FromStream( new FileStream( filename, FileMode.Open, FileAccess.Read ) );

		public static MapFormatHelper FromStream( Stream inputStream )
		{
			MapFormatHelper helper = new MapFormatHelper();
			BinaryReader reader = new BinaryReader( inputStream );

			//	Signature1
			int signature1 = reader.ReadInt32();
			ValidateSignature( _signature1, signature1, inputStream.Position - sizeof( int ) );

			//	Signature 2
			int signature2 = reader.ReadInt32();

			//	Header length, IDs
			inputStream.Position += 4; //	always 0
			int headerLength = reader.ReadInt32();
			helper.ModID = reader.ReadInt32();
			helper.MapClassID = reader.ReadInt32();

			//	Header stream
			helper.HeaderStream = new MemoryStream();
			inputStream.CopyBytesTo( helper.HeaderStream, headerLength );

			//	CFS signature
			ValidateSignature( reader, _signatureCFS );

			//	Data stream
			helper._dataStream = new Lazy<Stream>(
				() =>
				{
					MemoryStream dataStream = new MemoryStream();
					using ( ZlibStream zlib = new ZlibStream( inputStream, CompressionMode.Decompress ) )
					{
						zlib.CopyTo( dataStream );
					}
					return dataStream;
				} );

			return helper;
		}

		public static void WriteToStream( AowMap map, Stream outStream )
		{
			MapFormatHelper helper = new MapFormatHelper();
			helper.ModID = map.ModID;
			helper.MapClassID = map.ClassID;

			helper.HeaderStream = new MemoryStream();
			_headerSerializer.Serialize( helper.HeaderStream, map.PreviewHeader );

			helper._dataStream = new Lazy<Stream>( () => new MemoryStream() );
			_mapSerializer.Serialize( helper.DataStream, map );

			using ( helper )
			{
				helper.PackData( outStream );
			}
		}

		private MapFormatHelper()
		{
		}

		public int ModID { get; private set; }
		public int MapClassID { get; private set; }
		public Stream HeaderStream { get; private set; }

		private Lazy<Stream> _dataStream;
		public Stream DataStream => _dataStream.Value;

		public MapHeader DeserializeHeader()
		{
			HeaderStream.Position = 0;
			return _headerSerializer.Deserialize( HeaderStream ) as MapHeader;
		}

		public AowMap DeserializeMap()
		{
			DataStream.Position = 0;
			AowMap map = _mapSerializer.Deserialize( DataStream );
			map.ModID = ModID;
			map.ClassID = MapClassID;
			return map;
		}

		public void PackData( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			writer.Write( _signature1 );
			writer.Write( _signature2 );
			writer.Write( (int) 0 );

			writer.Write( (int) HeaderStream.Length );
			writer.Write( ModID );
			writer.Write( MapClassID );

			HeaderStream.Position = 0;
			HeaderStream.CopyTo( outStream );

			writer.Write( _signatureCFS );

			using ( ZlibStream zlib = new ZlibStream( outStream, CompressionMode.Compress ) )
			{
				DataStream.Position = 0;
				DataStream.CopyTo( zlib );
			}
		}

		public void Dispose()
		{
			HeaderStream.Dispose();
			DataStream.Dispose();
		}

		private static void ValidateSignature( int expected, int actual, long streamPosition )
		{
			if ( expected != actual )
			{
				throw new InvalidDataException( String.Format( "Invalid file: signature check failed at position {0}{3}expected:\t{1:x8}{3}actual\t:{2:x8}",
					streamPosition,
					expected,
					actual,
					System.Environment.NewLine ) );
			}
		}

		private static void ValidateSignature( BinaryReader reader, byte[] expected )
		{
			long streamPosition = reader.BaseStream.Position;
			byte[] actual = reader.ReadBytes( expected.Length );

			if ( !expected.SequenceEqual( actual ) )
			{
				string expectedString = BitConverter.ToString( expected );
				string actualString = BitConverter.ToString( actual );

				throw new InvalidDataException( String.Format( "Invalid file: signature check failed at position {0}{3}expected:\t{1}{3}actual\t:{2}",
					streamPosition,
					expectedString,
					actualString,
					System.Environment.NewLine ) );
			}
		}
	}
}
