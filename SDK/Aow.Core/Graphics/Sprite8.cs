namespace Aow2.Graphics
{
	[AowClass( ID = 0x300003 )]
	public class Sprite8 : KeyColorImage<byte>
	{
		[Field( 0x19 )] public ImageShowInfo ShowInfo { get; set; }
		[Field( 0x41 )] public bool u_41;
	}
}
