using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Graphics;
using Utils.IO;

namespace Aow2.Graphics.Internal.Readers
{
	class PictureImageReader : ImageDataReader<Picture>
	{
		private static readonly PixelFormat _pixelFormat = PixelFormats.Bgr565;

		protected override BitmapSource ReadData( Picture image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			int bytesPerPixel = _pixelFormat.BitsPerPixel / 8;
			int stride = BitmapHelper.Stride( image.Width, _pixelFormat.BitsPerPixel );
			int paddingBytes = stride - image.Width * bytesPerPixel;

			MemoryStream stridedStream = new MemoryStream();
			for ( int pos = 0; pos < length; pos += image.Width * bytesPerPixel )
			{
				stream.CopyBytesTo( stridedStream, image.Width * bytesPerPixel );
				stridedStream.Position += paddingBytes;
			}

			byte[] bytes = stridedStream.ToArray();
			return BitmapSource.Create( image.Width, image.Height, 96, 96, _pixelFormat, palette, bytes, stride );
		}
	}
}
