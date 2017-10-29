namespace Aow2.Graphics
{
	[AowClass]
	public abstract class KeyColorImage<TColor> : OffsetImage
	{
		[Field( 0x40 )] public TColor KeyColorIndex { get; set; }
	}
}
