namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b14 )]
	public class SpawnUnitsEvent : Event
	{
		public SpawnUnitsEvent() => Units = new RandomUnitIDList();

		[Field( 0x32 )] public RandomUnitIDList Units { get; private set; }
		[Field( 0x34 )] public EventXYL Location { get; set; }
		[Field( 0x35 )] public ArmyBehavior Behavior { get; set; }
		[Field( 0x36 )] public sbyte PlayerNumber { get; set; }
	}
}
