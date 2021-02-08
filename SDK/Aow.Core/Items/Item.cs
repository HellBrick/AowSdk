using System;

namespace Aow2.Items
{
	[AowClass( ID = 0x0110017f )]
	public class Item
	{
		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string LibraryName
		{
			get => _libraryName;
			set => _libraryName = value;
		}

		[Field( 0x2a, Order = 2 )] public ItemType Type { get; set; }
		[Field( 0x22 )] public Availability Availability { get; set; }

		[Field( 0x26, Order = 2 )] public byte Damage { get; set; }
		[Field( 0x27, Order = 2 )] public byte Attack { get; set; }
		[Field( 0x28, Order = 2 )] public byte Resistance { get; set; }
		[Field( 0x29, Order = 2 )] public byte Defense { get; set; }

		[Field( 0x1f )] public ItemAbilityList Abilities { get; set; }

		[Field( 0x2b, Order = 2 )] public Spirits RewardingSpirits { get; set; }

		[Field( 0x20 )] public int GoldCost { get; set; }
		[Field( 0x21 )] public int ImageIndex { get; set; }
		[Field( 0x24, Order = 1 )] public int CreationIndex { get; set; }


		[Field( 0x1e )] public int UnknownInt1E { get; set; }

		[Field( 0x23, Order = 2 )] private ShortPascalString _name = "";
		[Field( 0x25, Order = 2 )] private ShortPascalString _libraryName = "";

		public override string ToString() => Name;
	}

	public enum Availability : byte
	{
		Frequent = 3,
		Common = 2,
		Rare = 1,
		Exceptional = 0
	}

	public enum ItemType : byte
	{
		Helmet = 0,
		Armor = 1,
		Sword = 2,
		Shield = 3,
		Ring = 4,
		Usable = 5
	}

	[Flags]
	public enum ItemTypes : byte
	{
		None = 0,

		Helmet = 1 << (int) ItemType.Helmet,
		Armor = 1 << (int) ItemType.Armor,
		Sword = 1 << (int) ItemType.Sword,
		Shield = 1 << (int) ItemType.Shield,
		Ring = 1 << (int) ItemType.Ring,
		Usable = 1 << (int) ItemType.Usable
	}
}
