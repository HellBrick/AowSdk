namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b12 )]
	public class WatchAreaEvent : MultiplePlayersEvent
	{
		[Field( 0x32 )] public EventXYL Location { get; set; }
		[Field( 0x33 )] public byte? Radius { get; set; }
		[Field( 0x34 )] public int? Duration { get; set; }
	}
}
