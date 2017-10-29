namespace Aow2.Graphics
{
	[AowClass( ID = 0x300000 )]
	public class JpegImage : AowImage
	{
		[Field( 0x19 )]
		public ImageShowInfo ShowInfo { get; set; }
	}
}
