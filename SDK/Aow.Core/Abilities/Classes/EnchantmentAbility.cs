namespace Aow2.Abilities
{
	[AowClass( ID = 0x0110016a )]
	public class EnchantmentAbility : Ability
	{
		[Field( 0x1e )]
		public EnchantmentData u_1e;
	}

	[AowClass]
	public abstract class CombatEnchantmentEbility : EnchantmentAbility
	{
	}

	[AowClass( ID = 0x0121004a )]
	public class HighPrayerAbility : CombatEnchantmentEbility
	{
	}
}
