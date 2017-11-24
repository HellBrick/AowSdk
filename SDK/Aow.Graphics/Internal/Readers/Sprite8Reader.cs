using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Graphics;

namespace Aow2.Graphics.Internal.Readers
{
	class Sprite8Reader : ImageDataReader<Sprite8>
	{
		private static readonly PixelFormat _pixelFormat = PixelFormats.Indexed8;

		protected override BitmapSource ReadData( Sprite8 image, Stream stream, int length, BitmapPalette palette, PixelFormat? pixelFormat )
		{
			BinaryReader reader = new BinaryReader( stream );
			byte alpha = image.KeyColorIndex;
			List<Color> colors = palette.Colors.ToList();
			colors[ alpha ] = Color.FromArgb( 0x00, 0xFF, 0xFF, 0xFF );
			palette = new BitmapPalette( colors );

			byte[,] full_data = InitPixelArray( image, alpha );

			unsafe
			{
				fixed ( byte* full_data_ptr = &full_data[ 0, 0 ] )
				{
					SubArray inner = new SubArray( full_data_ptr, full_data.GetLength( 1 ), image.DataOffsetY, image.DataOffsetX );
					ReadInnerData( image, inner, length, reader, alpha );
				}

				return CreateBitmap( image, palette, full_data );
			}
		}

		private unsafe void ReadInnerData( Sprite8 image, SubArray inner, int length, BinaryReader reader, byte alpha )
		{
			bool is_micro = image.DataWidth < 4;   //	it disables striding and sequential alpha-channel mode

			int inner_width;
			if ( is_micro )
			{
				inner_width = image.DataWidth;
			}
			else
			{
				if ( image.DataWidth < 0x20 )                         //	copied from IlbMaker source code
					inner_width = ( 1 + ( image.DataWidth - 1 ) / 4 ) * 4;
				else
					inner_width = ( 1 + ( image.DataWidth - 1 ) / 8 ) * 8;
			}

			int inner_offset = 0;

			int i = 0;
			while ( i < length )
			{
				byte color = ReadNextIndex( reader, ref i );

				if ( !is_micro && ( color == alpha ) ) //	sequential alpha
				{
					int alpha_count = ReadNextIndex( reader, ref i );
					for ( int j = 0; j < alpha_count; j++ )
					{
						WriteInnerByte( inner, inner_width, alpha, ref inner_offset );
					}
				}
				else
				{
					WriteInnerByte( inner, inner_width, color, ref inner_offset );
				}
			}
		}

		private unsafe void WriteInnerByte( SubArray inner, int innerWidth, byte colorIndex, ref int offset )
		{
			int y = offset / innerWidth;
			int x = offset % innerWidth;
			inner[ y, x ] = colorIndex;
			offset++;
		}

		private BitmapSource CreateBitmap( Sprite8 image, BitmapPalette palette, byte[,] full_data )
		{
			unsafe
			{
				fixed ( byte* start = &full_data[ 0, 0 ] )
				{
					int stride = BitmapHelper.Stride( image.Width, _pixelFormat.BitsPerPixel );
					return BitmapSource.Create( image.Width, image.Height, 96, 96, _pixelFormat, palette, new IntPtr( start ), stride * image.Height, stride );
				}
			}
		}

		private byte[,] InitPixelArray( Sprite8 image, byte alpha )
		{
			int stride = BitmapHelper.Stride( image.Width, _pixelFormat.BitsPerPixel );
			byte[,] arr = new byte[ image.Height, stride ];
			for ( int i = 0; i < image.Height; i++ )
			{
				for ( int j = 0; j < stride; j++ )
				{
					arr[ i, j ] = alpha;
				}
			}
			return arr;
		}

		private byte ReadNextIndex( BinaryReader reader, ref int index )
		{
			index++;
			return reader.ReadByte();
		}
	}
}
