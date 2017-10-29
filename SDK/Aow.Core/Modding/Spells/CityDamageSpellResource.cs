namespace Aow2.Modding.Spells
{
	[AowClass]
	public class CityDamageSpellResource : SpellResource
	{
		[Field( 0x46 )] public byte Attack { get; set; }
		[Field( 0x47 )] public byte Damage { get; set; }
	}
}
