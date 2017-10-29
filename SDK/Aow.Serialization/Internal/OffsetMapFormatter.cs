using System;
using System.Collections.Generic;
using System.IO;

namespace Aow2.Serialization.Internal
{
	internal static class OffsetMapFormatter
	{
		public static List<IReadOffsetRecord> ReadNodesFromStream( Stream inStream, long offset, long length )
		{
			List<IReadOffsetRecord> nodes = new List<IReadOffsetRecord>();

			inStream.Position = offset;
			BinaryReader input = new BinaryReader( inStream );

			byte shortOffsetCount = input.ReadByte();
			int longOffsetCount = 0;

			if ( ( shortOffsetCount & 0x80 ) != 0 )
			{
				shortOffsetCount = shortOffsetCount -= 0x80;
				longOffsetCount = input.ReadInt32();
			}

			for ( int i = 0; i < shortOffsetCount; ++i )
			{
				OffsetRecord newOffset = new OffsetRecord
				{
					ID = input.ReadByte(),
					RelativeOffset = input.ReadByte()
				};
				nodes.Add( newOffset );
			}

			for ( int i = 0; i < longOffsetCount; ++i )
			{
				OffsetRecord newOffset = new OffsetRecord
				{
					ID = input.ReadInt32(),
					RelativeOffset = input.ReadInt32()
				};
				nodes.Add( newOffset );
			}

			long dataAbsoluteOffset = input.BaseStream.Position;
			long dataRelativeOffset = dataAbsoluteOffset - offset;

			for ( int i = 0; i < nodes.Count; i++ )
			{
				IReadOffsetRecord current = nodes[ i ];
				current.AbsoluteOffset = current.RelativeOffset + dataAbsoluteOffset;

				long nextRelativeOffset;

				if ( i + 1 < nodes.Count )
					nextRelativeOffset = nodes[ i + 1 ].RelativeOffset;
				else
					nextRelativeOffset = length - dataRelativeOffset;

				current.Length = nextRelativeOffset - current.RelativeOffset;
			}

			return nodes;
		}

		public static void WriteNodesToStream( Stream outStream, Stream dataStream, List<FieldLengthInfo> nodes )
		{
			using ( MemoryStream offsetStream = new MemoryStream() )
			{
				//	Offsets

				BinaryWriter writer = new BinaryWriter( offsetStream );

				byte shortOffsetCount = 0;
				int longOffsetCount = 0;
				long offset = 0;

				foreach ( FieldLengthInfo node in nodes )
				{
					if ( offset <= 0xff && node.ID <= 0xff )
					{
						writer.Write( (byte) node.ID );
						writer.Write( (byte) offset );
						++shortOffsetCount;
					}
					else
					{
						writer.Write( (int) node.ID );
						writer.Write( (int) offset );
						++longOffsetCount;
					}

					offset += node.Length;
				}

				writer.Flush();

				//	Offset count
				if ( longOffsetCount == 0 )
				{
					new BinaryWriter( outStream ).Write( shortOffsetCount );
				}
				else
				{
					BinaryWriter outWriter = new BinaryWriter( outStream );
					outWriter.Write( (byte) ( 0x80 + shortOffsetCount ) );
					outWriter.Write( (int) longOffsetCount );
				}

				//	Offsets (to the output stream now)
				offsetStream.Position = 0;
				offsetStream.CopyTo( outStream );

				//	Data
				dataStream.Position = 0;
				dataStream.CopyTo( outStream );
			}
		}
	}

	internal interface IReadOffsetRecord
	{
		int ID { get; set; }
		long RelativeOffset { get; set; }
		long AbsoluteOffset { get; set; }
		long Length { get; set; }
	}

	internal interface IWriteOffsetRecord
	{
		int ID { get; set; }
		byte[] Data { get; set; }
	}

	internal class FieldLengthInfo
	{
		public FieldLengthInfo( int id, long length )
		{
			ID = id;
			Length = length;
		}

		public int ID { get; private set; }
		public long Length { get; private set; }
	}

	internal class OffsetRecord : IReadOffsetRecord, IWriteOffsetRecord
	{
		public int ID { get; set; }
		public long RelativeOffset { get; set; }
		public long AbsoluteOffset { get; set; }
		public long Length { get; set; }
		public byte[] Data { get; set; }
	}
}
