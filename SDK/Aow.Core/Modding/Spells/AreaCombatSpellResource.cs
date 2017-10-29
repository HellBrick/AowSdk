namespace Aow2.Modding.Spells
{
	[AowClass]
	public class AreaCombatSpellResource : CombatSpellResource
	{
		[Field( 0x3c )]
		public int Radius { get; set; }
	}
}
