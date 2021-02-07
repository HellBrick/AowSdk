using System;
using Aow2.Collections;
using Aow2.Graphics;
using Aow2.Items;
using Aow2.Spells;

namespace Aow2.Units
{
	[AowClass]
	public abstract class AbstractHero: AbstractUnit
	{
		protected AbstractHero()
		{
			InventoryItems = new AowList<Item>();
			EquippedItems = new AowList<Item>();
		}

		[Field( 0x1f, Order = 1 )] protected ShortPascalString _name;
		[Field( 0x20, Order = 1 )] protected ShortPascalString _title;
		[Field( 0x2a, Order = 2 )] protected ShortPascalString _libraryName;

		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string Title
		{
			get => _title;
			set => _title = value;
		}

		public string LibraryName
		{
			get => _libraryName;
			set => _libraryName = value;
		}

		[Field( 0x21 )] public UnitGender Gender { get; set; }

		[Field( 0x22 )] public byte? BonusAttack { get; set; }
		[Field( 0x23 )] public byte? BonusDefense { get; set; }
		[Field( 0x24 )] public byte? BonusDamage { get; set; }
		[Field( 0x25 )] public byte? BonusHP { get; set; }
		[Field( 0x26 )] public byte? BonusMP { get; set; }
		[Field( 0x27 )] public byte? BonusResistance { get; set; }
		
		[Field( 0x31, Order = 2 )] public int? CurrentCastingPoints { get; set; }
		[Field( 0x28 )] public int? CastingSpellID { get; set; }
		[Field( 0x2d, Order = 2 )] public int? CastingProgress { get; set; }
		[Field( 0x2e, Order = 2 )] public int? CastingPrice { get; set; }

		[Field( 0x2f, Order = 2 )] public AowList<Item> InventoryItems { get; set; }
		[Field( 0x30, Order = 2 )] public AowList<Item> EquippedItems { get; set; }
		[Field( 0x32, Order = 2 )] public UnknownData Image { get; set; }

		[Field( 0x2c, Order = 2 )] public AowList<Spell> _UnknownData2c;
		[Field( 0x1e )] public int? _int1e;
		[Field( 0x29 )] public int? _int29;
		[Field( 0x2b, Order = 2 )] public int? _int2b;
		
		[Field( 0x33, Order = 2 )] public sbyte? _byte33;
		[Field( 0x34, Order = 2 )] public int? _int34;

		public override string ToString() => Name;
	}
}
