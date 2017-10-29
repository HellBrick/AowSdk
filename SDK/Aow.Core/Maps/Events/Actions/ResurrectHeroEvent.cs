using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b23 )]
	public class ResurrectHeroEvent : Event
	{
		[Field( 0x32 )] public EventXYL Location { get; set; }
		[Field( 0x33 )] public sbyte PlayerNumber { get; set; }
		[Field( 0x34 )] public int HeroID { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Resurrect hero] ID = {0}, {1}", HeroID, Location );
	}
}
