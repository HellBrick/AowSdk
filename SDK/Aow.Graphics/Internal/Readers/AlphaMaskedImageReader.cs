using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Graphics;
using Utils.IO;

namespace Aow2.Graphics.Internal.Readers
{
	class AlphaMaskedImageReader : ImageDataReader<AlphaMaskedImage>
	{
		private static readonly PixelFormat _outPixelFormat = PixelFormats.Bgra32;
		private static readonly PixelFormat _inPixelFormat = PixelFormats.Bgr565;

		protected override BitmapSource ReadData( AlphaMaskedImage image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			int stride = BitmapHelper.Stride( image.Width, _inPixelFormat );

			byte[,] alphaMask = new byte[ image.Height, image.Width ];
			byte[] imageBuffer = new byte[ image.Height * stride ];

			ReadImageAndAlpha( image, stream, length, stride, alphaMask, imageBuffer );
			BitmapSource internalBitmap = CreateInternalBitmap( image, palette, stride, imageBuffer );
			ColorConvertedBitmap convertedBitmap = ConvertToAlphaChanneled( internalBitmap );
			WriteableBitmap mergedBitmap = MergeAlphaMask( image, convertedBitmap, alphaMask );

			return mergedBitmap;
		}

		private BitmapSource CreateInternalBitmap( AlphaMaskedImage image, BitmapPalette palette, int stride, byte[] imageBuffer ) => BitmapSource.Create( image.Width, image.Height, 96, 96, _inPixelFormat, palette, imageBuffer, stride );

		private void ReadImageAndAlpha( AlphaMaskedImage image, Stream stream, int length, int stride, byte[,] alphaMask, byte[] imageBuffer )
		{
			int bytesPerPixel = BitmapHelper.BytesPerPixel( _inPixelFormat.BitsPerPixel );
			long stopPosition = stream.Position + length;
			BinaryReader reader = new BinaryReader( stream );

			MemoryStream imageStream = new MemoryStream( imageBuffer );
			imageStream.Position = image.DataOffsetY * stride + image.DataOffsetX * bytesPerPixel;

			for ( int y = 0; stream.Position < stopPosition; y++ )   //	fully transparent lines in the end are omitted, so we can't use naive y < Height condition
			{
				for ( int x = 0; x < image.DataWidth; x++ )
				{
					alphaMask[ y, x ] = reader.ReadByte();
				}

				long lineStart = imageStream.Position;
				stream.CopyBytesTo( imageStream, image.DataWidth * bytesPerPixel );
				imageStream.Position = lineStart + stride;
			}
		}

		private static ColorConvertedBitmap ConvertToAlphaChanneled( BitmapSource internalBitmap )
		{
			ColorContext sourceContext = new ColorContext( _inPixelFormat );
			ColorContext targetContext = new ColorContext( _outPixelFormat );
			ColorConvertedBitmap convertedBitmap = new ColorConvertedBitmap( internalBitmap, sourceContext, targetContext, _outPixelFormat );
			return convertedBitmap;
		}

		private unsafe WriteableBitmap MergeAlphaMask( AlphaMaskedImage image, BitmapSource imageBitmap, byte[,] alphaMask )
		{
			WriteableBitmap mergedBitmap = new WriteableBitmap( imageBitmap );
			mergedBitmap.Lock();

			int bytesPerPixel = BitmapHelper.BytesPerPixel( _outPixelFormat.BitsPerPixel );

			for ( int y = 0; y < image.Height; y++ )
			{
				for ( int x = 0; x < image.Width; x++ )
				{
					IntPtr pixelPtr = mergedBitmap.BackBuffer + y * mergedBitmap.BackBufferStride + x * bytesPerPixel;

					if ( y < image.DataOffsetY || y >= image.DataOffsetY + image.DataHeight || x < image.DataOffsetX || x >= image.DataOffsetX + image.DataWidth )
					{
						*( (int*) pixelPtr ) = 0;  //	if pixel is out of image rectangle, make it with fully transparent
					}
					else
					{
						IntPtr alphaPtr = pixelPtr + 3;  //	alpha byte is the last one
						*( (byte*) alphaPtr ) = alphaMask[ y - image.DataOffsetY, x - image.DataOffsetX ];
					}
				}
			}

			mergedBitmap.AddDirtyRect( new Int32Rect( image.DataOffsetX, image.DataOffsetY, image.DataWidth, image.DataHeight ) );
			mergedBitmap.Unlock();
			return mergedBitmap;
		}
	}
}
