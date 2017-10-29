namespace Aow2.Abilities
{
	[AowClass( ID = 0x0111000f )]
	public class Healing : TouchAbility
	{
		[Field( 0x32 )] public bool IsUsed { get; set; }
	}
}
