using Aow2.Collections;
namespace Aow2.Maps.Enchantments
{
	[AowClass]
	public class EnchantmentControl
	{
		public EnchantmentControl() => List = new AowList<Enchantment>();

		[Field( 0x1e )] public AowList<Enchantment> List { get; private set; }

		[Field( 0x1f )] public int _int1f;
	}
}
