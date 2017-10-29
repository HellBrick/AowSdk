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
			CastingSpellID = -1;
			InventoryItems = new AowList<Item>();
			EquippedItems = new AowList<Item>();
		}

		[Field( 0x1f )] protected ShortPascalString _name;
		[Field( 0x20 )] protected ShortPascalString _title;
		[Field( 0x2a )] protected ShortPascalString _libraryName;

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

		[Field( 0x22 )] public byte BonusAttack { get; set; }
		[Field( 0x23 )] public byte BonusDefense { get; set; }
		[Field( 0x24 )] public byte BonusDamage { get; set; }
		[Field( 0x25 )] public byte BonusHP { get; set; }
		[Field( 0x26 )] public byte BonusMP { get; set; }
		[Field( 0x27 )] public byte BonusResistance { get; set; }
		
		[Field( 0x31 )] public int CurrentCastingPoints { get; set; }
		[Field( 0x28 )] public int CastingSpellID { get; set; }
		[Field( 0x2d )] public int CastingProgress { get; set; }
		[Field( 0x2e )] public int CastingPrice { get; set; }

		[Field( 0x2f )] public AowList<Item> InventoryItems { get; set; }
		[Field( 0x30 )] public AowList<Item> EquippedItems { get; set; }
		[Field( 0x32 )] public PackedImage Image { get; set; }

		[Field( 0x2c )] public AowList<Spell> _UnknownData2c;
		[Field( 0x1e )] public int _int1e;		
		[Field( 0x29 )] public int _int29 = -1;		
		[Field( 0x2b )] public int _int2b;
		
		[Field( 0x33 )] public sbyte _byte33 = -1;
		[Field( 0x34 )] public int _int34 = -1;

		public override string ToString() => Name;
	}
}
