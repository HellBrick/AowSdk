﻿using System;
using System.IO;
using System.Linq;
using Aow2.Serialization;
using Ionic.Zlib;
using Utils.IO;

namespace Aow2.Maps.Internal
{
	internal static class MapFormatHelper
	{
		private const int _signature1 = 0x00000018;
		private static readonly byte[] _signatureCFS = { 0x43, 0x46, 0x53, 0x00, 0x02 };

		private static AowSerializer<AowMap> _mapSerializer = new AowSerializer<AowMap>( hasRootWrapper: true );
		private static AowSerializer<MapHeaderBase> _headerSerializer = new AowSerializer<MapHeaderBase>( hasRootWrapper: true );

		public static AowMap ReadMapFromStream( Stream inputStream )
		{
			(int modId, int mapClassId, int hmSignature, MemoryStream dataStream) = ReadPreHeaderAndDecompressDataStream( inputStream );
			AowMap map = _mapSerializer.Deserialize( dataStream );
			map.ModID = modId;
			map.ClassID = mapClassId;
			map.HmSignature = hmSignature;
			return map;
		}

		public static MapHeader ReadHeaderFromStream( Stream inputStream )
		{
			int headerLength = ReadPreHeader( inputStream ).headerLength;

			using ( MemoryStream headerStream = new MemoryStream() )
			{
				inputStream.CopyBytesTo( headerStream, headerLength );
				headerStream.Position = 0;
				return _headerSerializer.Deserialize( headerStream ) as MapHeader;
			}
		}

		internal static (int modId, int mapClassId, int hmSignature, MemoryStream dataStream) ReadPreHeaderAndDecompressDataStream( Stream inputStream )
		{
			(int modId, int mapClassId, int hmSignature, int headerLength) = ReadPreHeader( inputStream );

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

			dataStream.Position = 0;
			return (modId, mapClassId, hmSignature, dataStream);
		}

		private static (int modId, int mapClassId, int hmSignature, int headerLength) ReadPreHeader( Stream inputStream )
		{
			BinaryReader reader = new BinaryReader( inputStream );

			//	Signature1
			int signature1 = reader.ReadInt32();
			ValidateSignature( _signature1, signature1, inputStream.Position - sizeof( int ) );

			//	Signature 2
			int hmSignature = reader.ReadInt32();

			//	Header length, IDs
			inputStream.Position += 4; //	always 0
			int headerLength = reader.ReadInt32();
			int modID = reader.ReadInt32();
			int mapClassID = reader.ReadInt32();

			return (modID, mapClassID, hmSignature, headerLength);
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
				writer.Write( map.HmSignature );
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
