using Aow2.Collections;

namespace Aow2
{
	[AowClass( ID = 0x01100177 )]
	public class UnitIDList
	{
		public UnitIDList() => UnitIDs = new IntegerList();

		[Field( 0x1e )]
		public IntegerList UnitIDs { get; private set; }
	}
}
