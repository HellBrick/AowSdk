namespace Aow2.Maps.Diplomacy
{
	[AowClass( ID = 0x1100016 )]
	public class DiplomaticHistoryItem
	{
		[Field( 0x14 )] public int? PlayerIndex { get; set; }
		[Field( 0x15 )] public DiplomaticAction Action { get; set; }
	}
}
