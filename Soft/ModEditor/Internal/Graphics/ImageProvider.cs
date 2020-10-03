using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Aow2.Graphics;
using Utils.Graphics;

namespace Aow2.Modding.Internal.Graphics
{
	class ImageProvider: IImageProvider
	{
		private ConcurrentDictionary<string, ImageLibrary> _ilbCache = new ConcurrentDictionary<string, ImageLibrary>();

		#region IImageProvider Members

		public BitmapSource GetImage( string imageLibraryPath, int imageIndex )
		{
			ImageLibrary ilb = GetImageLibrary( imageLibraryPath );
			AowImage imageData = ilb[imageIndex];
			return ImageLoader.LoadBitmap( imageData, ilb );
		}

		public BitmapSource GetCroppedImage( string imageLibraryPath, int imageIndex )
		{
			AowImage imageData = GetImageData( imageLibraryPath, imageIndex );
			if ( imageData == null )
				return null;

			BitmapSource image = GetImage( imageLibraryPath, imageIndex );
			return Crop( image, imageData );
		}

		#endregion

		private static string IlbFullPath( string imageLibraryPath ) => Path.Combine( Environment.Instance.ImagesFolder, imageLibraryPath );

		private ImageLibrary GetImageLibrary( string imageLibraryPath ) => _ilbCache.GetOrAdd( imageLibraryPath, path => ImageLibrary.FromFile( IlbFullPath( imageLibraryPath ) ) );

		public AowImage GetImageData( string imageLibraryPath, int imageIndex )
		{
			ImageLibrary ilb = GetImageLibrary( imageLibraryPath );
			return ilb != null ? ilb[imageIndex] : null;
		}

		private BitmapSource Crop( BitmapSource image, AowImage imageData )
		{
			OffsetImage offsetData = imageData as OffsetImage;
			if ( offsetData != null )
			{
				Int32Rect rect = new Int32Rect( offsetData.DataOffsetX, offsetData.DataOffsetY, offsetData.DataWidth, offsetData.DataHeight );
				image = new CroppedBitmap( image, rect );
			}

			if ( image.Format == PixelFormats.Bgr565 || image.Format == PixelFormats.Bgr555 )
			{
				image = ConvertToAlpha( image, imageData );
			}

			return image;
		}

		private static BitmapSource ConvertToAlpha( BitmapSource image, AowImage imageData )
		{
			unsafe
			{
				int sourceStride = BitmapHelper.Stride( image.PixelWidth, image.Format );

				//	array has twice more rows than needed, but otherwise CopyPixels fails for some reason
				ushort[,] sourceDataRaw = new ushort[image.PixelHeight * sizeof( ushort ), sourceStride / sizeof( ushort )];
				fixed ( ushort* dataPtr = &sourceDataRaw[0, 0] )
				{
					image.CopyPixels( Int32Rect.Empty, new IntPtr( dataPtr ), sourceDataRaw.Length, sourceStride );
				}

				ushort alphaCode = (ushort) ( imageData as KeyColorImage<int> ).KeyColorIndex;
				List<IntPoint> alphaIndexes = new List<IntPoint>();

				for ( int i = 0; i < sourceDataRaw.GetLength( 0 ); i++ )
				{
					for ( int j = 0; j < sourceDataRaw.GetLength( 1 ); j++ )
						if ( sourceDataRaw[i, j] == alphaCode )
							alphaIndexes.Add( new IntPoint { X = i, Y = j } );
				}

				ColorContext sourceContext = new ColorContext( image.Format );
				ColorContext targetContext = new ColorContext( PixelFormats.Bgra32 );
				ColorConvertedBitmap converted = new ColorConvertedBitmap( image, sourceContext, targetContext, PixelFormats.Bgra32 );
				int targetStride = BitmapHelper.Stride( converted.PixelWidth, converted.Format );

				WriteableBitmap writeable = new WriteableBitmap( converted );
				writeable.Lock();

				IntPtr backBufferPtr = writeable.BackBuffer;
				foreach ( IntPoint p in alphaIndexes )
				{
					IntPtr pixelPtr = backBufferPtr + targetStride * p.X + 4 * p.Y;
					*( (int*) pixelPtr ) = 0;
				}

				writeable.AddDirtyRect( new Int32Rect( 0, 0, writeable.PixelWidth, writeable.PixelHeight ) );
				writeable.Unlock();

				return writeable;
			}
		}

		private struct IntPoint
		{
			public int X { get; set; }
			public int Y { get; set; }

			public override string ToString() => String.Concat( "{", X, ", ", Y, "}" );
		}
	}
}
