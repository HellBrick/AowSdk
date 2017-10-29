namespace Aow2.Graphics.Internal
{
	[AowClass( ID = 0x300010 )]
	public class Picture : AowImage
	{
		[Field( 0x19 )]
		public ImageShowInfo ShowInfo { get; set; }
	}
}
