using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b24 )]
	public class RebuildStructureEvent : Event
	{
		[Field( 0x32 )] public int StructureID { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Rebuild] ID = {0}", StructureID );
	}
}
