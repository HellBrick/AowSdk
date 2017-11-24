using System;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utils.Graphics
{
	public abstract class StrideArray<T> where T : struct
	{
		private int _linesPerRow;
		private int _lineWidth;

		public StrideArray( int width, int height, int bitsPerPixel )
		{
			SourceWidth = width;
			SourceHeight = height;
			Stride = BitmapHelper.Stride( width, bitsPerPixel );

			_linesPerRow = Stride / SourceWidth;
			_lineWidth = SourceWidth + ( Stride - _linesPerRow * SourceWidth ) / _linesPerRow;

			OutHeight = ( SourceHeight + _linesPerRow - 1 ) / _linesPerRow;   //	AlignUp(SourceHeigth/3)
			OutBuffer = new T[ OutHeight, Stride ];
		}

		public int SourceWidth { get; private set; }
		public int SourceHeight { get; private set; }

		public int Stride { get; private set; }
		public int OutHeight { get; private set; }

		public T[,] OutBuffer { get; private set; }

		public T this[ int y, int x ]
		{
			get
			{
				Point offset = GetStrideOffset( y, x );
				return OutBuffer[ offset.Y, offset.X ];
			}
			set
			{
				Point offset = GetStrideOffset( y, x );
				OutBuffer[ offset.Y, offset.X ] = value;
			}
		}

		private const int _defaultDPI = 96;

		public BitmapSource CreateBitmap( PixelFormat pixelFormat ) => CreateBitmap( pixelFormat, null, _defaultDPI, _defaultDPI );

		public BitmapSource CreateBitmap( PixelFormat pixelFormat, BitmapPalette palette ) => CreateBitmap( pixelFormat, palette, _defaultDPI, _defaultDPI );

		public BitmapSource CreateBitmap( PixelFormat pixelFormat, BitmapPalette palette, int dpiX, int dpiY )
		{
			BitmapSource bitmap = null;

			FixBufferAndExecute( ptr => bitmap = BitmapSource.Create(
				SourceWidth,
				SourceHeight,
				96,
				96,
				pixelFormat,
				palette,
				ptr,
				Stride * SourceHeight,
				Stride ) );

			return bitmap;
		}

		[SecuritySafeCritical]
		protected abstract unsafe void FixBufferAndExecute( Action<IntPtr> action );

		private Point GetStrideOffset( int sourceY, int sourceX )
		{
			if ( _linesPerRow == 1 )
				return new Point() { X = sourceX, Y = sourceY };

			int row = sourceY / _linesPerRow;
			int col = sourceY % _linesPerRow;

			return new Point()
			{
				Y = row,
				X = col * _lineWidth + sourceX
			};
		}

		private struct Point
		{
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}
