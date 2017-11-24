using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Collections;
using Utils.Graphics;

namespace Aow2.Graphics.Internal.Readers
{
	class SpriteReader : ImageDataReader<Sprite>
	{
		protected override BitmapSource ReadData( Sprite image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			UInt16StrideArray pixels = new UInt16StrideArray( image.Width, image.Height );
			pixels.OutBuffer.Fill( (ushort) image.KeyColorIndex );

			BinaryReader reader = new BinaryReader( stream );

			for ( int i = image.DataOffsetY; i < image.DataOffsetY + image.DataHeight; i++ )
			{
				for ( int j = image.DataOffsetX; j < image.DataOffsetX + image.DataWidth; j++ )
				{
					pixels[ i, j ] = reader.ReadUInt16();
				}
			}

			return pixels.CreateBitmap( pixelFormat.Value );
		}
	}
}
