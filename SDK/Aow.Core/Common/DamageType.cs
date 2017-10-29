using System;

namespace Aow2
{
	[Flags]
	public enum DamageType : short
	{
		Fire = 0x1,
		Cold = 0x2,
		Lightning = 0x4,
		Magic = 0x8,
		Poison = 0x10,
		Death = 0x20,
		Holy = 0x40,
		Physical = 0x80,
		Wall = 0x100,

		None = 0x0
	}
}
