using Aow2.Collections;

namespace Aow2.Maps.Quests
{
	[AowClass( ID = 0x11003E7 )]
	public class HuntInfidelsQuest : Quest
	{
		public HuntInfidelsQuest() => UnitIDs = new IntegerList();

		[Field( 0x28 )] public IntegerList UnitIDs { get; set; }
		[Field( 0x29 )] public bool _bool29;
	}
}
