namespace Aow2.Modding.Spells
{
	[AowClass]
	public class PestilenceSpellResource : StormSpellResource
	{
		[Field( 0x46 )]
		public byte DispelResistance { get; set; }
	}
}
