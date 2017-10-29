using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b07 )]
	public class UnitsJoinEvent : PlayerEvent
	{
		public UnitsJoinEvent()
		{
			Message = "";
			Units = new RandomUnitIDList();
		}

		[Field( 0x32 )] public RandomUnitIDList Units { get; private set; }
		[Field( 0x33 )] public int Price { get; set; }
		[Field( 0x34 )] public EventXYL Location { get; set; }

		[Field( 0x35 )] private LongPascalString message;
		public string Message
		{
			get => message;
			set => message = value;
		}
	}
}
