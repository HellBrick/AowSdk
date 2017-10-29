using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b1e )]
	public class ControlCounterEvent : Event
	{
		[Field( 0x32 )] public ControlCounterMode Mode { get; set; }
		[Field( 0x33 )] public int CounterID { get; set; }
		[Field( 0x34 )] public int Value { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Counter] Counter_{0} {1} {2}", CounterID, Mode == ControlCounterMode.Add ? "+=" : "=", Value );
	}

	public enum ControlCounterMode : byte
	{
		Add = 0,
		Set = 1
	}
}
