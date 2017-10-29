using Aow2.Collections;

namespace Aow2.Maps.Quests
{
	[AowClass( ID = 0x11003E2 )]
	public class BuildUpgradeQuest : Quest
	{
		public BuildUpgradeQuest() => BuildingIDs = new IntegerList();

		[Field( 0x32 )] public IntegerList BuildingIDs { get; set; }

		[Field( 0x33 )] private LongPascalString _description;
		public string Description
		{
			get => _description;
			set => _description = value;
		}
	}
}
