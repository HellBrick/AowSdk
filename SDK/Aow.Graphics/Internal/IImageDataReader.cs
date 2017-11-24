using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Aow2.Graphics.Internal
{
	internal interface IImageDataReader
	{
		BitmapSource ReadData( AowImage image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat );
	}
}
