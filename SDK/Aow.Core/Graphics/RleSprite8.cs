namespace Aow2.Graphics
{
	[AowClass( ID = 0x300002 )]
	public class RleSprite8 : KeyColorImage<int>
	{
		[Field( 0x19 )] public ImageShowInfo ShowInfo { get; set; }
	}
}
