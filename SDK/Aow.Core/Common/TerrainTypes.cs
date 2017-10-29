using System;

namespace Aow2
{
	[Flags]
	public enum TerrainTypes : int
	{
		None = 0,

		Border = 1 << TerrainType.Border,
		Grass = 1 << TerrainType.Grass,
		Desert = 1 << TerrainType.Desert,
		Snow = 1 << TerrainType.Snow,
		Steppe = 1 << TerrainType.Steppe,
		Wasteland = 1 << TerrainType.Wasteland,
		Swamp = 1 << TerrainType.Swamp,
		Beach = 1 << TerrainType.Beach,
		Water = 1 << TerrainType.Water,
		Ice = 1 << TerrainType.Ice,
		Dirt = 1 << TerrainType.Dirt,
		Earth = 1 << TerrainType.Earth,
		Rock = 1 << TerrainType.Rock,
		Lava = 1 << TerrainType.Lava,
		ShadowLand = 1 << TerrainType.ShadowLand,
		ShadowWater = 1 << TerrainType.ShadowWater,
		ShadowVoid = 1 << TerrainType.ShadowVoid
	}
}
