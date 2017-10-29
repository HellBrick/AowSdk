namespace Aow2.Modding.Abilities
{
	[AowClass]
	public class RangedAttackResource : CombatAbilityResource
	{
		[Field( 0x2a )] public int Shots { get; set; }
		[Field( 0x2b )] public ShootRange Range { get; set; }
	}
}
