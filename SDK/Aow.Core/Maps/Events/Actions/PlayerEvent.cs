namespace Aow2.Maps.Events.Actions
{
	[AowClass]
	public abstract class PlayerEvent : Event
	{
		[Field( 0x28 )] public sbyte PlayerNumber { get; set; }
	}
}
