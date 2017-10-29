using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b1f )]
	public class KillUnitEvent : Event
	{
		[Field( 0x28 )] public int UnitID { get; set; }
		[Field( 0x29 )] public sbyte KillerPlayerNumber { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Kill unit] ID = {0}", UnitID );
	}
}
