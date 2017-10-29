using System;

namespace Aow2
{
	public enum Spirit : byte
	{
		Order = 1,
		War = 2,
		Nature = 3,
		Magic = 4
	}

	[Flags]
	public enum Spirits : byte
	{
		Order = 1 << ( Spirit.Order - 1 ),
		War = 1 << ( Spirit.War - 1 ),
		Nature = 1 << ( Spirit.Nature - 1 ),
		Magic = 1 << ( Spirit.Magic - 1 ),

		None = 0
	}
}
