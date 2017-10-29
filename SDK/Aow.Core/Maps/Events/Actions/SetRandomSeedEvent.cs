namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b20 )]
	public class SetRandomSeedEvent : Event
	{
		[Field( 0x1e )] public int Seed { get; set; }
	}
}
