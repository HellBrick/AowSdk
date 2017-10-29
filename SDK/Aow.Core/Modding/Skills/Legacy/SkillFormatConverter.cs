using System.Collections.Generic;
using Aow2.Modding.Internal.Files;

namespace Aow2.Modding.Skills.Legacy
{
	class SkillFormatConverter : IConverter<SkillResourceListLegacy, SkillResourceList>
	{
		public SkillResourceList Convert( SkillResourceListLegacy legacyData )
		{
			SkillResourceList result = new SkillResourceList();

			FillIDs( legacyData );
			FillNames( legacyData );
			FillDefaultValues( legacyData );

			List<SkillResource> tmpList = FillList( legacyData );

			foreach ( SkillResource skill in tmpList )
			{
				skill.IsAllowedOnStart = true;
				skill.IsAllowedToResearch = skill.SkillPoints > 0;
				skill.IsLevelUp = false;
				result.Add( skill.ID, skill );
			}

			return result;
		}

		private void FillDefaultValues( SkillResourceListLegacy legacyData )
		{
			legacyData.Anarchist.Bonus = -20;
			legacyData.Bureaucrat.Multiplier = 0.75f;
			legacyData.Channeler.Multiplier = 0.9f;
			legacyData.Conqueror.Multiplier = 1.75f;

			legacyData.Constructor.OutpostBonus = 0;
			legacyData.Constructor.VillageBonus = 5;
			legacyData.Constructor.TownBonus = 10;
			legacyData.Constructor.CityBonus = 15;

			legacyData.Technophobe.OutpostBonus = 0;
			legacyData.Technophobe.VillageBonus = -5;
			legacyData.Technophobe.TownBonus = -10;
			legacyData.Technophobe.CityBonus = -15;

			legacyData.Expander.OutpostBonus = 5;
			legacyData.Expander.VillageBonus = 10;
			legacyData.Expander.TownBonus = 15;
			legacyData.Expander.CityBonus = 20;

			legacyData.Explorer.Bonus = 4;
			legacyData.Merchant.Multiplier = 1.1f;
			legacyData.Pacifist.Multiplier = 0.3f;
			legacyData.PeaceKeeper.Bonus = 20;
			legacyData.Scholar.Multiplier = 0.8f;
		}

		private static List<SkillResource> FillList( SkillResourceListLegacy legacyData )
		{
			List<SkillResource> tmpList = new List<SkillResource>()
			{
				legacyData.PeaceKeeper,
				legacyData.Explorer,
				legacyData.Expander,
				legacyData.Merchant,
				legacyData.Constructor,
				legacyData.Conqueror,
				legacyData.Survivalist,
				legacyData.Scholar,
				legacyData.Channeler,
				legacyData.Anarchist,
				legacyData.Decadence,
				legacyData.Pacifist,
				legacyData.Bureaucrat,
				legacyData.Technophobe,
				legacyData.Summoner,
				legacyData.SpellCasting,
				legacyData.Enchanter,
				legacyData.WarMage,

			};
			return tmpList;
		}

		private static void FillNames( SkillResourceListLegacy legacyData )
		{
			legacyData.Anarchist.Name = "Anarchist";
			legacyData.Bureaucrat.Name = "Bureaucrat";
			legacyData.Channeler.Name = "Channeler";
			legacyData.Conqueror.Name = "Conqueror";
			legacyData.Constructor.Name = "Constructor";
			legacyData.Decadence.Name = "Decadence";
			legacyData.Enchanter.Name = "Enchanter";
			legacyData.Expander.Name = "Expander";
			legacyData.Explorer.Name = "Explorer";
			legacyData.Merchant.Name = "Merchant";
			legacyData.Pacifist.Name = "Pacifist";
			legacyData.PeaceKeeper.Name = "Peace keeper";
			legacyData.Scholar.Name = "Scholar";
			legacyData.SpellCasting.Name = "Casting specialist";
			legacyData.Summoner.Name = "Summoner";
			legacyData.Survivalist.Name = "Survivalist";
			legacyData.Technophobe.Name = "Technophobe";
			legacyData.WarMage.Name = "War mage";
		}

		private static void FillIDs( SkillResourceListLegacy legacyData )
		{
			legacyData.Anarchist.ID = _anarchistID;
			legacyData.Bureaucrat.ID = _bureaucratID;
			legacyData.Channeler.ID = _channelerID;
			legacyData.Conqueror.ID = _conquerorID;
			legacyData.Constructor.ID = _constructorID;
			legacyData.Decadence.ID = _decadenceID;
			legacyData.Enchanter.ID = _enchanterID;
			legacyData.Expander.ID = _expanderID;
			legacyData.Explorer.ID = _explorerID;
			legacyData.Merchant.ID = _merchantID;
			legacyData.Pacifist.ID = _pacifistID;
			legacyData.PeaceKeeper.ID = _peaceKeeperID;
			legacyData.Scholar.ID = _scholarID;
			legacyData.SpellCasting.ID = _spellCastingID;
			legacyData.Summoner.ID = _summonerID;
			legacyData.Survivalist.ID = _survivalistID;
			legacyData.Technophobe.ID = _technophobeID;
			legacyData.WarMage.ID = _warMageID;
		}

		private const int _peaceKeeperID = 0x01;
		private const int _explorerID = 0x03;
		private const int _expanderID = 0x04;
		private const int _merchantID = 0x05;
		private const int _constructorID = 0x06;
		private const int _conquerorID = 0x07;
		private const int _survivalistID = 0x09;
		private const int _scholarID = 0x0a;
		private const int _channelerID = 0x0b;
		private const int _anarchistID = 0x0c;
		private const int _decadenceID = 0x0d;
		private const int _pacifistID = 0x0e;
		private const int _bureaucratID = 0x0f;
		private const int _technophobeID = 0x11;
		private const int _summonerID = 0x12;
		private const int _spellCastingID = 0x13;
		private const int _enchanterID = 0x14;
		private const int _warMageID = 0x15;
	}
}
