using System.Windows.Media.Imaging;

namespace Aow2.Modding.Internal.Graphics
{
	interface IImageProvider
	{
		BitmapSource GetImage( string imageLibraryPath, int imageIndex );
		BitmapSource GetCroppedImage( string imageLibraryPath, int imageIndex );
	}
}
