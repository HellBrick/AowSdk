namespace Aow2.Abilities
{
	public class CombatStatusAbility : Ability
	{
		[Field( 0x1e )] public int _u1e;
		[Field( 0x1f )] public int _u1f;
	}

	[AowClass( ID = 0x0121003f )]
	public class BlindedAbility : CombatStatusAbility
	{
	}

	[AowClass( ID = 0x01110039 )]
	public class ParalyzedAbility : CombatStatusAbility
	{
	}
}
