using Aow2.Collections;

namespace Aow2.Graphics
{
	[AowClass]
	internal class PaletteManager
	{
		public PaletteManager() => Palettes = new AowList<AowPalette>();

		[Field( 0x14 )]
		public AowList<AowPalette> Palettes { get; set; }
	}
}
