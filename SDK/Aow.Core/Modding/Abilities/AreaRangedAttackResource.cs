namespace Aow2.Modding.Abilities
{
	[AowClass]
	public class AreaRangedAttackResource : RangedAttackResource
	{
		[Field( 0x3c )] public int Radius { get; set; }
	}
}
