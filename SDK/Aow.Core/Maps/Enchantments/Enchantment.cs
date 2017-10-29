namespace Aow2.Maps.Enchantments
{
	[AowClass]
	public abstract class Enchantment
	{
		[Field( 0x15 )] public int Upkeep { get; set; }
		[Field( 0x17 )] public int Mana { get; set; }
		[Field( 0x16 )] public byte PlayerIndex { get; set; }

		[Field( 0x14 )] public int _int14;  //	spell index?		
		[Field( 0x18 )] public byte _byte18 = 1;
	}
}
