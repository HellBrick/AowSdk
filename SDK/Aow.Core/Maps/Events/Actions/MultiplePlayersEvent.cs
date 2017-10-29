namespace Aow2.Maps.Events.Actions
{
	[AowClass]
	public abstract class MultiplePlayersEvent : Event
	{
		public MultiplePlayersEvent() => Players = new EventPlayerList();

		[Field( 0x28 )] public EventPlayerList Players { get; protected set; }
	}
}
