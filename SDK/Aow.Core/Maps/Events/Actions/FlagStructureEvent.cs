using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b16 )]
	public class FlagStructureEvent : Event
	{
		[Field( 0x32 )] public int StructureID { get; set; }
		[Field( 0x33 )] public sbyte? PlayerNumber { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Flag structure] ID = {0}, {1}", StructureID, PlayerOrIndNumberToName( PlayerNumber ?? 0 ) );
	}
}
