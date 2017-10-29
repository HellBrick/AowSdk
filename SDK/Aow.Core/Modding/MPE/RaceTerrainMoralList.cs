using System.IO;

namespace Aow2.Modding.MPE
{
	public class DoubleTerrainFlags
	{
		public DoubleTerrainFlags()
		{
		}

		public DoubleTerrainFlags( TerrainTypes friendly, TerrainTypes hostile )
		{
			Friendly = friendly;
			Hostile = hostile;
		}

		public TerrainTypes Friendly { get; set; }
		public TerrainTypes Hostile { get; set; }
	}

	public class RaceTerrainMoralList : RaceDictionary<DoubleTerrainFlags>
	{
		protected override DoubleTerrainFlags[] DefaultValues => new DoubleTerrainFlags[]
				{
					new DoubleTerrainFlags( TerrainTypes.None, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.Desert, TerrainTypes.Snow | TerrainTypes.Ice ),
					new DoubleTerrainFlags( TerrainTypes.None, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.Snow | TerrainTypes.Ice, TerrainTypes.Desert ),
					new DoubleTerrainFlags( TerrainTypes.Grass, TerrainTypes.Wasteland ),
					new DoubleTerrainFlags( TerrainTypes.Grass, TerrainTypes.Wasteland ),
					new DoubleTerrainFlags( TerrainTypes.Dirt, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.None, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.Dirt, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.None, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.Dirt, TerrainTypes.None ),
					new DoubleTerrainFlags( TerrainTypes.Wasteland, TerrainTypes.Grass ),
					new DoubleTerrainFlags( TerrainTypes.Desert, TerrainTypes.Snow | TerrainTypes.Ice ),
					new DoubleTerrainFlags (TerrainTypes.ShadowLand, TerrainTypes.None ),
					new DoubleTerrainFlags (TerrainTypes.ShadowLand, TerrainTypes.None )
				};

		protected override DoubleTerrainFlags NullValue => new DoubleTerrainFlags();

		protected override void WriteValue( BinaryWriter writer, DoubleTerrainFlags value )
		{
			writer.Write( (int) value.Friendly );
			writer.Write( (int) value.Hostile );
		}

		protected override DoubleTerrainFlags ReadValue( BinaryReader reader ) => new DoubleTerrainFlags()
		{
			Friendly = (TerrainTypes) reader.ReadInt32(),
			Hostile = (TerrainTypes) reader.ReadInt32()
		};
	}
}
