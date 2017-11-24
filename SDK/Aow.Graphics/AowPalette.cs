using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Aow2.Serialization;

namespace Aow2.Graphics
{
	public enum PaletteType : uint
	{
		Default = 0x88801B18
	}

	[AowClass( ID = 0x00300101 )]
	public class AowPalette
	{
		public static implicit operator BitmapPalette( AowPalette aowPalette )
		{
			if ( aowPalette != null )
				return new BitmapPalette( aowPalette.Colors );
			else
				return null;
		}

		public List<Color> Colors
		{
			get => _colorData.Colors;
			set => _colorData.Colors = value;
		}

		[Field( 0x13 )] public int Index { get; set; }
		[Field( 0x14 )] private ColorData _colorData = new ColorData();

		private class ColorData : ICustomFormatted
		{
			private const int _defaultPaletteSize = 0x100;
			private const int _defaultPaletteBytePerColor = 4;

			public ColorData()
			{
				Type = PaletteType.Default;
				Colors = new List<Color>();
			}

			public PaletteType Type { get; private set; }
			public List<Color> Colors { get; set; }

			void ICustomFormatted.Serialize( System.IO.Stream outStream ) => throw new NotImplementedException();

			void ICustomFormatted.Deserialize( Stream inStream, long length )
			{
				BinaryReader reader = new BinaryReader( inStream );
				Type = (PaletteType) reader.ReadUInt32();
				Colors = new List<Color>();

				switch ( Type )
				{
					case PaletteType.Default:
						EnsureLength( length, _defaultPaletteSize * _defaultPaletteBytePerColor );
						ReadDefaultPalette( reader );
						break;

					default:
						throw new NotSupportedException( String.Format( "Not supported palette type: {0:x8}", (uint) Type ) );
				}
			}

			bool ICustomFormatted.ShouldBeOmitted() => false;

			private void EnsureLength( long fieldLength, int requiredLength )
			{
				if ( fieldLength < requiredLength ) //	UInt32 - Type field
					throw new ArgumentOutOfRangeException( "Can't read palette: buffer is too small" );
			}

			private ColorData ReadDefaultPalette( BinaryReader reader )
			{
				for ( int i = 0; i < _defaultPaletteSize; i++ )
				{
					byte red = reader.ReadByte();
					byte green = reader.ReadByte();
					byte blue = reader.ReadByte();
					reader.ReadByte();   //	skip unused 0x00 byte
					byte alpha = 0xFF;

					Colors.Add( Color.FromArgb( alpha, red, green, blue ) );
				}

				return this;
			}
		}
	}
}
