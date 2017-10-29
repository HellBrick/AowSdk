using Aow2.Collections;

namespace Aow2.Modding
{
	[AowClass]
	public abstract class ImageSequenceBase
	{
		[Field( 0x15 )] protected ShortPascalString _ilbFilename = "";
		public string ImageLibraryFilename
		{
			get => _ilbFilename;
			set => _ilbFilename = value;
		}

		[Field( 0x17 )] public IntegerList FrameTable { get; set; }
		[Field( 0x14 )] public AnimationMode AnimationMode { get; set; }
		[Field( 0x1a )] public ImageSequenceBase NextSequence { get; set; }

		[Field( 0x16 )] public int u_16;

		[Field( 0x18 )] public int u_18;
		[Field( 0x19 )] public int u_19;
		[Field( 0x1b )] public UnknownData u_1b;
	}

	[AowClass( ID = 0x00300102 )]
	public class ImageSequence : ImageSequenceBase
	{
	}

	public enum AnimationMode : byte
	{
		None = 0,
		Up,
		UpDown,
		Random,
		FrameTable,
		RandomFrameTable
	}
}
