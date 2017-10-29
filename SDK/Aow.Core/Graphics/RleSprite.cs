namespace Aow2.Graphics
{
	[AowClass( ID = 0x00300011 )]
	public class RleSprite : KeyColorImage<int>
	{
		[Field( 0x19 )] public ImageShowInfo ShowInfo { get; set; }
	}
}
