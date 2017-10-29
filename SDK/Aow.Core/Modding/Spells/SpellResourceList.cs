using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow2.Modding.Spells
{
	[AowClass]
	[SuppressListSerialization]
	public class SpellResourceList : IEnumerable<KeyValuePair<int, SpellResource>>
	{
		static SpellResourceList() => InitializeListAdapterMap();

		#region Colelction interfaces

		#region Pseudo-IDictionary<int,SpellResource> Members

		public ICollection<int> Keys => _fieldListAdapter.Keys;

		public bool TryGetValue( int key, out SpellResource value )
		{
			AccessorPair acc;
			if ( _fieldListAdapter.TryGetValue( key, out acc ) )
			{
				value = acc.Getter( this );
				return true;
			}
			else
			{
				value = null;
				return false;
			}
		}

		public IEnumerable<SpellResource> Values => _fieldListAdapter.Values.Select( g => g.Getter( this ) );

		public SpellResource this[ int key ] => _fieldListAdapter[ key ].Getter( this );

		public int Count => _fieldListAdapter.Count;

		#endregion

		#region IEnumerable<KeyValuePair<int,SpellResource>> Members

		public IEnumerator<KeyValuePair<int, SpellResource>> GetEnumerator() => _fieldListAdapter.Select( p => new KeyValuePair<int, SpellResource>( p.Key, p.Value.Getter( this ) ) ).GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#endregion

		#region Legacy list adapters

		private const int _listStartingIndex = 0x14;
		private static Dictionary<int, AccessorPair> _fieldListAdapter = new Dictionary<int, AccessorPair>();

		private class AccessorPair
		{
			public Func<SpellResourceList, SpellResource> Getter { get; set; }
			public Action<SpellResourceList, SpellResource> Setter { get; set; }
		}

		private static void AddAccessors( int key, string name, Func<SpellResourceList, SpellResource> getter/*, Action<SpellResourceList, SpellResource> setter*/ ) => _fieldListAdapter.Add( key, new AccessorPair() { Getter = InfoInjector( getter, key, name )/*, Setter = setter*/ } );

		private static Func<SpellResourceList, SpellResource> InfoInjector( Func<SpellResourceList, SpellResource> getter, int id, string name )
			=> list =>
			{
				SpellResource value = getter( list );

				if ( value.ID == default( int ) )
					value.ID = id;

				if ( value.Name == null )
					value.Name = name;

				return value;
			};

		private static void InitializeListAdapterMap()
		{
			AddAccessors( (int) SpellIndex.FireStorm - _listStartingIndex, "Fire storm", list => list.Fire_Storm );
			AddAccessors( (int) SpellIndex.IceStorm - _listStartingIndex, "Ice storm", list => list.Ice_Storm );
			AddAccessors( (int) SpellIndex.DeathStorm - _listStartingIndex, "Death storm", list => list.Death_Storm );
			AddAccessors( (int) SpellIndex.DivineStorm - _listStartingIndex, "Divine storm", list => list.Divine_Storm );
			AddAccessors( (int) SpellIndex.LightningStorm - _listStartingIndex, "Lightning storm", list => list.Lightning_Storm );
			AddAccessors( (int) SpellIndex.DispelMagic - _listStartingIndex, "Dispel magic", list => list.Dispel_Magic );
			AddAccessors( (int) SpellIndex.Pestilence - _listStartingIndex, "Pestilence", list => list.Pestilence );
			AddAccessors( (int) SpellIndex.FreezeWater - _listStartingIndex, "Freeze water", list => list.Freeze_Water );
			AddAccessors( (int) SpellIndex.WildFire - _listStartingIndex, "Wildfire", list => list.Wild_Fire );
			AddAccessors( (int) SpellIndex.PoisonPlants - _listStartingIndex, "Poison plants", list => list.Poison_Plants );
			AddAccessors( (int) SpellIndex.SacredWoods - _listStartingIndex, "Sacred woods", list => list.Sacred_Woods );
			AddAccessors( (int) SpellIndex.EvilWoods - _listStartingIndex, "Evil woods", list => list.Evil_Woods );
			AddAccessors( (int) SpellIndex.Haste - _listStartingIndex, "Haste", list => list.Haste );
			AddAccessors( (int) SpellIndex.EnchantWeapon - _listStartingIndex, "Enchanted weapon", list => list.Enchant_Weapon );
			AddAccessors( (int) SpellIndex.StoneSkin - _listStartingIndex, "Stone skin", list => list.Stone_Skin );
			AddAccessors( (int) SpellIndex.LevelTerrain - _listStartingIndex, "Level terrain", list => list.Level_Terrain );
			AddAccessors( (int) SpellIndex.RaiseTerrain - _listStartingIndex, "Raise terrain", list => list.Raise_Terrain );
			AddAccessors( (int) SpellIndex.SummonBlackSpider - _listStartingIndex, "Black spider", list => list.Summon_Black_Spider );
			AddAccessors( (int) SpellIndex.SummonGoldDragon - _listStartingIndex, "Gold dragon", list => list.Summon_Gold_Dragon );
			AddAccessors( (int) SpellIndex.SummonBlackDragon - _listStartingIndex, "Black dragon", list => list.Summon_Black_Dragon );
			AddAccessors( (int) SpellIndex.SummonAirElemental - _listStartingIndex, "Air elemental", list => list.Summon_Air_Elemental );
			AddAccessors( (int) SpellIndex.SummonEarthElemental - _listStartingIndex, "Earth elemental", list => list.Summon_Earth_Elemental );
			AddAccessors( (int) SpellIndex.SummonFireElemental - _listStartingIndex, "Fire elemental", list => list.Summon_Fire_Elemental );
			AddAccessors( (int) SpellIndex.SummonWaterElemental - _listStartingIndex, "Water elemental", list => list.Summon_Water_Elemental );
			AddAccessors( (int) SpellIndex.FreeMovement - _listStartingIndex, "Free movement", list => list.Free_Movement );
			AddAccessors( (int) SpellIndex.Fury - _listStartingIndex, "Fury", list => list.Fury );
			AddAccessors( (int) SpellIndex.HolyChampion - _listStartingIndex, "Holy champion", list => list.Holy_Champion );
			AddAccessors( (int) SpellIndex.UnholyChampion - _listStartingIndex, "Unholy champion", list => list.Unholy_Champion );
			AddAccessors( (int) SpellIndex.Concealment - _listStartingIndex, "Concealment", list => list.Concealment );
			AddAccessors( (int) SpellIndex.MightyMeek - _listStartingIndex, "Mighty meek", list => list.Mighty_Meek );
			AddAccessors( (int) SpellIndex.WaterWalking - _listStartingIndex, "Water walking", list => list.Water_Walking );
			AddAccessors( (int) SpellIndex.WindWalking - _listStartingIndex, "Wind walking", list => list.Wind_Walking );
			AddAccessors( (int) SpellIndex.EnchantedRoads - _listStartingIndex, "Enchanted roads", list => list.Enchanted_Roads );
			AddAccessors( (int) SpellIndex.WaterMastery - _listStartingIndex, "Water mastery", list => list.Water_Mastery );
			AddAccessors( (int) SpellIndex.ForgeBlast - _listStartingIndex, "Forge blast", list => list.Forge_Blast );
			AddAccessors( (int) SpellIndex.SummonBasilisk - _listStartingIndex, "Basilisk", list => list.Summon_Basilisk );
			AddAccessors( (int) SpellIndex.PowerLeak - _listStartingIndex, "Power leak", list => list.Power_Leak );
			AddAccessors( (int) SpellIndex.LiquidForm - _listStartingIndex, "Liquid form", list => list.Liquid_Form );
			AddAccessors( (int) SpellIndex.SummonNorthernGlow - _listStartingIndex, "Northern glow", list => list.Summon_Northern_Glow );
			AddAccessors( (int) SpellIndex.Disjunction - _listStartingIndex, "Disjunction", list => list.Disjunction );
			AddAccessors( (int) SpellIndex.Healing - _listStartingIndex, "Healing", list => list.Healing );
			AddAccessors( (int) SpellIndex.Bless - _listStartingIndex, "Bless", list => list.Bless );
			AddAccessors( (int) SpellIndex.StaticShield - _listStartingIndex, "Static shield", list => list.Static_Shield );
			AddAccessors( (int) SpellIndex.AirMastery - _listStartingIndex, "Air mastery", list => list.Air_Mastery );
			AddAccessors( (int) SpellIndex.LifeMastery - _listStartingIndex, "Life mastery", list => list.Life_Mastery );
			AddAccessors( (int) SpellIndex.DeathMastery - _listStartingIndex, "Death mastery", list => list.Death_Mastery );
			AddAccessors( (int) SpellIndex.FireMastery - _listStartingIndex, "Fire mastery", list => list.Fire_Mastery );
			AddAccessors( (int) SpellIndex.EarthMastery - _listStartingIndex, "Earth mastery", list => list.Earth_Mastery );
			AddAccessors( (int) SpellIndex.SpellWard - _listStartingIndex, "Spell ward", list => list.Spell_Ward );
			AddAccessors( (int) SpellIndex.ResurrectHero - _listStartingIndex, "Resurrect hero", list => list.Resurrect_Hero );
			AddAccessors( (int) SpellIndex.AnimateHero - _listStartingIndex, "Animate hero", list => list.Animate_Hero );
			AddAccessors( (int) SpellIndex.Tornado - _listStartingIndex, "Tornado", list => list.Tornado );
			AddAccessors( (int) SpellIndex.CallHero - _listStartingIndex, "Call hero", list => list.Call_Hero );
			AddAccessors( (int) SpellIndex.ViolentStorm - _listStartingIndex, "Violent storm", list => list.Violent_Storm );
			AddAccessors( (int) SpellIndex.Anarchy - _listStartingIndex, "Anarchy", list => list.Anarchy );
			AddAccessors( (int) SpellIndex.ClearTerrain - _listStartingIndex, "Clear terrain", list => list.Clear_Terrain );
			AddAccessors( (int) SpellIndex.MistCloak - _listStartingIndex, "Mist cloak", list => list.Mist_Cloak );
			AddAccessors( (int) SpellIndex.SpidersCurse - _listStartingIndex, "Spider curse", list => list.Spiders_Curse );
			AddAccessors( (int) SpellIndex.AnimateRuins - _listStartingIndex, "Animate ruins", list => list.Animate_Ruins );
			AddAccessors( (int) SpellIndex.DarkGift - _listStartingIndex, "Dark gift", list => list.Dark_Gift );
			AddAccessors( (int) SpellIndex.SummonPhoenix - _listStartingIndex, "Phoenix", list => list.Summon_Phoenix );
			AddAccessors( (int) SpellIndex.FireHalo - _listStartingIndex, "Fire halo", list => list.Fire_Halo );
			AddAccessors( (int) SpellIndex.SummonHellhound - _listStartingIndex, "Hell hound", list => list.Summon_Hellhound );
			AddAccessors( (int) SpellIndex.SummonAngel - _listStartingIndex, "Angel", list => list.Summon_Angel );
			AddAccessors( (int) SpellIndex.SummonFairy - _listStartingIndex, "Fairy", list => list.Summon_Fairy );
			AddAccessors( (int) SpellIndex.SummonUnicorn - _listStartingIndex, "Unicorn", list => list.Summon_Unicorn );
			AddAccessors( (int) SpellIndex.SummonBlackAngel - _listStartingIndex, "Black angel", list => list.Summon_Black_Angel );
			AddAccessors( (int) SpellIndex.SummonWyrm - _listStartingIndex, "Wyrm", list => list.Summon_Wyrm );
			AddAccessors( (int) SpellIndex.SummonEfreet - _listStartingIndex, "Efreet", list => list.Summon_Efreet );
			AddAccessors( (int) SpellIndex.SummonMinotaur - _listStartingIndex, "Minotaur", list => list.Summon_Minotaur );
			AddAccessors( (int) SpellIndex.SummonRiftLord - _listStartingIndex, "Chaos lord", list => list.Summon_Rift_Lord );
			AddAccessors( (int) SpellIndex.SummonLurker - _listStartingIndex, "Lurker", list => list.Summon_Lurker );
			AddAccessors( (int) SpellIndex.SummonZephyrBird - _listStartingIndex, "Zephyr bird", list => list.Summon_Zephyr_Bird );
			AddAccessors( (int) SpellIndex.SummonMagicServant - _listStartingIndex, "Magic servant", list => list.Summon_Magic_Servant );
			AddAccessors( (int) SpellIndex.SummonBoneDragon - _listStartingIndex, "Bone dragon", list => list.Summon_Bone_Dragon );
			AddAccessors( (int) SpellIndex.FireDomain - _listStartingIndex, "Fire domain", list => list.Fire_Domain );
			AddAccessors( (int) SpellIndex.SummonIceDragon - _listStartingIndex, "Ice dragon", list => list.Summon_Ice_Dragon );
			AddAccessors( (int) SpellIndex.Watcher - _listStartingIndex, "Watcher", list => list.Watcher );
			AddAccessors( (int) SpellIndex.LifeDomain - _listStartingIndex, "Life domain", list => list.Life_Domain );
			AddAccessors( (int) SpellIndex.ChainLightning - _listStartingIndex, "Chain lightning", list => list.Chain_Lightning );
			AddAccessors( (int) SpellIndex.Tremors - _listStartingIndex, "Tremors", list => list.Tremors );
			AddAccessors( (int) SpellIndex.GreatHail - _listStartingIndex, "Great hail", list => list.Great_Hail );
			AddAccessors( (int) SpellIndex.Fireball - _listStartingIndex, "Fireball", list => list.Fireball );
			AddAccessors( (int) SpellIndex.Geyser - _listStartingIndex, "Geyser", list => list.Geyser );
			AddAccessors( (int) SpellIndex.SacredWrath - _listStartingIndex, "Sacred wrath", list => list.Sacred_Wrath );
			AddAccessors( (int) SpellIndex.SummonRiftSpawn - _listStartingIndex, "Chaos spawn", list => list.Summon_Rift_Spawn );
			AddAccessors( (int) SpellIndex.SummonWaterDancer - _listStartingIndex, "Water dancer", list => list.Summon_Water_Dancer );
			AddAccessors( (int) SpellIndex.SummonDireBoar - _listStartingIndex, "Dire boar", list => list.Summon_Dire_Boar );
			AddAccessors( (int) SpellIndex.DomainOfDarkness - _listStartingIndex, "Domain of darkness", list => list.Domain_Of_Darkness );
			AddAccessors( (int) SpellIndex.Darkland - _listStartingIndex, "Darkland", list => list.Darkland );
			AddAccessors( (int) SpellIndex.Wetland - _listStartingIndex, "Wetland", list => list.Wetland );
			AddAccessors( (int) SpellIndex.Rejuvenate - _listStartingIndex, "Rejuvenate", list => list.Rejuvenate );
			AddAccessors( (int) SpellIndex.IceAge - _listStartingIndex, "Ice age", list => list.Ice_Age );
			AddAccessors( (int) SpellIndex.CitySpy - _listStartingIndex, "City spy", list => list.City_Spy );
			AddAccessors( (int) SpellIndex.CityPlague - _listStartingIndex, "City plague", list => list.City_Plague );
			AddAccessors( (int) SpellIndex.CityQuake - _listStartingIndex, "City quake", list => list.City_Quake );
			AddAccessors( (int) SpellIndex.GoldenAge - _listStartingIndex, "Golden age", list => list.Golden_Age );
			AddAccessors( (int) SpellIndex.Damnation - _listStartingIndex, "Damnation", list => list.Damnation );
			AddAccessors( (int) SpellIndex.SpringRains - _listStartingIndex, "Spring rains", list => list.Spring_Rains );
			AddAccessors( (int) SpellIndex.CosmosMastery - _listStartingIndex, "Cosmos mastery", list => list.Cosmos_Mastery );
			AddAccessors( (int) SpellIndex.AlterNode - _listStartingIndex, "Alter node", list => list.Alter_Node );
			AddAccessors( (int) SpellIndex.Wither - _listStartingIndex, "Wither", list => list.Wither );
			AddAccessors( (int) SpellIndex.Resurgence - _listStartingIndex, "Resurgence", list => list.Resurgence );
			AddAccessors( (int) SpellIndex.HasteDomain - _listStartingIndex, "Haste domain", list => list.Haste_Domain );
			AddAccessors( (int) SpellIndex.PoisonDomain - _listStartingIndex, "Poison domain", list => list.Poison_Domain );
			AddAccessors( (int) SpellIndex.ShadowLock - _listStartingIndex, "Shadow lock", list => list.Shadow_Lock );
			AddAccessors( (int) SpellIndex.ShadowWalking - _listStartingIndex, "Shadow walking", list => list.Shadow_Walking );
			AddAccessors( (int) SpellIndex.ShadowShift - _listStartingIndex, "Shadow shift", list => list.Shadow_Shift );
			AddAccessors( (int) SpellIndex.PurifyingWater - _listStartingIndex, "Purifying water", list => list.Purifying_Water );
			AddAccessors( (int) SpellIndex.SummonersAura - _listStartingIndex, "Summoners aura", list => list.Summoners_Aura );
			AddAccessors( (int) SpellIndex.CallOfTheForest - _listStartingIndex, "Call of the forest", list => list.Call_Of_The_Forest );
			AddAccessors( (int) SpellIndex.RecallHero - _listStartingIndex, "Recall hero", list => list.Recall_Hero );
			AddAccessors( (int) SpellIndex.EarthsAwareness - _listStartingIndex, "Earth awareness", list => list.Earths_Awareness );
			AddAccessors( (int) SpellIndex.SummonDirePenguin - _listStartingIndex, "Dire penguin", list => list.Summon_Dire_Penguin );
			AddAccessors( (int) SpellIndex.BlazingComet - _listStartingIndex, "Blazing comet", list => list.Blazing_Comet );
			AddAccessors( (int) SpellIndex.VengefulVapor - _listStartingIndex, "Vengeful vapor", list => list.Vengeful_Vapor );
			AddAccessors( (int) SpellIndex.Weaken - _listStartingIndex, "Weaken", list => list.Weaken );
			AddAccessors( (int) SpellIndex.SolarFlare - _listStartingIndex, "Shooting stars", list => list.Solar_Flare );
			AddAccessors( (int) SpellIndex.BanishSummoned - _listStartingIndex, "Banish summoned", list => list.Banish_Summoned );
			AddAccessors( (int) SpellIndex.TurnUndead - _listStartingIndex, "Turn undead", list => list.Turn_Undead );
			AddAccessors( (int) SpellIndex.CorpusFuria - _listStartingIndex, "Corpus furia", list => list.Corpus_Furia );
			AddAccessors( (int) SpellIndex.CosmicSpray - _listStartingIndex, "Cosmic spray", list => list.Cosmic_Spray );
			AddAccessors( (int) SpellIndex.Suffocate - _listStartingIndex, "Suffocate", list => list.Suffocate );
			AddAccessors( (int) SpellIndex.DeepFissure - _listStartingIndex, "Deep fissure", list => list.Deep_Fissure );
			AddAccessors( (int) SpellIndex.AnimateDead - _listStartingIndex, "Animate dead", list => list.Animate_Dead );
			AddAccessors( (int) SpellIndex.DeathRay - _listStartingIndex, "Death ray", list => list.Death_Ray );
			AddAccessors( (int) SpellIndex.WindsOfFury - _listStartingIndex, "Winds of fury", list => list.Winds_Of_Fury );
			AddAccessors( (int) SpellIndex.RainOfFire - _listStartingIndex, "HellFire", list => list.Rain_Of_Fire );
			AddAccessors( (int) SpellIndex.Swarm - _listStartingIndex, "Swarm", list => list.Swarm );
			AddAccessors( (int) SpellIndex.HighPrayer - _listStartingIndex, "High prayer", list => list.High_Prayer );
			AddAccessors( (int) SpellIndex.Stoning - _listStartingIndex, "Stoning", list => list.Stoning );
			AddAccessors( (int) SpellIndex.TowerAttack - _listStartingIndex, "Tower guard", list => list.Tower_Attack );
			AddAccessors( (int) SpellIndex.HealingShowers - _listStartingIndex, "Healing showers", list => list.Healing_Showers );
			AddAccessors( (int) SpellIndex.SkinOfOil - _listStartingIndex, "Skin of oil", list => list.Skin_Of_Oil );
			AddAccessors( (int) SpellIndex.Freedom - _listStartingIndex, "Freedom", list => list.Freedom );
			AddAccessors( (int) SpellIndex.DoubleGravity - _listStartingIndex, "Double gravity", list => list.Double_Gravity );
			AddAccessors( (int) SpellIndex.Seeker - _listStartingIndex, "Seeker", list => list.Seeker );
			AddAccessors( (int) SpellIndex.BindSummoned - _listStartingIndex, "Bind summoned", list => list.Bind_Summoned );
			AddAccessors( (int) SpellIndex.WindWard - _listStartingIndex, "Wind ward", list => list.Wind_Ward );
			AddAccessors( (int) SpellIndex.RegenerateWalls - _listStartingIndex, "Regenerate walls", list => list.Regenerate_Walls );
			AddAccessors( (int) SpellIndex.HolyLight - _listStartingIndex, "Holy light", list => list.Holy_Light );
			AddAccessors( (int) SpellIndex.UnholyDarkness - _listStartingIndex, "Unholy darkness", list => list.Unholy_Darkness );
			AddAccessors( (int) SpellIndex.Mud - _listStartingIndex, "Mud", list => list.Mud );
			AddAccessors( (int) SpellIndex.MassConfusion - _listStartingIndex, "Mass confusion", list => list.Mass_Confusion );
			AddAccessors( (int) SpellIndex.Knock - _listStartingIndex, "Crash gates", list => list.Knock );
			AddAccessors( (int) SpellIndex.Infection - _listStartingIndex, "Infection", list => list.Infection_E );
			AddAccessors( (int) SpellIndex.Combustion - _listStartingIndex, "Combustion", list => list.Combustion_E );
			AddAccessors( (int) SpellIndex.PanicAttack - _listStartingIndex, "Panic attack", list => list.Panic_Attack_E );
			AddAccessors( (int) SpellIndex.Martyr - _listStartingIndex, "Martyr", list => list.Martyr_E );
			AddAccessors( (int) SpellIndex.MagicFist - _listStartingIndex, "Magic fist", list => list.Magic_Fist );
			AddAccessors( (int) SpellIndex.BlindingFlash - _listStartingIndex, "Blinding flash", list => list.Blinding_Flash_E );
			AddAccessors( (int) SpellIndex.Rot - _listStartingIndex, "Rot", list => list.Rot );
		}

		#region Struct

		[Field( 0x17 )] private StormSpellResource Fire_Storm = new StormSpellResource();
		[Field( 0x18 )] private StormSpellResource Ice_Storm = new StormSpellResource();
		[Field( 0x19 )] private StormSpellResource Death_Storm = new StormSpellResource();
		[Field( 0x1a )] private StormSpellResource Divine_Storm = new StormSpellResource();
		[Field( 0x1b )] private StormSpellResource Lightning_Storm = new StormSpellResource();
		[Field( 0x1c )] private DispelSpellResource Dispel_Magic = new DispelSpellResource();
		[Field( 0x1d )] private PestilenceSpellResource Pestilence = new PestilenceSpellResource();
		[Field( 0x1e )] private SpellResource Freeze_Water = new SpellResource();
		[Field( 0x1f )] private AddObjectsSpellResource Wild_Fire = new AddObjectsSpellResource();
		[Field( 0x20 )] private SpellResource Poison_Plants = new SpellResource();
		[Field( 0x21 )] private SpellResource Sacred_Woods = new SpellResource();
		[Field( 0x22 )] private SpellResource Evil_Woods = new SpellResource();
		[Field( 0x23 )] private SpellResource Haste = new SpellResource();
		[Field( 0x24 )] private SpellResource Enchant_Weapon = new SpellResource();
		[Field( 0x25 )] private SpellResource Stone_Skin = new SpellResource();
		[Field( 0x26 )] private SpellResource Level_Terrain = new SpellResource();
		[Field( 0x27 )] private SpellResource Raise_Terrain = new SpellResource();
		[Field( 0x28 )] private SummonSpellResource Summon_Black_Spider = new SummonSpellResource();
		[Field( 0x2d )] private SummonSpellResource Summon_Gold_Dragon = new SummonSpellResource();
		[Field( 0x2e )] private SummonSpellResource Summon_Black_Dragon = new SummonSpellResource();
		[Field( 0x2f )] private SummonSpellResource Summon_Air_Elemental = new SummonSpellResource();
		[Field( 0x30 )] private SummonSpellResource Summon_Earth_Elemental = new SummonSpellResource();
		[Field( 0x31 )] private SummonSpellResource Summon_Fire_Elemental = new SummonSpellResource();
		[Field( 0x32 )] private SummonSpellResource Summon_Water_Elemental = new SummonSpellResource();
		[Field( 0x34 )] private SpellResource Free_Movement = new SpellResource();
		[Field( 0x35 )] private SpellResource Fury = new SpellResource();
		[Field( 0x38 )] private SpellResource Holy_Champion = new SpellResource();
		[Field( 0x39 )] private SpellResource Unholy_Champion = new SpellResource();
		[Field( 0x3b )] private SpellResource Concealment = new SpellResource();
		[Field( 0x3d )] private SpellResource Mighty_Meek = new SpellResource();
		[Field( 0x3e )] private SpellResource Water_Walking = new SpellResource();
		[Field( 0x3f )] private SpellResource Wind_Walking = new SpellResource();
		[Field( 0x40 )] private DomainSpellResource Enchanted_Roads = new DomainSpellResource();
		[Field( 0x41 )] private SpellResource Water_Mastery = new SpellResource();
		[Field( 0x42 )] private SpellResource Forge_Blast = new SpellResource();
		[Field( 0x43 )] private SummonSpellResource Summon_Basilisk = new SummonSpellResource();
		[Field( 0x44 )] private SpellResource Power_Leak = new SpellResource();
		[Field( 0x45 )] private SpellResource Liquid_Form = new SpellResource();
		[Field( 0x46 )] private SummonSpellResource Summon_Northern_Glow = new SummonSpellResource();
		[Field( 0x47 )] private DispelSpellResource Disjunction = new DispelSpellResource();
		[Field( 0x4b )] private SpellResource Healing = new SpellResource();
		[Field( 0x4c )] private SpellResource Bless = new SpellResource();
		[Field( 0x4d )] private SpellResource Static_Shield = new SpellResource();
		[Field( 0x50 )] private SpellResource Air_Mastery = new SpellResource();
		[Field( 0x51 )] private SpellResource Life_Mastery = new SpellResource();
		[Field( 0x52 )] private SpellResource Death_Mastery = new SpellResource();
		[Field( 0x53 )] private SpellResource Fire_Mastery = new SpellResource();
		[Field( 0x54 )] private SpellResource Earth_Mastery = new SpellResource();
		[Field( 0x55 )] private SpellResource Spell_Ward = new SpellResource();
		[Field( 0x56 )] private SpellResource Resurrect_Hero = new SpellResource();
		[Field( 0x57 )] private SpellResource Animate_Hero = new SpellResource();
		[Field( 0x58 )] private StormSpellResource Tornado = new StormSpellResource();
		[Field( 0x59 )] private SpellResource Call_Hero = new SpellResource();
		[Field( 0x5a )] private StormSpellResource Violent_Storm = new StormSpellResource();
		[Field( 0x5c )] private SpellResource Anarchy = new SpellResource();
		[Field( 0x5d )] private AddObjectsSpellResource Clear_Terrain = new AddObjectsSpellResource();
		[Field( 0x5e )] private AddObjectsSpellResource Mist_Cloak = new AddObjectsSpellResource();
		[Field( 0x5f )] private AddObjectsSpellResource Spiders_Curse = new AddObjectsSpellResource();
		[Field( 0x60 )] private SpellResource Animate_Ruins = new SpellResource();
		[Field( 0x61 )] private SpellResource Dark_Gift = new SpellResource();
		[Field( 0x63 )] private SummonSpellResource Summon_Phoenix = new SummonSpellResource();
		[Field( 0x64 )] private SpellResource Fire_Halo = new SpellResource();
		[Field( 0x65 )] private SummonSpellResource Summon_Hellhound = new SummonSpellResource();
		[Field( 0x66 )] private SummonSpellResource Summon_Angel = new SummonSpellResource();
		[Field( 0x67 )] private SummonSpellResource Summon_Fairy = new SummonSpellResource();
		[Field( 0x68 )] private SummonSpellResource Summon_Unicorn = new SummonSpellResource();
		[Field( 0x69 )] private SummonSpellResource Summon_Black_Angel = new SummonSpellResource();
		[Field( 0x6a )] private SummonSpellResource Summon_Wyrm = new SummonSpellResource();
		[Field( 0x6b )] private SummonSpellResource Summon_Efreet = new SummonSpellResource();
		[Field( 0x6c )] private SummonSpellResource Summon_Minotaur = new SummonSpellResource();
		[Field( 0x6d )] private SummonSpellResource Summon_Rift_Lord = new SummonSpellResource();
		[Field( 0x6e )] private SummonSpellResource Summon_Lurker = new SummonSpellResource();
		[Field( 0x6f )] private SummonSpellResource Summon_Zephyr_Bird = new SummonSpellResource();
		[Field( 0x70 )] private SummonSpellResource Summon_Magic_Servant = new SummonSpellResource();
		[Field( 0x71 )] private SummonSpellResource Summon_Bone_Dragon = new SummonSpellResource();
		[Field( 0x72 )] private DomainSpellResource Fire_Domain = new DomainSpellResource();
		[Field( 0x73 )] private SummonSpellResource Summon_Ice_Dragon = new SummonSpellResource();
		[Field( 0x76 )] private DomainSpellResource Watcher = new DomainSpellResource();
		[Field( 0x77 )] private DomainSpellResource Life_Domain = new DomainSpellResource();
		[Field( 0x7f )] private AreaCombatSpellResource Chain_Lightning = new AreaCombatSpellResource();
		[Field( 0x84 )] private CombatSpellResource Tremors = new CombatSpellResource();
		[Field( 0x88 )] private AreaCombatSpellResource Great_Hail = new AreaCombatSpellResource();
		[Field( 0x89 )] private AreaCombatSpellResource Fireball = new AreaCombatSpellResource();
		[Field( 0x8a )] private CombatSpellResource Geyser = new CombatSpellResource();
		[Field( 0x91 )] private CombatSpellResource Sacred_Wrath = new CombatSpellResource();
		[Field( 0x97 )] private SummonSpellResource Summon_Rift_Spawn = new SummonSpellResource();
		[Field( 0x98 )] private SummonSpellResource Summon_Water_Dancer = new SummonSpellResource();
		[Field( 0x99 )] private SummonSpellResource Summon_Dire_Boar = new SummonSpellResource();
		[Field( 0xaa )] private DomainSpellResource Domain_Of_Darkness = new DomainSpellResource();
		[Field( 0xac )] private DomainSpellResource Darkland = new DomainSpellResource();
		[Field( 0xad )] private DomainSpellResource Wetland = new DomainSpellResource();
		[Field( 0xae )] private DomainSpellResource Rejuvenate = new DomainSpellResource();
		[Field( 0xaf )] private DomainSpellResource Ice_Age = new DomainSpellResource();
		[Field( 0xb0 )] private DomainSpellResource City_Spy = new DomainSpellResource();
		[Field( 0xb1 )] private SpellResource City_Plague = new SpellResource();
		[Field( 0xb2 )] private CityDamageSpellResource City_Quake = new CityDamageSpellResource();
		[Field( 0xb3 )] private DomainSpellResource Golden_Age = new DomainSpellResource();
		[Field( 0xb4 )] private DomainSpellResource Damnation = new DomainSpellResource();
		[Field( 0xb5 )] private DomainSpellResource Spring_Rains = new DomainSpellResource();
		[Field( 0xb6 )] private SpellResource Cosmos_Mastery = new SpellResource();
		[Field( 0xb7 )] private SpellResource Alter_Node = new SpellResource();
		[Field( 0xb8 )] private SpellResource Wither = new SpellResource();
		[Field( 0xb9 )] private SpellResource Resurgence = new SpellResource();
		[Field( 0xba )] private DomainSpellResource Haste_Domain = new DomainSpellResource();
		[Field( 0xbb )] private DomainSpellResource Poison_Domain = new DomainSpellResource();
		[Field( 0xbc )] private DomainSpellResource Shadow_Lock = new DomainSpellResource();
		[Field( 0xbd )] private SpellResource Shadow_Walking = new SpellResource();
		[Field( 0xbe )] private SpellResource Shadow_Shift = new SpellResource();
		[Field( 0xc0 )] private DomainSpellResource Purifying_Water = new DomainSpellResource();
		[Field( 0xc1 )] private DomainSpellResource Summoners_Aura = new DomainSpellResource();
		[Field( 0xc2 )] private DomainSpellResource Call_Of_The_Forest = new DomainSpellResource();
		[Field( 0xc3 )] private SpellResource Recall_Hero = new SpellResource();
		[Field( 0xc4 )] private SpellResource Earths_Awareness = new SpellResource();
		[Field( 0xc5 )] private SummonSpellResource Summon_Dire_Penguin = new SummonSpellResource();
		[Field( 0xe1 )] private CombatSpellResource Blazing_Comet = new CombatSpellResource();
		[Field( 0xe2 )] private CombatSpellResource Vengeful_Vapor = new CombatSpellResource();
		[Field( 0xe3 )] private CombatSpellResource Weaken = new CombatSpellResource();
		[Field( 0xe4 )] private CombatSpellResource Solar_Flare = new CombatSpellResource();
		[Field( 0xe5 )] private CombatSpellResource Banish_Summoned = new CombatSpellResource();
		[Field( 0xe6 )] private CombatSpellResource Turn_Undead = new CombatSpellResource();
		[Field( 0xe7 )] private AreaCombatSpellResource Corpus_Furia = new AreaCombatSpellResource();
		[Field( 0xe8 )] private AreaCombatSpellResource Cosmic_Spray = new AreaCombatSpellResource();
		[Field( 0xe9 )] private CombatSpellResource Suffocate = new CombatSpellResource();
		[Field( 0xea )] private CombatSpellResource Deep_Fissure = new CombatSpellResource();
		[Field( 0xeb )] private CombatSpellResource Animate_Dead = new CombatSpellResource();
		[Field( 0xec )] private CombatSpellResource Death_Ray = new CombatSpellResource();
		[Field( 0xed )] private CombatSpellResource Winds_Of_Fury = new CombatSpellResource();
		[Field( 0xee )] private CombatSpellResource Rain_Of_Fire = new CombatSpellResource();
		[Field( 0xef )] private AreaCombatSpellResource Swarm = new AreaCombatSpellResource();
		[Field( 0xf0 )] private CombatSpellResource High_Prayer = new CombatSpellResource();
		[Field( 0xf1 )] private CombatSpellResource Stoning = new CombatSpellResource();
		[Field( 0xf2 )] private CombatSpellResource Tower_Attack = new CombatSpellResource();
		[Field( 0xf3 )] private AreaCombatSpellResource Healing_Showers = new AreaCombatSpellResource();
		[Field( 0xf4 )] private CombatSpellResource Skin_Of_Oil = new CombatSpellResource();
		[Field( 0xf5 )] private CombatSpellResource Freedom = new CombatSpellResource();
		[Field( 0xf6 )] private CombatSpellResource Double_Gravity = new CombatSpellResource();
		[Field( 0xf7 )] private SpellResource Seeker = new SpellResource();
		[Field( 0xf8 )] private CombatSpellResource Bind_Summoned = new CombatSpellResource();
		[Field( 0xf9 )] private CombatSpellResource Wind_Ward = new CombatSpellResource();
		[Field( 0xfa )] private CombatSpellResource Regenerate_Walls = new CombatSpellResource();
		[Field( 0xfb )] private CombatSpellResource Holy_Light = new CombatSpellResource();
		[Field( 0xfc )] private CombatSpellResource Unholy_Darkness = new CombatSpellResource();
		[Field( 0xfd )] private CombatSpellResource Mud = new CombatSpellResource();
		[Field( 0xfe )] private CombatSpellResource Mass_Confusion = new CombatSpellResource();
		[Field( 0xff )] private CombatSpellResource Knock = new CombatSpellResource();
		[Field( 0x100 )] private CombatSpellResource Infection_E = new CombatSpellResource();
		[Field( 0x101 )] private CombatSpellResource Combustion_E = new CombatSpellResource();
		[Field( 0x102 )] private CombatSpellResource Panic_Attack_E = new CombatSpellResource();
		[Field( 0x103 )] private CombatSpellResource Martyr_E = new CombatSpellResource();
		[Field( 0x104 )] private CombatSpellResource Magic_Fist = new CombatSpellResource();
		[Field( 0x105 )] private CombatSpellResource Blinding_Flash_E = new CombatSpellResource();
		[Field( 0x106 )] private CombatSpellResource Rot = new CombatSpellResource();

		#endregion

		#endregion
	}
}
