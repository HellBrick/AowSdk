namespace Aow2.Modding.Spells
{
	[AowClass]
	public class AddObjectsSpellResource : SpellResource
	{
		[Field( 0x3c )]
		public int Radius { get; set; }
	}
}
