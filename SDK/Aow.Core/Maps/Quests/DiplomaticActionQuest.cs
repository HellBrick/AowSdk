namespace Aow2.Maps.Quests
{
	[AowClass( ID = 0x11003E6 )]
	public class DiplomaticActionQuest : Quest
	{
		[Field( 0x28 )] public DiplomaticAction Action { get; set; }
		[Field( 0x29 )] public byte TargetPlayerIndex { get; set; }
	}
}
