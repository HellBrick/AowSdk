using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b10 )]
	public class ExploreAreaEvent : MultiplePlayersEvent
	{
		[Field( 0x32 )] public EventXYL Point { get; set; }
		[Field( 0x33 )] public byte Radius { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Explore] {0}, Radius = {1}", Point, Radius );
	}
}
