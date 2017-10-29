using System;
using Aow2.Collections;
using Aow2.Modding.Generated;
using Utils.Text;

namespace Aow2.Modding
{
	[AowClass( ID = 0x0110012b )]
	public class RaceResource
	{
		[Field( 0x0c )] private int _race;
		public Race Race
		{
			get => (Race) _race;
			set => _race = (int) value;
		}

		[Field( 0x26 )] public int ProductionBonusOutpost { get; set; }
		[Field( 0x29 )] public int ProductionBonusVillage { get; set; }
		[Field( 0x2c )] public int ProductionBonusTown { get; set; }
		[Field( 0x31 )] public int ProductionBonusCity { get; set; }

		[Field( 0x24 )] public int GoldBonusOutpost { get; set; }
		[Field( 0x27 )] public int GoldBonusVillage { get; set; }
		[Field( 0x2a )] public int GoldBonusTown { get; set; }
		[Field( 0x2f )] public int GoldBonusCity { get; set; }

		[Field( 0x25 )] public int ManaBonusOutpost { get; set; }
		[Field( 0x28 )] public int ManaBonusVillage { get; set; }
		[Field( 0x2b )] public int ManaBonusTown { get; set; }
		[Field( 0x30 )] public int ManaBonusCity { get; set; }

		[Field( 0x33 )] public int ResearchBonusOutpost { get; set; }
		[Field( 0x34 )] public int ResearchBonusVillage { get; set; }
		[Field( 0x35 )] public int ResearchBonusTown { get; set; }
		[Field( 0x36 )] public int ResearchBonusCity { get; set; }

		[Field( 0x37 )] public int GrowthBonusOutpost { get; set; }
		[Field( 0x38 )] public int GrowthBonusVillage { get; set; }
		[Field( 0x39 )] public int GrowthBonusTown { get; set; }
		[Field( 0x3a )] public int GrowthBonusCity { get; set; }

		[Field( 0x19 )] public IntegerList BaseUnitsID;
		[Field( 0x1a )] public IntegerList BarracksUnitsID;
		[Field( 0x1c )] public IntegerList WarHallUnitsID;
		[Field( 0x1d )] public IntegerList ChampionsGuildUnitsID;
		[Field( 0x1e )] public IntegerList MonasteryUnitsID;
		[Field( 0x1f )] public IntegerList SiegeWorkshopUnitsID;
		[Field( 0x20 )] public IntegerList SpecialUnitsID;
		[Field( 0x21 )] public IntegerList MastersGuildUnitsID;
		[Field( 0x22 )] public IntegerList TempleComplexUnitsID;
		[Field( 0x23 )] public IntegerList BuildersHallUnitsID;
		[Field( 0x32 )] public IntegerList ShipyardUnitsID;
		[Field( 0x3c )] public IntegerList RacialUnitsID;

		[Field( 0x3b )] private LongPascalString _cityNames = "";
		public string CityNames
		{
			get => _cityNames;
			set => _cityNames = value;
		}

		[Field( 0x16 )] private LongPascalString _description = "";
		public string Description
		{
			get => _description;
			set => _description = value;
		}

		#region Generated
		[Field( 0x0a )] public int U_StrangeIndex { get; set; }  //	1-12 for WT races, 18-20 for SM races
		[Field( 0x0b )] public int u_0b; //	always 0


		[Field( 0x18 )] public TILManagerItem u_18;
		[Field( 0x17 )] public TImageSequenceList isl;
		[Field( 0x3d )] public TEFilename filename;
		#endregion

		public override string ToString() => Race.ToSplitWordString();

		#region Obsolete

		private const string _obsoleteListComment = "Is not used by the game";

		[Field( 0x14 )]
		[Obsolete( _obsoleteListComment )]
		private IntegerList _list14;

		[Field( 0x15 )]
		[Obsolete( _obsoleteListComment )]
		private IntegerList _list15;

		#endregion
	}
}
