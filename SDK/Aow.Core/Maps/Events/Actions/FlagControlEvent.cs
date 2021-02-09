using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b0f )]
	public class FlagControlEvent : Event
	{
		public FlagControlEvent() => Value = true;

		[Field( 0x32 )] public int? FlagID { get; set; }
		[Field( 0x33 )] public bool? Value { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Flag] Flag_{0} = {1}", FlagID, Value );
	}
}
