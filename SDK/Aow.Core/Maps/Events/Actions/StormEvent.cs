namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b05 )]
	public class StormEvent : PlayerEvent
	{
		[Field( 0x32 )] public EventXYL Location { get; set; }
		[Field( 0x34 )] public StormType Type { get; set; }
		[Field( 0x35 )] public int Radius { get; set; }
	}

	public enum StormType : byte
	{
		Fire = 1,
		Death,
		Holy,
		Ice,
		Lightning,
		Poison
	}
}
