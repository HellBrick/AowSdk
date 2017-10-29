namespace Aow2.Maps.Enchantments
{
	[AowClass]
	public abstract class DomainEnchantment : Enchantment
	{
		[Field( 0x1D )] public int SpellID { get; set; }
	}
}
