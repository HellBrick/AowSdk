namespace Aow2.Units
{
	[AowClass( ID = 0x0110011d )]
	public class Unit: AbstractUnit
	{
		[Field( 0x1e )] public short _short1e;	//	depending on some weird conditions this field can contain different values
		[Field( 0x1f )] public byte _byte1f;
	}
}
