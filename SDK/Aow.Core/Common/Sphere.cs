using System;

namespace Aow2
{
	public enum Sphere : byte
	{
		Common = 0,
		Fire = 1,
		Water = 2,
		Earth = 3,
		Air = 4,
		Life = 5,
		Death = 6,
		Cosmos = 7,
		Secret = 8,
		Random = 9
	}

	[Flags]
	public enum SphereMask : short
	{
		None = 0,

		Common = 1 << Sphere.Common,
		Fire = 1 << Sphere.Fire,
		Water = 1 << Sphere.Water,
		Earth = 1 << Sphere.Earth,
		Air = 1 << Sphere.Air,
		Life = 1 << Sphere.Life,
		Death = 1 << Sphere.Death,
		Cosmos = 1 << Sphere.Cosmos,
		Secret = 1 << Sphere.Secret
	}
}
