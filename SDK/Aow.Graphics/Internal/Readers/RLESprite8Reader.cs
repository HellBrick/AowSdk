using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Graphics;

namespace Aow2.Graphics.Internal.Readers
{
	class RLESprite8Reader : ImageDataReader<RleSprite8>
	{
		PixelFormat _pixelFormat = PixelFormats.Indexed8;

		protected override BitmapSource ReadData( RleSprite8 image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			BinaryReader reader = new BinaryReader( stream );
			byte alpha = (byte) image.KeyColorIndex;

			List<Color> colors = palette.Colors.ToList();
			colors[ alpha ] = Color.FromArgb( 0x00, 0xFF, 0xFF, 0xFF );
			palette = new BitmapPalette( colors );

			int stride = BitmapHelper.Stride( image.Width, _pixelFormat );
			int bytesPerPixel = BitmapHelper.BytesPerPixel( _pixelFormat.BitsPerPixel );
			byte[] rawPixels = new byte[ image.Height * stride ];
			FillDataWithAlpha( rawPixels, alpha );

			MemoryStream pixelStream = new MemoryStream( rawPixels );
			pixelStream.Position = image.DataOffsetY * stride + image.DataOffsetX * bytesPerPixel;

			long stopPosition = stream.Position + length;
			while ( stream.Position < stopPosition )
			{
				long lastLinePosition = pixelStream.Position;

				int lineLength = reader.ReadInt32() - sizeof( int );
				byte[] currentLine = reader.ReadBytes( lineLength );
				BinaryReader lineReader = new BinaryReader( new MemoryStream( currentLine ) );

				while ( lineReader.BaseStream.Position < currentLine.LongLength )
				{
					byte pixel = lineReader.ReadByte();
					int repeatCount = 1;
					if ( pixel == alpha )
						repeatCount = lineReader.ReadByte();

					for ( int i = 0; i < repeatCount; i++ )
						pixelStream.WriteByte( pixel );
				}

				pixelStream.Position = lastLinePosition + stride;
			}

			return BitmapSource.Create( image.Width, image.Height, 96, 96, _pixelFormat, palette, rawPixels, stride );
		}
		unsafe private static void FillDataWithAlpha( byte[] fullData, byte alpha )
		{
			fixed ( byte* startPtr = &fullData[ 0 ] )
			{
				for ( byte* ptr = startPtr; ptr < startPtr + fullData.Length; ptr += sizeof( byte ) )
				{
					byte* bytePtr = (byte*) ptr;
					*bytePtr = alpha;
				}
			}
		}
	}
}
