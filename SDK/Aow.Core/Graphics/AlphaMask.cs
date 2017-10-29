namespace Aow2.Graphics
{
	[AowClass( ID = 0x00300016 )]
	public class AlphaMask : OffsetImage
	{
		[Field( 0x19 )] public AlphaMaskShowInfo ShowInfo { get; set; }
	}

	[AowClass]
	public class AlphaMaskShowInfo : ImageShowInfo
	{
		[Field( 0x1e )] public int Color { get; set; }
	}
}
