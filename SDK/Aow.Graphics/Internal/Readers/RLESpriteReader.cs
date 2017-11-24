using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Graphics;

namespace Aow2.Graphics.Internal.Readers
{
	class RLESpriteReader : ImageDataReader<RleSprite>
	{
		private static readonly PixelFormat _pixelFormat = PixelFormats.Bgr565;

		protected override BitmapSource ReadData( RleSprite image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			ushort alpha = (ushort) image.KeyColorIndex;
			int stride = BitmapHelper.Stride( image.Width, _pixelFormat );
			int bytesPerPixel = BitmapHelper.BytesPerPixel( _pixelFormat.BitsPerPixel );

			byte[] fullData = new byte[ image.Height * stride ];
			FillDataWithAlpha( fullData, alpha );

			MemoryStream fullDataStream = new MemoryStream( fullData );
			fullDataStream.Position = image.DataOffsetY * stride + image.DataOffsetX * bytesPerPixel;
			BinaryWriter dataWriter = new BinaryWriter( fullDataStream );

			BinaryReader reader = new BinaryReader( stream );
			long stopPosition = stream.Position + length;

			while ( stream.Position < stopPosition )
			{
				long targetLineStart = fullDataStream.Position; //	remember where the the line starts to simplify calculating start of the next line

				int lineLength = reader.ReadInt32() - sizeof( int );  //	this is length of real data, padding is not included
				byte[] currentLineEncoded = reader.ReadBytes( lineLength );

				if ( ( lineLength % 4 ) != 0 )   //	if line is not 4-byte-aligned, it's padded by alpha pixel, so we need to skip it
					reader.ReadUInt16();

				BinaryReader currentLineReader = new BinaryReader( new MemoryStream( currentLineEncoded ) );
				while ( currentLineReader.BaseStream.Position < currentLineReader.BaseStream.Length )
				{
					ushort pixel = currentLineReader.ReadUInt16();
					int repeatCount = 1;

					if ( pixel == alpha )
						repeatCount = currentLineReader.ReadUInt16() / sizeof( ushort );

					for ( int i = 0; i < repeatCount; i++ )
						dataWriter.Write( pixel );
				}

				fullDataStream.Position = targetLineStart + stride;
			}

			return BitmapSource.Create( image.Width, image.Height, 96, 96, _pixelFormat, palette, fullData, stride );
		}

		unsafe private static void FillDataWithAlpha( byte[] fullData, ushort alpha )
		{
			fixed ( byte* startPtr = &fullData[ 0 ] )
			{
				for ( byte* ptr = startPtr; ptr < startPtr + fullData.Length; ptr += sizeof( ushort ) )
				{
					ushort* int16Ptr = (ushort*) ptr;
					*int16Ptr = alpha;
				}
			}
		}
	}
}
