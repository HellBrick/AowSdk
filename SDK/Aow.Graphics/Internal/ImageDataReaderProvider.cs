using System;
using System.Collections.Generic;
using Aow2.Graphics.Internal.Readers;

namespace Aow2.Graphics.Internal
{
	internal static class ImageDataReaderProvider
	{
		private static readonly Dictionary<Type, IImageDataReader> _readers = new Dictionary<Type, IImageDataReader>()
		{
			{ typeof( AlphaMask ), new AlphaMaskReader() },
			{ typeof( AlphaMaskedImage ), new AlphaMaskedImageReader() },
			{ typeof( Picture ), new PictureImageReader() },
			{ typeof( RleSprite ), new RLESpriteReader() },
			{ typeof( RleSprite8 ), new RLESprite8Reader() },
			{ typeof( Sprite ), new SpriteReader() },
			{ typeof( Sprite8 ), new Sprite8Reader() },
		};

		public static IImageDataReader ReaderFor( AowImage image ) => ReaderFor( image.GetType() );

		public static IImageDataReader ReaderFor( Type type )
		{
			IImageDataReader reader;
			if ( !_readers.TryGetValue( type, out reader ) )
				throw new NotSupportedException( String.Format( "Image type {0} is not supported", type ) );

			return reader;
		}
	}
}
