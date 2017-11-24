using System.Windows.Media;
using Utils.Math;

namespace Utils.Graphics
{
	public static class BitmapHelper
	{
		public static int Stride( int width, int bitsPerPixel )
		{
			int bytes_pp = BytesPerPixel( bitsPerPixel );
			return MathHelper.AlignUp( width * bytes_pp, 4 );
		}

		public static int Stride( int width, PixelFormat pixelFormat ) => Stride( width, pixelFormat.BitsPerPixel );

		public static int BytesPerPixel( int bitsPerPixel ) => ( bitsPerPixel + 7 ) / 8;
	}
}
