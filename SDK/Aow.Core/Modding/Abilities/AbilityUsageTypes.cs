using System;

namespace Aow2.Modding.Abilities
{
	[Flags]
	public enum AbilityUsageTypes : int
	{
		Unit = 1 << 0,
		HeadItem = 1 << 1,
		TorsoItem = 1 << 2,
		AttackItem = 1 << 3,
		DefenseItem = 1 << 4,
		RingItem = 1 << 5,
		UseItem = 1 << 6,
		CustomizeLeader = 1 << 7,
		HeroUpgrade = 1 << 8,
		Editor = 1 << 9,
		HeroWarrior = 1 << 10,
		HeroPaladin = 1 << 11,
		HeroPriest = 1 << 12,
		HeroRanger = 1 << 13,
		HeroRogue = 1 << 14,
		HeroShaman = 1 << 15,
		ItemForge = 1 << 16,
		Flag18 = 1 << 17,
		Flag19 = 1 << 18,
		Flag20 = 1 << 19,
		Flag21 = 1 << 20,
		Flag22 = 1 << 21,
		Flag23 = 1 << 22,
		Flag24 = 1 << 23,
		Flag25 = 1 << 24,
		Flag26 = 1 << 25,
		Flag27 = 1 << 26,
		Flag28 = 1 << 27,
		Flag29 = 1 << 28,
		Flag30 = 1 << 29,
		Flag31 = 1 << 30,
		Flag32 = 1 << 31
	}
}
