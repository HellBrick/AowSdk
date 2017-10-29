namespace Aow2.Modding.MPE
{
	[AowClass]
	public class MpeSettings
	{
		public MpeSettings()
		{
			ProductionTransferRate = 1.0f;
			ResearchConversionRate = 0.3f;

			OutpostPopulation = 110;
			PioneerCitySize = 600;
			FirstPioneerPricePenalty = 50;

			OutpostGarrisonPrice = 80;
			VillageGarrisonPrice = 140;
			TownGarrisonPrice = 260;
			CityGarrisonPrice = 500;

			GarrisonStrengthMod = 0.25f;

			SpellLimits = new SpellLimits();
			BribeMultiplyers = new UnitBribeMultiplyers();
			RaceAlignments = new RaceAlignments();
			RaceTerrainMorals = new RaceTerrainMoralList();
			RaceCropsTerrains = new RaceCropsTerrainList();

			WoodenGateHP = 20;
			StoneGateHP = 30;
			WoodenWallHP = 35;
			StoneWallHP = 50;

			UpkeepLevel0 = 4;
			UpkeepGoldPerLevel = 3;

			SpellGraph = new SpellGraph();

			MinHitChance = 0.05f;
			MaxHitChance = 0.95f;
			ElementalStatusAttack = 8;
		}

		[Field( 0xAAA0 )] public float ProductionTransferRate { get; set; }
		[Field( 0xAAA1 )] public float ResearchConversionRate { get; set; }

		[Field( 0xAAA9 )] public int OutpostPopulation { get; set; }
		[Field( 0xAAB0 )] public int PioneerCitySize { get; set; }
		[Field( 0xAAA2 )] public int FirstPioneerPricePenalty { get; set; }

		[Field( 0xAAA3 )] public int OutpostGarrisonPrice { get; set; }
		[Field( 0xAAA4 )] public int VillageGarrisonPrice { get; set; }
		[Field( 0xAAA5 )] public int TownGarrisonPrice { get; set; }
		[Field( 0xAAA6 )] public int CityGarrisonPrice { get; set; }

		[Field( 0xAAA7 )] public float GarrisonStrengthMod { get; set; }

		[Field( 0xAAA8 )] public SpellLimits SpellLimits { get; set; }

		[Field( 0xAAB1 )] public UnitBribeMultiplyers BribeMultiplyers { get; set; }

		[Field( 0xAAB2 )] public RaceAlignments RaceAlignments { get; set; }
		[Field( 0xAAB3 )] public RaceTerrainMoralList RaceTerrainMorals { get; set; }

		[Field( 0xAAB4 )] public RaceCropsTerrainList RaceCropsTerrains { get; set; }

		[Field( 0xAAB5 )] public int WoodenGateHP { get; set; }
		[Field( 0xAAB6 )] public int StoneGateHP { get; set; }
		[Field( 0xAAB7 )] public int WoodenWallHP { get; set; }
		[Field( 0xAAB8 )] public int StoneWallHP { get; set; }

		[Field( 0xAAB9 )] public int UpkeepLevel0 { get; set; }
		[Field( 0xAAC0 )] public int UpkeepGoldPerLevel { get; set; }

		[Field( 0xAAC1 )] public SpellGraph SpellGraph { get; set; }

		[Field( 0xAAC2 )] public float MinHitChance { get; set; }
		[Field( 0xAAC3 )] public float MaxHitChance { get; set; }

		[Field( 0xAAC4 )] public int ElementalStatusAttack { get; set; }

		[Field( 0xAAC5 )] public int MissDamage { get; set; }
	}
}
