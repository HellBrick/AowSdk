using System;

namespace Aow2
{
	public enum Race : sbyte
	{
		None = -1,
		Humans = 0,
		Tigrans = 1,
		Draconians = 2,
		Frostlings = 3,
		Elves = 4,
		Halflings = 5,
		Dwarves = 6,
		Archons = 7,
		DarkElves = 8,
		Orcs = 9,
		Goblins = 10,
		Undead = 11,
		Nomads = 12,
		Syrons = 13,
		ShadowDemons = 14
	}

	[Flags]
	public enum Races : short
	{
		None = 0,

		Humans = 1 << (int) Race.Humans,
		Tigrans = 1 << (int) Race.Tigrans,
		Draconians = 1 << (int) Race.Draconians,
		Frostlings = 1 << (int) Race.Frostlings,
		Elves = 1 << (int) Race.Elves,
		Halflings = 1 << (int) Race.Halflings,
		Dwarves = 1 << (int) Race.Dwarves,
		Archons = 1 << (int) Race.Archons,
		DarkElves = 1 << (int) Race.DarkElves,
		Orcs = 1 << (int) Race.Orcs,
		Goblins = 1 << (int) Race.Goblins,
		Undead = 1 << (int) Race.Undead,
		Nomads = 1 << (int) Race.Nomads,
		Syrons = 1 << (int) Race.Syrons,
		ShadowDemons = 1 << (int) Race.ShadowDemons
	}
}
