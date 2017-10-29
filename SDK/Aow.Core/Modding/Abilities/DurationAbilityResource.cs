namespace Aow2.Modding.Abilities
{
	[AowClass]
	public class DurationAbilityResource : EnchantmentAbilityResource
	{
		[Field( 0x32 )] public byte Duration { get; set; }
	}
}
