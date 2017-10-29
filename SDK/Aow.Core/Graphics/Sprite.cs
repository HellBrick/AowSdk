namespace Aow2.Graphics
{
	[AowClass( ID = 0x300014 )]
	public class Sprite : KeyColorImage<int>
	{
		[Field( 0x19 )] public ImageShowInfo ShowInfo { get; set; }
	}
}
