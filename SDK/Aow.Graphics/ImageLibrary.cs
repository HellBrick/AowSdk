using System.Collections.Generic;
using System.IO;
using Aow2.Collections;
using Aow2.Serialization;

namespace Aow2.Graphics
{
	[AowClass]
	[ListStartingIndex( 0x32 )]
	public class ImageLibrary : Dictionary<int, AowImage>
	{
		public ImageLibrary() => DataStream = new MemoryStream();

		public static ImageLibrary FromStream( Stream inStream )
		{
			AowSerializer<ImageLibrary> serializer = new AowSerializer<ImageLibrary>();
			ImageLibrary ilb = serializer.Deserialize( inStream );

			ilb.DataStream = new MemoryStream();
			inStream.CopyTo( ilb.DataStream );

			return ilb;
		}

		public static ImageLibrary FromFile( string filename )
		{
			if ( !Path.IsPathRooted( filename ) )
				filename = Path.Combine( Environment.Instance.ImagesFolder, filename );

			try
			{
				using ( FileStream stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					MemoryStream memStream = new MemoryStream();
					stream.CopyTo( memStream );
					return FromStream( memStream );
				}
			}
			catch
			{
				return null;
			}
		}

		public MemoryStream DataStream { get; private set; }

		[Field( 0x0a )] public int u_0a;
		[Field( 0x0b )] public int u_0b;

		[Field( 0x0c )] private PaletteManager _paletteManager = new PaletteManager();
		public AowList<AowPalette> Palettes
		{
			get => _paletteManager.Palettes;
			set => _paletteManager.Palettes = value;
		}
	}
}
