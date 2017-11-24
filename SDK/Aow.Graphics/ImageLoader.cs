using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Aow2.Graphics.Internal;

namespace Aow2.Graphics
{
	public static class ImageLoader
	{
		public static BitmapSource LoadBitmap( AowImage image, ImageLibrary library )
		{
			IImageDataReader reader = ImageDataReaderProvider.ReaderFor( image );

			library.DataStream.Position = image.DataOffset;
			BitmapPalette palette = library.Palettes.FirstOrDefault( p => p.Index == image.PaletteIndex );
			PixelFormat? pixelFormat = null;

			if ( palette == null )
			{
				switch ( image.PaletteIndex )
				{
					case 1431343888:
						pixelFormat = PixelFormats.Bgr555;
						break;

					case 1448121104:
						pixelFormat = PixelFormats.Bgr565;
						break;
				}
			}

			return reader.ReadData( image, library.DataStream, image.DataLength, palette, pixelFormat );
		}
	}
}
