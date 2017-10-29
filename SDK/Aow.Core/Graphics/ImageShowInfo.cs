namespace Aow2.Graphics
{
	[AowClass]
	public class ImageShowInfo
	{
		public ImageShowInfo() => BlendValue = 1.0f;

		[Field( 0x14 )] public ShowMode ShowMode { get; set; }
		[Field( 0x15 )] public BlendMode BlendMode { get; set; }
		[Field( 0x16 )] public float BlendValue { get; set; }
	}

	public enum BlendMode : byte
	{
		Opaque = 0,
		Alpha = 1,
		Intensity = 2,
		Shadow = 3,
		Brighten = 4
	}

	public enum ShowMode : byte
	{
		Normal = 0,
		DitherEven = 1,
		DitherOdd = 2
	}
}
