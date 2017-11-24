using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aow2.Graphics.Internal
{
	internal abstract class ImageDataReader<T> : IImageDataReader where T : AowImage
	{
		#region IImageDataReader Members

		public BitmapSource ReadData( AowImage image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat ) => ReadData( image as T, stream, length, palette, pixelFormat );

		#endregion

		protected abstract BitmapSource ReadData( T image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat );
	}
}
