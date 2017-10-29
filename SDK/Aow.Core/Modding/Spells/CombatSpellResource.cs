namespace Aow2.Modding.Spells
{
	[AowClass]
	public class CombatSpellResource : SpellResource
	{
		[Field( 0x32 )] public int Attack { get; set; }
		[Field( 0x33 )] public int Damage { get; set; }
		[Field( 0x34 )] public int Hits { get; set; }
		[Field( 0x35 )] public DamageType DamageType { get; set; }
	}
}
