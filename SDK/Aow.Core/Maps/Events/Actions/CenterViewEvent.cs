using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b11 )]
	public class CenterViewEvent : MultiplePlayersEvent
	{
		[Field( 0x32 )] public EventXYL Point { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Center view] {0}", Point.ToString() );
	}
}
