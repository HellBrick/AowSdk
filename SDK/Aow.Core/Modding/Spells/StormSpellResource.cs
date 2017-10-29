namespace Aow2.Modding.Spells
{
	[AowClass]
	public class StormSpellResource : SpellResource
	{
		[Field( 0x32 )] public byte Attack { get; set; }
		[Field( 0x33 )] public byte Damage { get; set; }
	}
}
