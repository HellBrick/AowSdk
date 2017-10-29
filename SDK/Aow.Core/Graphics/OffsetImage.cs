namespace Aow2.Graphics
{
	[AowClass]
	public abstract class OffsetImage : AowImage
	{
		[Field( 0x3c )] public int DataWidth { get; set; }
		[Field( 0x3d )] public int DataHeight { get; set; }
		[Field( 0x3e )] public int DataOffsetX { get; set; }
		[Field( 0x3f )] public int DataOffsetY { get; set; }

		protected bool InDataArea( int y, int x ) => ( DataOffsetX <= x ) && ( x < DataOffsetX + DataWidth ) &&
				( DataOffsetY <= y ) && ( y < DataOffsetY + DataHeight );
	}
}
