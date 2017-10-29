namespace Aow2.Modding.Abilities
{
	[AowClass]
	public class CombatAbilityResource : AbilityResource
	{
		[Field( 0x20 )] public byte Attack { get; set; }
		[Field( 0x21 )] public byte Damage { get; set; }
		[Field( 0x22 )] public DamageType DamageType { get; set; }
	}
}
