using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aow2.Collections;
using Aow2.Units;
using Utils.Text;

namespace Aow2.Modding.Units
{
	[AowClass( ID = 0x0110011e )]
	public class UnitResource : EResource
	{
		public UnitResource()
		{
			Abilities = new AowList<Ability>();
			SilverUpgrades = new AowList<Ability>();
			GoldUpgrades = new AowList<Ability>();
			_UnknownEnum26 = Enum26.Flag1;
		}

		[Field( 0x14 )] private ShortPascalString _name;
		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string Description { get; set; }

		[Field( 0x23 )]
		private LongPascalString _fullDescription
		{
			get
			{
				StringBuilder builder = new StringBuilder();
				AppendUpgradeDescription( builder );
				builder.AppendLine();
				builder.Append( Description );

				return builder.ToString();
			}
			set => Description = ClearUpgradeDescription( value );
		}

		[Field( 0x15 )] private ShortPascalString _class;
		public string Class
		{
			get => _class;
			set => _class = value;
		}

		[Field( 0x16 )] public Alignment Alignment { get; set; }
		[Field( 0x17 )] public Race Race { get; set; }
		[Field( 0x24 )] public int Price { get; set; }

		[Field( 0x18 )] public byte Attack { get; set; }
		[Field( 0x1a )] public byte Damage { get; set; }
		[Field( 0x19 )] public byte Defense { get; set; }
		[Field( 0x1d )] public byte Resistance { get; set; }
		[Field( 0x1b )] public byte HP { get; set; }
		[Field( 0x1c )] public byte MP { get; set; }
		[Field( 0x1e )] public byte Level { get; set; }

		[Field( 0x1f )] public UnitType Type { get; set; }
		[Field( 0x21 )] public UnitGender Gender { get; set; }
		[Field( 0x25 )] public UnitSize Size { get; set; }
		[Field( 0x29 )] public int ModelIndex { get; set; }

		[Field( 0x22 )] public AowList<Ability> Abilities { get; private set; }
		[Field( 0x27 )] public AowList<Ability> SilverUpgrades { get; private set; }
		[Field( 0x28 )] public AowList<Ability> GoldUpgrades { get; private set; }

		[Field( 0xA0 )] public bool IsGarrisonDisabled { get; set; }

		[Field( 0x20 )] public UnknownEnum _UnknownEnum20; //	1 bytes
		[Field( 0x26 )] public Enum26 _UnknownEnum26;

		[Flags]
		public enum Enum26 : byte
		{
			Flag1 = 1,  //	Set for everyone except Frost witch
			Flag2 = 2,  //	Set for Slither, Croacadile and Goblyn wyvern

			None = 0
		}

		private string ClearUpgradeDescription( string description )
		{
			//	remove upatch descriptions
			description = Regex.Replace( description, @"Silver:[^\n]*\n", "" );
			description = Regex.Replace( description, @"Gold:[^\n]*\n", "" );

			//	remove MPE descriptions
			description = Regex.Replace( description, @"\(silver\)[^\n]*\n", "" );
			description = Regex.Replace( description, @"\(gold\)[^\n]*\n", "" );

			//	remove free space
			description = Regex.Replace( description, @"^\s+", "" );

			return description;
		}

		private void AppendUpgradeDescription( StringBuilder builder )
		{
			builder.Append( "(silver)\t" );
			AppendAbilityList( builder, SilverUpgrades );
			builder.AppendLine();

			builder.Append( "(gold)\t" );
			AppendAbilityList( builder, GoldUpgrades );
			builder.AppendLine();
		}

		private static void AppendAbilityList( StringBuilder builder, IEnumerable<Ability> abilities )
		{
			if ( abilities.Count() != 0 )
			{
				builder.AppendCollection<string>( abilities.Select( a => a.ToString() ), ", " );
			}
			else
				builder.Append( "<none>" );
		}

		public override string ToString() => Name;
	}
}
