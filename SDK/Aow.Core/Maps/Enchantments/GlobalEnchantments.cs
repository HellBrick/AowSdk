namespace Aow2.Maps.Enchantments
{
	[AowClass( ID = 0x1100903 )]
	public class SpellWardEnchantment : GlobalEnchantment
	{
	}

	[AowClass( ID = 0x1100904 )]
	public class PowerLeakEnchantment : GlobalEnchantment
	{
	}

	[AowClass( ID = 0x1100927 )]
	public class EarthAwarenessEnchantment : GlobalEnchantment
	{
	}

	[AowClass]
	public abstract class MasteryEnchantment : GlobalEnchantment
	{
	}

	[AowClass( ID = 0x110091a )]
	public class FireMasteryEnchantment : MasteryEnchantment
	{
	}

	[AowClass( ID = 0x110091b )]
	public class WaterMasteryEnchantment : MasteryEnchantment
	{
	}

	[AowClass( ID = 0x110091c )]
	public class EarthMasteryEnchantment : MasteryEnchantment
	{
	}

	[AowClass( ID = 0x110091d )]
	public class AirMasteryEnchantment : MasteryEnchantment
	{
	}

	[AowClass( ID = 0x110091e )]
	public class LifeMasteryEnchantment : MasteryEnchantment
	{
	}

	[AowClass( ID = 0x110091f )]
	public class DeathMasteryEnchantment : MasteryEnchantment
	{
	}

	[AowClass( ID = 0x1100920 )]
	public class CosmosMasteryEnchantment : MasteryEnchantment
	{
	}
}
