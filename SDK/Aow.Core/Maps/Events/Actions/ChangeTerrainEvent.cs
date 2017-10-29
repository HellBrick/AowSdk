using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b22 )]
	public class ChangeTerrainEvent : Event
	{
		[Field( 0x32 )] public EventXYL Point { get; set; }
		[Field( 0x33 )] public int Radius { get; set; }
		[Field( 0x34 )] public TerrainType NewTerrain { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Change terrain] {0}, {1}, Radius = {2}", NewTerrain, Point.ToString(), Radius );
	}
}
