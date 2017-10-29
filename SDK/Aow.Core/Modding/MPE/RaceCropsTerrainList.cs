namespace Aow2.Modding.MPE
{
	public class RaceCropsTerrainList : RaceDictionary<TerrainTypes>
	{
		protected override TerrainTypes[] DefaultValues => new TerrainTypes[ 15 ]
				{
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt | TerrainTypes.Desert,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt | TerrainTypes.Snow,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt | TerrainTypes.Wasteland,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt | TerrainTypes.Wasteland,
					TerrainTypes.None,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt | TerrainTypes.ShadowLand,
					TerrainTypes.Grass | TerrainTypes.Steppe | TerrainTypes.Dirt | TerrainTypes.ShadowLand
				};

		protected override TerrainTypes NullValue => TerrainTypes.None;

		protected override void WriteValue( System.IO.BinaryWriter writer, TerrainTypes value ) => writer.Write( (int) value );

		protected override TerrainTypes ReadValue( System.IO.BinaryReader reader ) => (TerrainTypes) reader.ReadInt32();
	}
}
