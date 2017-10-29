using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b1d )]
	public class ControlPlayerRelationshipEvent : Event
	{
		[Field( 0x32 )] public sbyte FromPlayerNumber { get; set; }
		[Field( 0x33 )] public sbyte ToPlayerNumber { get; set; }
		[Field( 0x34 )] public sbyte Modifier { get; set; }

		public override string ToString() => base.ToString() +
				String.Format( " [Player relationship] {0} -> {1}: {2}",
				PlayerNumberToName( FromPlayerNumber ), PlayerNumberToName( ToPlayerNumber ), Modifier );
	}
}
