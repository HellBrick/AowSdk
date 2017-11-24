using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Graphics;
using Utils.IO;

namespace Aow2.Graphics.Internal.Readers
{
	class AlphaMaskReader : ImageDataReader<AlphaMask>
	{
		private static readonly PixelFormat _pixelFormat = PixelFormats.Gray8;

		protected override BitmapSource ReadData( AlphaMask image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			int bytesPerPixel = BitmapHelper.BytesPerPixel( _pixelFormat.BitsPerPixel );
			int stride = BitmapHelper.Stride( image.Width, _pixelFormat );

			byte[] fullData = new byte[ image.Height * stride ];
			MemoryStream dataStream = new MemoryStream( fullData );
			long position = image.DataOffsetY * stride + image.DataOffsetX;   //	upper left corner of inner frame

			for ( int inStreamPosition = 0; inStreamPosition < length; inStreamPosition += image.DataWidth )
			{
				dataStream.Position = position;
				stream.CopyBytesTo( dataStream, image.DataWidth );
				position += stride;
			}

			return BitmapSource.Create( image.Width, image.Height, 96, 96, _pixelFormat, palette, fullData, stride );
		}
	}
}
