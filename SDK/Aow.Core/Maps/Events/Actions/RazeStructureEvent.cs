using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b17 )]
	public class RazeStructureEvent : Event
	{
		[Field( 0x32 )] public int StructureID { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Raze] ID = {0}", StructureID );
	}
}
