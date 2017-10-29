namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b0c )]
	public class TeleportEvent : Event
	{
		public TeleportEvent() => Players = new EventPlayerList();

		[Field( 0x32 )] public EventPlayerList Players { get; private set; }
		[Field( 0x33 )] public EventXYL From { get; set; }
		[Field( 0x34 )] public EventXYL To { get; set; }
	}
}
