namespace Aow2.Abilities
{
	[AowClass( ID = 0x1110035 )]
	public class Morph : RangedAttack
	{
		[Field( 0x29 )] public int TargetUnitID { get; set; }
	}
}
