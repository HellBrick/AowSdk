using System;

namespace Aow2.Units
{
	public enum HeroClass : byte
	{
		Warrior = 0,
		Paladin = 1,
		Priest = 2,
		Ranger = 3,
		Rogue = 4,
		Shaman = 5
	}

	[Flags]
	public enum HeroClasses : byte
	{
		None = 0,

		Warrior = 1 << (int) HeroClass.Warrior,
		Paladin = 1 << (int) HeroClass.Paladin,
		Priest = 1 << (int) HeroClass.Priest,
		Ranger = 1 << (int) HeroClass.Ranger,
		Rogue = 1 << (int) HeroClass.Rogue,
		Shaman = 1 << (int) HeroClass.Shaman,
	}
}
