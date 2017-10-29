using Aow2.Collections;

namespace Aow2.Modding.Spells
{
	[AowClass]
	public class SummonSpellResource : SpellResource
	{
		[Field( 0x32 )]
		public IntegerList UnitIDs { get; set; }
	}
}
