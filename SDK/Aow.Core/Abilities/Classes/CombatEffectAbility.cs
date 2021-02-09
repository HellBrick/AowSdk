namespace Aow2.Abilities.Classes
{
	[AowClass( ID = 0x1100136 )]
	public class CombatEffectAbility : Ability
	{
		[Field( 0x1e )] public UnknownData u_1e;
	}

	[AowClass( ID = 0x01110030 )]
	public class WindWardAbility : CombatEffectAbility
	{
	}

	[AowClass( ID = 0x0121000e )]
	public class WeakenedAbility : CombatEffectAbility
	{
	}
}
