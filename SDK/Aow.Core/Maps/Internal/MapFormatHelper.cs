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

		public static AowMap ReadMapFromFile( string filename )
		{
			using ( FileStream inputStream = new FileStream( filename, FileMode.Open, FileAccess.Read ) )
			{
				(int modId, int mapClassId, int headerLength) = ReadPreHeader( inputStream );

				//	Header stream
				inputStream.Position += headerLength;

				//	CFS signature
				ValidateSignature( new BinaryReader( inputStream ), _signatureCFS );

				//	Data stream
				MemoryStream dataStream = new MemoryStream();
				using ( ZlibStream zlib = new ZlibStream( inputStream, CompressionMode.Decompress, leaveOpen: true ) )
				{
					zlib.CopyTo( dataStream );
				}

				using ( dataStream )
				{
					dataStream.Position = 0;
					AowMap map = _mapSerializer.Deserialize( dataStream );
					map.ModID = modId;
					map.ClassID = mapClassId;
					return map;
				}
			}
		}

		public static MapHeader ReadHeaderFromFile( string filename )
		{
			using ( FileStream inputStream = new FileStream( filename, FileMode.Open, FileAccess.Read ) )
			{
				(int modId, int mapClassId, int headerLength) = ReadPreHeader( inputStream );

				//	Header stream
				MemoryStream headerStream = new MemoryStream();
				inputStream.CopyBytesTo( headerStream, headerLength );

				using ( headerStream )
				{
					headerStream.Position = 0;
					return _headerSerializer.Deserialize( headerStream ) as MapHeader;
				}
			}
		}

		private static (int modId, int mapClassId, int headerLength) ReadPreHeader( Stream inputStream )
		{
			BinaryReader reader = new BinaryReader( inputStream );

			//	Signature1
			int signature1 = reader.ReadInt32();
			ValidateSignature( _signature1, signature1, inputStream.Position - sizeof( int ) );

			//	Signature 2
			int signature2 = reader.ReadInt32();

			//	Header length, IDs
			inputStream.Position += 4; //	always 0
			int headerLength = reader.ReadInt32();
			int modID = reader.ReadInt32();
			int mapClassID = reader.ReadInt32();

			return (modID, mapClassID, headerLength);
		}

		public static void WriteToStream( AowMap map, Stream outStream )
		{
			int modID = map.ModID;
			int mapClassID = map.ClassID;

			using ( MemoryStream dataStream = new MemoryStream() )
			using ( MemoryStream headerStream = new MemoryStream() )
			{
				_headerSerializer.Serialize( headerStream, map.PreviewHeader );
				_mapSerializer.Serialize( dataStream, map );

				BinaryWriter writer = new BinaryWriter( outStream );
				writer.Write( _signature1 );
				writer.Write( _signature2 );
				writer.Write( (int) 0 );

				writer.Write( (int) headerStream.Length );
				writer.Write( modID );
				writer.Write( mapClassID );

				headerStream.Position = 0;
				headerStream.CopyTo( outStream );

				writer.Write( _signatureCFS );

				using ( ZlibStream zlib = new ZlibStream( outStream, CompressionMode.Compress ) )
				{
					dataStream.Position = 0;
					dataStream.CopyTo( zlib );
				}
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
