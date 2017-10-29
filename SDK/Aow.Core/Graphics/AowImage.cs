namespace Aow2.Graphics
{
	[AowClass]
	public abstract class AowImage
	{
		public string Title
		{
			get => _filename;
			set => _filename = value;
		}

		public string Description
		{
			get => _description;
			set => _description = value;
		}

		[Field( 0x0b )] public int Width { get; set; }
		[Field( 0x0c )] public int Height { get; set; }

		[Field( 0x12 )] public int ExtendedWidth { get; set; }
		[Field( 0x13 )] public int ExtendedHeight { get; set; }

		[Field( 0x16 )] public int HotspotX { get; set; }
		[Field( 0x17 )] public int HotspotY { get; set; }

		[Field( 0x0d )] public int OffsetX { get; set; }
		[Field( 0x0e )] public int OffsetY { get; set; }

		[Field( 0x10 )] public LoadMode LoadMode { get; set; }

		[Field( 0x0f )] public int ImageFileIndex { get; set; }

		[Field( 0x11 )] public int DataLength { get; set; }
		[Field( 0x14 )] public int DataOffset { get; set; }

		[Field( 0x32 )] public int PaletteIndex { get; set; }

		[Field( 0x0a )] protected ShortPascalString _filename = new ShortPascalString();
		[Field( 0x18 )] protected ShortPascalString _description = new ShortPascalString();

		[Field( 0x08 )] public AowImage NextLayer { get; set; }

		public override string ToString() => Title;
	}
}
