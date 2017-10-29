using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow2.Modding.Abilities
{
	[AowClass]
	[SuppressListSerialization]
	public class AbilityResourceList : IReadOnlyDictionary<AbilityID, AbilityResource>
	{
		#region IReadOnlyDictionary<AbilityID,AbilityResource> Members

		public bool ContainsKey( AbilityID key ) => _fieldListAdapter.ContainsKey( key );

		public IEnumerable<AbilityID> Keys => _fieldListAdapter.Keys;

		public bool TryGetValue( AbilityID key, out AbilityResource value )
		{
			if ( _fieldListAdapter.TryGetValue( key, out Accessor accessor ) )
			{
				value = accessor.Getter( this );
				return true;
			}
			else
			{
				value = null;
				return false;
			}
		}

		public IEnumerable<AbilityResource> Values => _fieldListAdapter.Values.Select( g => g.Getter( this ) );

		public AbilityResource this[ AbilityID key ] => _fieldListAdapter[ key ].Getter( this );

		#endregion

		#region IReadOnlyCollection<KeyValuePair<AbilityID,AbilityResource>> Members

		public int Count => _fieldListAdapter.Count;

		#endregion

		#region IEnumerable<KeyValuePair<AbilityID,AbilityResource>> Members

		public IEnumerator<KeyValuePair<AbilityID, AbilityResource>> GetEnumerator() => _fieldListAdapter.Select( p => new KeyValuePair<AbilityID, AbilityResource>( p.Key, p.Value.Getter( this ) ) ).GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region List adapters

		private class Accessor
		{
			public Func<AbilityResourceList, AbilityResource> Getter { get; set; }
			public Action<AbilityResourceList, AbilityResource> Setter { get; set; }
		}

		private static readonly Dictionary<AbilityID, Accessor> _fieldListAdapter = new Dictionary<AbilityID, Accessor>();

		static AbilityResourceList() => InitializeFieldIDMap();

		private static Accessor WrapInfoInjector( Func<AbilityResourceList, AbilityResource> getter, AbilityID id, string name ) => new Accessor()
		{
			Getter =
					list =>
					{
						AbilityResource ar = getter( list );

						if ( String.IsNullOrEmpty( ar.Name ) )
							ar.Name = name;

						if ( ar.ID == default( AbilityID ) )
							ar.ID = id;

						return ar;
					}
		};

		private static void AddAdapter( AbilityID id, Func<AbilityResourceList, AbilityResource> getter, string name ) => _fieldListAdapter.Add( id, WrapInfoInjector( getter, id, name ) );

		private static void InitializeFieldIDMap()
		{
			AddAdapter( AbilityID.Walking, list => list._walking, "Walking" );
			AddAdapter( AbilityID.Flying, list => list._flying, "Flying" );
			AddAdapter( AbilityID.Swimming, list => list._swimming, "Swimming" );
			AddAdapter( AbilityID.Forestry, list => list._forestry, "Forestry" );
			AddAdapter( AbilityID.CaveCrawling, list => list._cave_crawling, "Cave crawling" );
			AddAdapter( AbilityID.Mountaineering, list => list._mountaineering, "Mountaineering" );
			AddAdapter( AbilityID.MagicImmunity, list => list._magic_immunity, "Magic immunity" );
			AddAdapter( AbilityID.FireImmunity, list => list._fire_immunity, "Fire immunity" );
			AddAdapter( AbilityID.ColdImmunity, list => list._cold_immunity, "Cold immunity" );
			AddAdapter( AbilityID.LightningImmunity, list => list._lightning_immunity, "Lightning immunity" );
			AddAdapter( AbilityID.PoisonImmunity, list => list._poison_immunity, "Poison immunity" );
			AddAdapter( AbilityID.DeathImmunity, list => list._death_immunity, "Death immunity" );
			AddAdapter( AbilityID.HolyImmunity, list => list._holy_immunity, "Holy immunity" );
			AddAdapter( AbilityID.PhysicalImmunity, list => list._physical_immunity, "Physical immunity" );
			AddAdapter( AbilityID.FireStrike, list => list._fire_strike, "Fire strike" );
			AddAdapter( AbilityID.ColdStrike, list => list._cold_strike, "Cold strike" );
			AddAdapter( AbilityID.LightningStrike, list => list._lightning_strike, "Lightning strike" );
			AddAdapter( AbilityID.PoisonStrike, list => list._poison_strike, "Poison strike" );
			AddAdapter( AbilityID.DeathStrike, list => list._death_strike, "Death strike" );
			AddAdapter( AbilityID.HolyStrike, list => list._holy_strike, "Holy strike" );
			AddAdapter( AbilityID.MagicStrike, list => list._magic_strike, "Magic strike" );
			AddAdapter( AbilityID.Strike, list => list._strike, "Strike" );
			AddAdapter( AbilityID.Archery, list => list._archery, "Archery" );
			AddAdapter( AbilityID.Sailing, list => list._sailing, "Sailing" );
			AddAdapter( AbilityID.HurlBoulder, list => list._hurl_boulder, "Hurl boulder" );
			AddAdapter( AbilityID.HurlStones, list => list._hurl_stones, "Hurl stones" );
			AddAdapter( AbilityID.TrailOfDarkness, list => list._trail_of_darkness, "Trail of darkness" );
			AddAdapter( AbilityID.Web, list => list._web, "Web" );
			AddAdapter( AbilityID.Dominate, list => list._dominate, "Dominate" );
			AddAdapter( AbilityID.Seduce, list => list._seduce, "Seduce" );
			AddAdapter( AbilityID.FlameThrowing, list => list._flame_throwing, "Flame throwing" );
			AddAdapter( AbilityID.Markmanship, list => list._markmanship, "Marksmanship" );
			AddAdapter( AbilityID.WallClimbing, list => list._wall_climbing, "Wall climbing" );
			AddAdapter( AbilityID.BardSkills, list => list._bards_skills, "Bard skills" );
			AddAdapter( AbilityID.TurnUndead, list => list._turn_undead, "Turn undead" );
			AddAdapter( AbilityID.NightVision, list => list._night_vision, "Night vision" );
			AddAdapter( AbilityID.Entangle, list => list._entangle, "Entangle" );
			AddAdapter( AbilityID.TrueSeeing, list => list._true_seeing, "True seeing" );
			AddAdapter( AbilityID.Tunneling, list => list._tunneling, "Tunneling" );
			AddAdapter( AbilityID.Ignition, list => list._ignition, "Ignition" );
			AddAdapter( AbilityID.FireCannon, list => list._fire_cannon, "Fire cannon" );
			AddAdapter( AbilityID.FirePistol, list => list._fire_pistol, "Fire pistol" );
			AddAdapter( AbilityID.Leadership, list => list._leadership, "Leadership" );
			AddAdapter( AbilityID.Healing, list => list._healing, "Healing" );
			AddAdapter( AbilityID.Seduced, list => list._seduced, "Seduced" );
			AddAdapter( AbilityID.Regeneration, list => list._regeneration, "Regeneration" );
			AddAdapter( AbilityID.Transport, list => list._transport, "Transport" );
			AddAdapter( AbilityID.CauseFear, list => list._cause_fear, "Cause fear" );
			AddAdapter( AbilityID.SpellCasting, list => list._spell_casting, "Spell casting" );
			AddAdapter( AbilityID.ShadowShift, list => list._shadow_shift, "Shadow shift" );
			AddAdapter( AbilityID.Invisibility, list => list._invisibility, "Invisibility" );
			AddAdapter( AbilityID.DoomGaze, list => list._doom_gaze, "Doom gaze" );
			AddAdapter( AbilityID.CastingSpecialist, list => list._casting_specialist, "Casting specialist" );
			AddAdapter( AbilityID.ShootJavelin, list => list._shoot_javelin, "Shoot javelin" );
			AddAdapter( AbilityID.BlackJavelin, list => list._black_javelin, "Black javelin" );
			AddAdapter( AbilityID.Floating, list => list._floating, "Floating" );
			AddAdapter( AbilityID.DispelMagic, list => list._dispel_magic, "Dispel magic" );
			AddAdapter( AbilityID.PoisonDarts, list => list._poison_darts, "Poison darts" );
			AddAdapter( AbilityID.VenomousSpit, list => list._venomous_spit, "Venomous spit" );
			AddAdapter( AbilityID.Dragon, list => list._dragon, "Dragon" );
			AddAdapter( AbilityID.Vision, list => list._vision, "Vision" );
			AddAdapter( AbilityID.SelfDestruct, list => list._self_destruct, "Self destruct" );
			AddAdapter( AbilityID.PassWalls, list => list._pass_wall, "Pass walls" );
			AddAdapter( AbilityID.MagicRelay, list => list._magic_relay, "Magic relay" );
			AddAdapter( AbilityID.PathOfDecay, list => list._path_of_decay, "Path of decay" );
			AddAdapter( AbilityID.PathOfLife, list => list._path_of_life, "Path of life" );
			AddAdapter( AbilityID.DeathProtection, list => list._death_protection, "Death protection" );
			AddAdapter( AbilityID.FireProtection, list => list._fire_protection, "Fire protection" );
			AddAdapter( AbilityID.HolyProtection, list => list._holy_protection, "Holy protection" );
			AddAdapter( AbilityID.PoisonProtection, list => list._poison_protection, "Poison protection" );
			AddAdapter( AbilityID.LightningProtection, list => list._lightning_protection, "Lightning protection" );
			AddAdapter( AbilityID.MagicProtection, list => list._magic_protection, "Magic protection" );
			AddAdapter( AbilityID.ColdProtection, list => list._cold_protection, "Cold protection" );
			AddAdapter( AbilityID.PhysicalProtection, list => list._physical_protection, "Physical protection" );
			AddAdapter( AbilityID.FireBreath, list => list._fire_breath, "Fire breath" );
			AddAdapter( AbilityID.ColdBreath, list => list._cold_breath, "Cold breath" );
			AddAdapter( AbilityID.BlackBreath, list => list._black_breath, "Black breath" );
			AddAdapter( AbilityID.DivineBreath, list => list._divine_breath, "Divine breath" );
			AddAdapter( AbilityID.GasBreath, list => list._gas_breath, "Gas breath" );
			AddAdapter( AbilityID.Stunned, list => list._stunned, "Stunned" );
			AddAdapter( AbilityID.Cursed, list => list._cursed, "Cursed" );
			AddAdapter( AbilityID.Entangled, list => list._entangled, "Entangled" );
			AddAdapter( AbilityID.Frozen, list => list._frozen, "Frozen" );
			AddAdapter( AbilityID.Poisoned, list => list._poisoned, "Poisoned" );
			AddAdapter( AbilityID.Webbed, list => list._webbed, "Webbed" );
			AddAdapter( AbilityID.Vertigo, list => list._vertigo, "Vertigo" );
			AddAdapter( AbilityID.HasteDuration, list => list._haste_duration, "Haste" );
			AddAdapter( AbilityID.Possess, list => list._possess, "Possess" );
			AddAdapter( AbilityID.Possessed, list => list._possessed, "Possessed" );
			AddAdapter( AbilityID.Panicked, list => list._panicked, "Panicked" );
			AddAdapter( AbilityID.PathOfFrost, list => list._path_of_frost, "Path of frost" );
			AddAdapter( AbilityID.SnowWanderer, list => list._snow_wanderer, "Snow wanderer" );
			AddAdapter( AbilityID.Charge, list => list._charge, "Charge" );
			AddAdapter( AbilityID.DragonSlaying, list => list._dragon_slaying, "Dragon slaying" );
			AddAdapter( AbilityID.Block, list => list._block, "Block" );
			AddAdapter( AbilityID.RoundAttack, list => list._round_attack, "Round attack" );
			AddAdapter( AbilityID.ExtraStrike, list => list._extra_strike, "Extra strike" );
			AddAdapter( AbilityID.FirstStrike, list => list._first_strike, "First strike" );
			AddAdapter( AbilityID.BuildRoads, list => list._build_roads, "Build roads" );
			AddAdapter( AbilityID.LifeStealing, list => list._life_stealing, "Life stealing" );
			AddAdapter( AbilityID.EntangleStrike, list => list._entangle_strike, "Entangle strike" );
			AddAdapter( AbilityID.BlackBolts, list => list._black_bolts, "Black bolts" );
			AddAdapter( AbilityID.MagicBolts, list => list._magic_bolts, "Magic bolts" );
			AddAdapter( AbilityID.FrostBolts, list => list._frost_bolts, "Frot bolts" );
			AddAdapter( AbilityID.LightningBolts, list => list._lightning_bolts, "Lightning bolts" );
			AddAdapter( AbilityID.HolyBolts, list => list._holy_bolts, "Holy bolts" );
			AddAdapter( AbilityID.Burning, list => list._burning, "Burning" );
			AddAdapter( AbilityID.Resurrected, list => list._resurrected, "Resurrected" );
			AddAdapter( AbilityID.Animated, list => list._animated, "Animated" );
			AddAdapter( AbilityID.SteppeConcealment, list => list._steppe_concealment, "Steppe concealment" );
			AddAdapter( AbilityID.UGConcealment, list => list._underground_concealment, "UG concealment" );
			AddAdapter( AbilityID.GrassConcealment, list => list._grass_concealment, "Grass concealment" );
			AddAdapter( AbilityID.SnowConcealment, list => list._snow_concealment, "Snow concealment" );
			AddAdapter( AbilityID.DesertConcealment, list => list._desert_concealment, "Desert concealment" );
			AddAdapter( AbilityID.WastelandConcealment, list => list._wasteland_concealment, "Wasteland concealment" );
			AddAdapter( AbilityID.WaterConcealment, list => list._water_concealment, "Water concealment" );
			AddAdapter( AbilityID.Concealment, list => list._concealment, "Conceament" );
			AddAdapter( AbilityID.HolyChampion, list => list._holy_champion, "Holy champion" );
			AddAdapter( AbilityID.UnholyChampion, list => list._unholy_champion, "Unholy champion" );
			AddAdapter( AbilityID.Charmed, list => list._charmed, "Charmed" );
			AddAdapter( AbilityID.Dominated, list => list._dominated, "Dominated" );
			AddAdapter( AbilityID.Haste, list => list._haste, "Haste" );
			AddAdapter( AbilityID.StoneSkin, list => list._stone_skin, "Stone skin" );
			AddAdapter( AbilityID.EnchantedWeapon, list => list._enchanted_weapon, "Enchanted weapon" );
			AddAdapter( AbilityID.Summoned, list => list._summoned, "Summoned" );
			AddAdapter( AbilityID.Fury, list => list._fury, "Fury" );
			AddAdapter( AbilityID.FreeMovement, list => list._free_movement, "Free movement" );
			AddAdapter( AbilityID.Blessed, list => list._blessed, "Blessed" );
			AddAdapter( AbilityID.HolyChampionEnchantment, list => list._holy_champion_enchantment, "Holy champion (enchantment)" );
			AddAdapter( AbilityID.UnholyChampionEnchantment, list => list._unholy_champion_enchantment, "Unholy champion (enchantment)" );
			AddAdapter( AbilityID.ConcealmentEnchantment, list => list._concealment_enchantment, "Concealment (enchantment)" );
			AddAdapter( AbilityID.FireHalo, list => list._fire_halo, "Fire halo" );
			AddAdapter( AbilityID.WaterWalking, list => list._water_walking, "Water walking" );
			AddAdapter( AbilityID.WindWalking, list => list._wind_walking, "Wind walking" );
			AddAdapter( AbilityID.LiquidForm, list => list._liquid_form, "Liquid form" );
			AddAdapter( AbilityID.DarkGift, list => list._dark_gift, "Dark gift" );
			AddAdapter( AbilityID.Mounted, list => list._mounted, "Mounted" );
			AddAdapter( AbilityID.Animal, list => list._animal, "Animal" );
			AddAdapter( AbilityID.Undead, list => list._undead, "Undead" );
			AddAdapter( AbilityID.BuildOutpost, list => list._build_outpost, "Build outpost" );
			AddAdapter( AbilityID.FireDomain, list => list._fire_domain, "Fire domain" );
			AddAdapter( AbilityID.LifeDomain, list => list._life_domain, "Life domain" );
			AddAdapter( AbilityID.RebuildStructure, list => list._rebuild_structure, "Rebuild structure" );
			AddAdapter( AbilityID.StaticShield, list => list._static_shield, "Static shield" );
			AddAdapter( AbilityID.PoisonDomain, list => list._poison_domain, "Poison domain" );
			AddAdapter( AbilityID.Caravan, list => list._caravan, "Caravan" );
			AddAdapter( AbilityID.ShadowSickness, list => list._shadow_sickness, "Shadow sickness" );
			AddAdapter( AbilityID.ShadowWalker, list => list._shadow_walker, "Shadow walker" );
			AddAdapter( AbilityID.ShadowWalkingEnchantment, list => list._shadow_walking_enchantment, "Shadow walking (enchantment)" );
			AddAdapter( AbilityID.DigAdit, list => list._dig_adit, "Dig an adit" );
			AddAdapter( AbilityID.ShadowWalkingDuration, list => list._shadow_walking_duration, "Shadow walking (duration)" );
			AddAdapter( AbilityID.SummonersAura, list => list._summoners_aura, "Summoner's aura" );
			AddAdapter( AbilityID.AnimatedHero, list => list._animated_hero, "Animated hero" );
			AddAdapter( AbilityID.DoubleStrike, list => list._double_strike, "Double strike" );
			AddAdapter( AbilityID.Willpower, list => list._willpower, "Willpower" );
			AddAdapter( AbilityID.Steam, list => list._steam, "Steam" );
			AddAdapter( AbilityID.Blurred, list => list._blurred, "Blurred" );
			AddAdapter( AbilityID.PhysicalWeakness, list => list._physical_weakness, "Physical weakness" );
			AddAdapter( AbilityID.MagicWeakness, list => list._magic_weakness, "Magic weakness" );
			AddAdapter( AbilityID.FireWeakness, list => list._fire_weakness, "Fire weakness" );
			AddAdapter( AbilityID.ColdWeakness, list => list._cold_weakness, "Cold weakness" );
			AddAdapter( AbilityID.DeathWeakness, list => list._death_weakness, "Death weakness" );
			AddAdapter( AbilityID.HolyWeakness, list => list._holy_weakness, "Holy weakness" );
			AddAdapter( AbilityID.PoisonWeakness, list => list._poison_weakness, "Poison weakness" );
			AddAdapter( AbilityID.LightningWeakness, list => list._lightning_weakness, "Lightning weakness" );
			AddAdapter( AbilityID.WallCrushing, list => list._wall_crushing, "Wall crushing" );
			AddAdapter( AbilityID.Sabotage, list => list._sabotage, "Sabotage" );
			AddAdapter( AbilityID.EnergyDrain, list => list._energy_drain, "Energy drain" );
			AddAdapter( AbilityID.EnergyDrained, list => list._energy_drained, "Energy drained" );
			AddAdapter( AbilityID.Phase, list => list._phase, "Phase" );
			AddAdapter( AbilityID.ControlAnimal, list => list._control_animal, "Control animal" );
			AddAdapter( AbilityID.Taunt, list => list._taunt, "Taunt" );
			AddAdapter( AbilityID.FireCrossbow, list => list._fire_crossbow, "Fire crossbow" );
			AddAdapter( AbilityID.RepairMachine, list => list._repair_machine, "Repair machine" );
			AddAdapter( AbilityID.ThrowBlade, list => list._throw_blade, "Throw blade" );
			AddAdapter( AbilityID.FireBolts, list => list._fire_bolts, "Fire bolts" );
			AddAdapter( AbilityID.FeralMount, list => list._feral_mount, "Feral mount" );
			AddAdapter( AbilityID.Controlled, list => list._controlled, "Controlled" );
			AddAdapter( AbilityID.MagicalMount, list => list._magical_mount, "Magical mount" );
			AddAdapter( AbilityID.Weakened, list => list._weakened, "Weakened" );
			AddAdapter( AbilityID.ResurgenceEnchantment, list => list._resurgence_enchantment, "Resurgence (enchantment)" );
			AddAdapter( AbilityID.SwallowWhole, list => list._swallow_whole, "Shallow whole" );
			AddAdapter( AbilityID.Swarmed, list => list._swarmed, "Swarmed" );
			AddAdapter( AbilityID.Taunted, list => list._taunted, "Taunted" );
			AddAdapter( AbilityID.SmokyHaze, list => list._smoky_haze, "Smoky haze" );
			AddAdapter( AbilityID.OilySkin, list => list._oily_skin, "Oily skin" );
			AddAdapter( AbilityID.HurlFirebomb, list => list._hurl_firebomb, "Hurl firebomb" );
			AddAdapter( AbilityID.ResurgenceCombat, list => list._resurgence_combat, "Resurgence" );
			AddAdapter( AbilityID.Trap, list => list._trap, "Trap" );
			AddAdapter( AbilityID.Trapped, list => list._trapped, "Trapped" );
			AddAdapter( AbilityID.Grasp, list => list._grasp, "Grasp" );
			AddAdapter( AbilityID.Devour, list => list._devour, "Devour" );
			AddAdapter( AbilityID.SpawnLarva, list => list._spawn_larva, "Spawn larva" );
			AddAdapter( AbilityID.Metamorphosis, list => list._metamorphosis, "Metamorphosis" );
			AddAdapter( AbilityID.StealEnchantment, list => list._steal_enchantment, "Steal enchantment" );
			AddAdapter( AbilityID.Bombard, list => list._bombard, "Bombard" );
			AddAdapter( AbilityID.ThrowSpear, list => list._throw_spear, "Throw spear" );
			AddAdapter( AbilityID.HurlLightning, list => list._hurl_lightning, "Hurl lightning" );
			AddAdapter( AbilityID.Whirlwind, list => list._whirlwind, "Whirlwind" );
			AddAdapter( AbilityID.DoubleGravity, list => list._double_gravity, "Double gravity" );
			AddAdapter( AbilityID.HolyLight, list => list._holy_light, "Holy light" );
			AddAdapter( AbilityID.UnholyDarkness, list => list._unholy_darkness, "Unholy darkness" );
			AddAdapter( AbilityID.WindWard, list => list._wind_ward, "Wind ward" );
			AddAdapter( AbilityID.Mud, list => list._mud, "Mud" );
			AddAdapter( AbilityID.Confused, list => list._confused, "Confused" );
			AddAdapter( AbilityID.SeekerEnchantment, list => list._seeker_enchantment, "Seeker (enchantment)" );
			AddAdapter( AbilityID.Changeling, list => list._changeling, "Changeling" );
			AddAdapter( AbilityID.Morph, list => list._morph, "Morph" );
			AddAdapter( AbilityID.Paralyzed, list => list._paralyzed, "Paralyzed" );
			AddAdapter( AbilityID.Enslaved, list => list._enslaved, "Enslaved" );
			AddAdapter( AbilityID.SpreadAttack, list => list._spread_attack, "Spread attack" );
			AddAdapter( AbilityID.Strangle, list => list._strangle, "Strangle" );
			AddAdapter( AbilityID.Infected, list => list._infected, "Infected" );
			AddAdapter( AbilityID.Infect, list => list._infect, "Infect" );
			AddAdapter( AbilityID.Resurrect, list => list._resurrect, "Resurrect" );
			AddAdapter( AbilityID.AnimateCorpse, list => list._animate_corpse, "Animate corpse" );
			AddAdapter( AbilityID.Martyr, list => list._martyr, "Martyr" );
			AddAdapter( AbilityID.Ram, list => list._ram, "Ram" );
			AddAdapter( AbilityID.PixieDust, list => list._pixie_dust, "Pixie dust" );
			AddAdapter( AbilityID.DrainWill, list => list._drain_will, "Drain will" );
			AddAdapter( AbilityID.WillDrained, list => list._will_drained, "Will drained" );
			AddAdapter( AbilityID.FrostBlowing, list => list._frost_blowing, "Frost blowing" );
			AddAdapter( AbilityID.ShootJavelins, list => list._shoot_javelins, "Shoot javelins" );
			AddAdapter( AbilityID.DragonGrowth, list => list._dragon_growth, "Dragon growth" );
			AddAdapter( AbilityID.MightyMeek, list => list._mighty_meek, "Mighty meek" );
			AddAdapter( AbilityID.Blinded, list => list._blinded, "Blinded" );
			AddAdapter( AbilityID.Rotting, list => list._rotting, "Rotting" );
			AddAdapter( AbilityID.Seeker, list => list._seeker, "Seeker" );
			AddAdapter( AbilityID.DraconianGrowth, list => list._draconian_growth, "Draconian growth" );
			AddAdapter( AbilityID.PoleArm, list => list._polearm, "Pole arm" );
			AddAdapter( AbilityID.HighPrayer, list => list._high_prayer, "High prayer" );
		}

		#region Struct
		[Field( 0x0a )] private AbilityResource _walking = new AbilityResource();
		[Field( 0x0b )] private AbilityResource _flying = new AbilityResource();
		[Field( 0x0c )] private AbilityResource _swimming = new AbilityResource();
		[Field( 0x0d )] private AbilityResource _forestry = new AbilityResource();
		[Field( 0x0e )] private AbilityResource _cave_crawling = new AbilityResource();
		[Field( 0x0f )] private AbilityResource _mountaineering = new AbilityResource();
		[Field( 0x10 )] private AbilityResource _magic_immunity = new AbilityResource();
		[Field( 0x11 )] private AbilityResource _fire_immunity = new AbilityResource();
		[Field( 0x12 )] private AbilityResource _cold_immunity = new AbilityResource();
		[Field( 0x13 )] private AbilityResource _lightning_immunity = new AbilityResource();
		[Field( 0x14 )] private AbilityResource _poison_immunity = new AbilityResource();
		[Field( 0x15 )] private AbilityResource _death_immunity = new AbilityResource();
		[Field( 0x16 )] private AbilityResource _holy_immunity = new AbilityResource();
		[Field( 0x17 )] private AbilityResource _physical_immunity = new AbilityResource();
		[Field( 0x18 )] private AbilityResource _fire_strike = new AbilityResource();
		[Field( 0x19 )] private AbilityResource _cold_strike = new AbilityResource();
		[Field( 0x1a )] private AbilityResource _lightning_strike = new AbilityResource();
		[Field( 0x1b )] private AbilityResource _poison_strike = new AbilityResource();
		[Field( 0x1c )] private AbilityResource _death_strike = new AbilityResource();
		[Field( 0x1d )] private AbilityResource _holy_strike = new AbilityResource();
		[Field( 0x1e )] private AbilityResource _magic_strike = new AbilityResource();
		[Field( 0x1f )] private AbilityResource _strike = new AbilityResource();
		[Field( 0x20 )] private RangedAttackResource _archery = new RangedAttackResource();
		[Field( 0x21 )] private AbilityResource _sailing = new AbilityResource();
		[Field( 0x22 )] private RangedAttackResource _hurl_boulder = new RangedAttackResource();
		[Field( 0x23 )] private RangedAttackResource _hurl_stones = new RangedAttackResource();
		[Field( 0x24 )] private AbilityResource _trail_of_darkness = new AbilityResource();
		[Field( 0x25 )] private CombatAbilityResource _web = new CombatAbilityResource();
		[Field( 0x26 )] private CombatAbilityResource _dominate = new CombatAbilityResource();
		[Field( 0x27 )] private CombatAbilityResource _seduce = new CombatAbilityResource();
		[Field( 0x28 )] private RangedAttackResource _flame_throwing = new RangedAttackResource();
		[Field( 0x2a )] private AbilityResource _markmanship = new AbilityResource();
		[Field( 0x2e )] private AbilityResource _wall_climbing = new AbilityResource();
		[Field( 0x2f )] private AbilityResource _bards_skills = new AbilityResource();
		[Field( 0x30 )] private CombatAbilityResource _turn_undead = new CombatAbilityResource();
		[Field( 0x31 )] private AbilityResource _night_vision = new AbilityResource();
		[Field( 0x32 )] private CombatAbilityResource _entangle = new CombatAbilityResource();
		[Field( 0x33 )] private AbilityResource _true_seeing = new AbilityResource();
		[Field( 0x34 )] private AbilityResource _tunneling = new AbilityResource();
		[Field( 0x35 )] private AbilityResource _ignition = new AbilityResource();
		[Field( 0x36 )] private RangedAttackResource _fire_cannon = new RangedAttackResource();
		[Field( 0x37 )] private RangedAttackResource _fire_pistol = new RangedAttackResource();
		[Field( 0x38 )] private AbilityResource _leadership = new AbilityResource();
		[Field( 0x39 )] private CombatAbilityResource _healing = new CombatAbilityResource();
		[Field( 0x3a )] private AbilityResource _seduced = new AbilityResource();
		[Field( 0x3b )] private AbilityResource _regeneration = new AbilityResource();
		[Field( 0x3c )] private AbilityResource _transport = new AbilityResource();
		[Field( 0x3d )] private AbilityResource _cause_fear = new AbilityResource();
		[Field( 0x3e )] private CombatAbilityResource _spell_casting = new CombatAbilityResource();
		[Field( 0x3f )] private AbilityResource _shadow_shift = new AbilityResource();
		[Field( 0x40 )] private AbilityResource _invisibility = new AbilityResource();
		[Field( 0x41 )] private RangedAttackResource _doom_gaze = new RangedAttackResource();
		[Field( 0x42 )] private AbilityResource _casting_specialist = new AbilityResource();
		[Field( 0x43 )] private RangedAttackResource _shoot_javelin = new RangedAttackResource();
		[Field( 0x44 )] private RangedAttackResource _black_javelin = new RangedAttackResource();
		[Field( 0x45 )] private AbilityResource _floating = new AbilityResource();
		[Field( 0x46 )] private CombatAbilityResource _dispel_magic = new CombatAbilityResource();
		[Field( 0x47 )] private RangedAttackResource _poison_darts = new RangedAttackResource();
		[Field( 0x48 )] private RangedAttackResource _venomous_spit = new RangedAttackResource();
		[Field( 0x49 )] private AbilityResource _dragon = new AbilityResource();
		[Field( 0x4a )] private AbilityResource _vision = new AbilityResource();
		[Field( 0x4b )] private CombatAbilityResource _self_destruct = new CombatAbilityResource();
		[Field( 0x4c )] private AbilityResource _pass_wall = new AbilityResource();
		[Field( 0x4d )] private AbilityResource _magic_relay = new AbilityResource();
		[Field( 0x4e )] private AbilityResource _path_of_decay = new AbilityResource();
		[Field( 0x4f )] private AbilityResource _path_of_life = new AbilityResource();
		[Field( 0x50 )] private AbilityResource _death_protection = new AbilityResource();
		[Field( 0x51 )] private AbilityResource _fire_protection = new AbilityResource();
		[Field( 0x52 )] private AbilityResource _holy_protection = new AbilityResource();
		[Field( 0x53 )] private AbilityResource _poison_protection = new AbilityResource();
		[Field( 0x54 )] private AbilityResource _lightning_protection = new AbilityResource();
		[Field( 0x55 )] private AbilityResource _magic_protection = new AbilityResource();
		[Field( 0x56 )] private AbilityResource _cold_protection = new AbilityResource();
		[Field( 0x57 )] private AbilityResource _physical_protection = new AbilityResource();
		[Field( 0x60 )] private RangedAttackResource _fire_breath = new RangedAttackResource();
		[Field( 0x61 )] private RangedAttackResource _cold_breath = new RangedAttackResource();
		[Field( 0x62 )] private RangedAttackResource _black_breath = new RangedAttackResource();
		[Field( 0x63 )] private RangedAttackResource _divine_breath = new RangedAttackResource();
		[Field( 0x64 )] private RangedAttackResource _gas_breath = new RangedAttackResource();
		[Field( 0x66 )] private AbilityResource _stunned = new AbilityResource();
		[Field( 0x67 )] private DurationAbilityResource _cursed = new DurationAbilityResource();
		[Field( 0x68 )] private AbilityResource _entangled = new AbilityResource();
		[Field( 0x69 )] private AbilityResource _frozen = new AbilityResource();
		[Field( 0x6a )] private DurationAbilityResource _poisoned = new DurationAbilityResource();
		[Field( 0x6b )] private AbilityResource _webbed = new AbilityResource();
		[Field( 0x6c )] private DurationAbilityResource _vertigo = new DurationAbilityResource();
		[Field( 0x6f )] private DurationAbilityResource _haste_duration = new DurationAbilityResource();
		[Field( 0x74 )] private CombatAbilityResource _possess = new CombatAbilityResource();
		[Field( 0x75 )] private AbilityResource _possessed = new AbilityResource();
		[Field( 0x76 )] private AbilityResource _panicked = new AbilityResource();
		[Field( 0x77 )] private AbilityResource _path_of_frost = new AbilityResource();
		[Field( 0x78 )] private SnowWandererResource _snow_wanderer = new SnowWandererResource();
		[Field( 0x79 )] private AbilityResource _charge = new AbilityResource();
		[Field( 0x7a )] private AbilityResource _dragon_slaying = new AbilityResource();
		[Field( 0x7b )] private AbilityResource _block = new AbilityResource();
		[Field( 0x7c )] private CombatAbilityResource _round_attack = new CombatAbilityResource();
		[Field( 0x7d )] private AbilityResource _extra_strike = new AbilityResource();
		[Field( 0x7e )] private AbilityResource _first_strike = new AbilityResource();
		[Field( 0x7f )] private AbilityResource _build_roads = new AbilityResource();
		[Field( 0x80 )] private AbilityResource _life_stealing = new AbilityResource();
		[Field( 0x81 )] private AbilityResource _entangle_strike = new AbilityResource();
		[Field( 0x82 )] private RangedAttackResource _black_bolts = new RangedAttackResource();
		[Field( 0x83 )] private RangedAttackResource _magic_bolts = new RangedAttackResource();
		[Field( 0x84 )] private RangedAttackResource _frost_bolts = new RangedAttackResource();
		[Field( 0x85 )] private RangedAttackResource _lightning_bolts = new RangedAttackResource();
		[Field( 0x86 )] private RangedAttackResource _holy_bolts = new RangedAttackResource();
		[Field( 0x89 )] private AbilityResource _burning = new AbilityResource();
		[Field( 0x8a )] private AbilityResource _resurrected = new AbilityResource();
		[Field( 0x8b )] private AbilityResource _animated = new AbilityResource();
		[Field( 0x94 )] private AbilityResource _steppe_concealment = new AbilityResource();
		[Field( 0x95 )] private AbilityResource _underground_concealment = new AbilityResource();
		[Field( 0x96 )] private AbilityResource _grass_concealment = new AbilityResource();
		[Field( 0x97 )] private AbilityResource _snow_concealment = new AbilityResource();
		[Field( 0x98 )] private AbilityResource _desert_concealment = new AbilityResource();
		[Field( 0x99 )] private AbilityResource _wasteland_concealment = new AbilityResource();
		[Field( 0x9a )] private AbilityResource _water_concealment = new AbilityResource();
		[Field( 0x9b )] private AbilityResource _concealment = new AbilityResource();
		[Field( 0x9c )] private AbilityResource _holy_champion = new AbilityResource();
		[Field( 0x9d )] private AbilityResource _unholy_champion = new AbilityResource();
		[Field( 0x9f )] private AbilityResource _charmed = new AbilityResource();
		[Field( 0xa0 )] private AbilityResource _dominated = new AbilityResource();
		[Field( 0xa2 )] private EnchantmentAbilityResource _haste = new EnchantmentAbilityResource();
		[Field( 0xa3 )] private EnchantmentAbilityResource _stone_skin = new EnchantmentAbilityResource();
		[Field( 0xa4 )] private EnchantmentAbilityResource _enchanted_weapon = new EnchantmentAbilityResource();
		[Field( 0xa5 )] private AbilityResource _summoned = new AbilityResource();
		[Field( 0xa6 )] private EnchantmentAbilityResource _fury = new EnchantmentAbilityResource();
		[Field( 0xa7 )] private EnchantmentAbilityResource _free_movement = new EnchantmentAbilityResource();
		[Field( 0xa8 )] private EnchantmentAbilityResource _blessed = new EnchantmentAbilityResource();
		[Field( 0xaa )] private EnchantmentAbilityResource _holy_champion_enchantment = new EnchantmentAbilityResource();
		[Field( 0xab )] private EnchantmentAbilityResource _unholy_champion_enchantment = new EnchantmentAbilityResource();
		[Field( 0xac )] private EnchantmentAbilityResource _concealment_enchantment = new EnchantmentAbilityResource();
		[Field( 0xad )] private EnchantmentAbilityResource _fire_halo = new EnchantmentAbilityResource();
		[Field( 0xae )] private EnchantmentAbilityResource _water_walking = new EnchantmentAbilityResource();
		[Field( 0xaf )] private EnchantmentAbilityResource _wind_walking = new EnchantmentAbilityResource();
		[Field( 0xb0 )] private EnchantmentAbilityResource _liquid_form = new EnchantmentAbilityResource();
		[Field( 0xb3 )] private EnchantmentAbilityResource _dark_gift = new EnchantmentAbilityResource();
		[Field( 0xb4 )] private AbilityResource _mounted = new AbilityResource();
		[Field( 0xb5 )] private AbilityResource _animal = new AbilityResource();
		[Field( 0xb6 )] private AbilityResource _undead = new AbilityResource();
		[Field( 0xb7 )] private AbilityResource _build_outpost = new AbilityResource();
		[Field( 0xb8 )] private AbilityResource _fire_domain = new AbilityResource();
		[Field( 0xba )] private AbilityResource _life_domain = new AbilityResource();
		[Field( 0xbe )] private AbilityResource _rebuild_structure = new AbilityResource();
		[Field( 0xbf )] private EnchantmentAbilityResource _static_shield = new EnchantmentAbilityResource();
		[Field( 0xc0 )] private EnchantmentAbilityResource _poison_domain = new EnchantmentAbilityResource();
		[Field( 0xc1 )] private AbilityResource _caravan = new AbilityResource();
		[Field( 0xc2 )] private AbilityResource _shadow_sickness = new AbilityResource();
		[Field( 0xc3 )] private AbilityResource _shadow_walker = new AbilityResource();
		[Field( 0xc4 )] private EnchantmentAbilityResource _shadow_walking_enchantment = new EnchantmentAbilityResource();
		[Field( 0xc5 )] private AbilityResource _dig_adit = new AbilityResource();
		[Field( 0xc6 )] private DurationAbilityResource _shadow_walking_duration = new DurationAbilityResource();
		[Field( 0xc7 )] private EnchantmentAbilityResource _summoners_aura = new EnchantmentAbilityResource();
		[Field( 0xc8 )] private AbilityResource _animated_hero = new AbilityResource();
		[Field( 0xd4 )] private AbilityResource _double_strike = new AbilityResource();
		[Field( 0xd5 )] private AbilityResource _willpower = new AbilityResource();
		[Field( 0xd6 )] private CombatAbilityResource _steam = new CombatAbilityResource();
		[Field( 0xd7 )] private AbilityResource _blurred = new AbilityResource();
		[Field( 0xd8 )] private AbilityResource _physical_weakness = new AbilityResource();
		[Field( 0xd9 )] private AbilityResource _magic_weakness = new AbilityResource();
		[Field( 0xda )] private AbilityResource _fire_weakness = new AbilityResource();
		[Field( 0xdb )] private AbilityResource _cold_weakness = new AbilityResource();
		[Field( 0xdc )] private AbilityResource _death_weakness = new AbilityResource();
		[Field( 0xdd )] private AbilityResource _holy_weakness = new AbilityResource();
		[Field( 0xde )] private AbilityResource _poison_weakness = new AbilityResource();
		[Field( 0xdf )] private AbilityResource _lightning_weakness = new AbilityResource();
		[Field( 0xe0 )] private AbilityResource _wall_crushing = new AbilityResource();
		[Field( 0xe1 )] private CombatAbilityResource _sabotage = new CombatAbilityResource();
		[Field( 0xe2 )] private AbilityResource _energy_drain = new AbilityResource();
		[Field( 0xe3 )] private AbilityResource _energy_drained = new AbilityResource();
		[Field( 0xe4 )] private RangedAttackResource _phase = new RangedAttackResource();
		[Field( 0xe5 )] private CombatAbilityResource _control_animal = new CombatAbilityResource();
		[Field( 0xe6 )] private RangedAttackResource _taunt = new RangedAttackResource();
		[Field( 0xe7 )] private RangedAttackResource _fire_crossbow = new RangedAttackResource();
		[Field( 0xe8 )] private CombatAbilityResource _repair_machine = new CombatAbilityResource();
		[Field( 0xe9 )] private RangedAttackResource _throw_blade = new RangedAttackResource();
		[Field( 0xea )] private RangedAttackResource _fire_bolts = new RangedAttackResource();
		[Field( 0xeb )] private AbilityResource _feral_mount = new AbilityResource();
		[Field( 0xec )] private AbilityResource _controlled = new AbilityResource();
		[Field( 0xed )] private AbilityResource _magical_mount = new AbilityResource();
		[Field( 0xee )] private EnchantmentAbilityResource _weakened = new EnchantmentAbilityResource();
		[Field( 0xef )] private EnchantmentAbilityResource _resurgence_enchantment = new EnchantmentAbilityResource();
		[Field( 0xf0 )] private AbilityResource _swallow_whole = new AbilityResource();
		[Field( 0xf1 )] private AbilityResource _swarmed = new AbilityResource();
		[Field( 0xf2 )] private AbilityResource _taunted = new AbilityResource();
		[Field( 0xf4 )] private AbilityResource _smoky_haze = new AbilityResource();
		[Field( 0xf5 )] private EnchantmentAbilityResource _oily_skin = new EnchantmentAbilityResource();
		[Field( 0xf6 )] private AreaRangedAttackResource _hurl_firebomb = new AreaRangedAttackResource();
		[Field( 0xf7 )] private AbilityResource _resurgence_combat = new AbilityResource();
		[Field( 0xf8 )] private CombatAbilityResource _trap = new CombatAbilityResource();
		[Field( 0xf9 )] private AbilityResource _trapped = new AbilityResource();
		[Field( 0xfa )] private CombatAbilityResource _grasp = new CombatAbilityResource();
		[Field( 0xfd )] private AbilityResource _devour = new AbilityResource();
		[Field( 0xfe )] private RangedAttackResource _spawn_larva = new RangedAttackResource();
		[Field( 0xff )] private AbilityResource _metamorphosis = new AbilityResource();
		[Field( 0x100 )] private CombatAbilityResource _steal_enchantment = new CombatAbilityResource();
		[Field( 0x102 )] private AreaRangedAttackResource _bombard = new AreaRangedAttackResource();
		[Field( 0x103 )] private RangedAttackResource _throw_spear = new RangedAttackResource();
		[Field( 0x104 )] private AreaRangedAttackResource _hurl_lightning = new AreaRangedAttackResource();
		[Field( 0x105 )] private CombatAbilityResource _whirlwind = new CombatAbilityResource();
		[Field( 0x106 )] private AbilityResource _double_gravity = new AbilityResource();
		[Field( 0x107 )] private AbilityResource _holy_light = new AbilityResource();
		[Field( 0x108 )] private AbilityResource _unholy_darkness = new AbilityResource();
		[Field( 0x109 )] private AbilityResource _wind_ward = new AbilityResource();
		[Field( 0x10a )] private AbilityResource _mud = new AbilityResource();
		[Field( 0x10b )] private AbilityResource _confused = new AbilityResource();
		[Field( 0x10c )] private EnchantmentAbilityResource _seeker_enchantment = new EnchantmentAbilityResource();
		[Field( 0x10d )] private AbilityResource _changeling = new AbilityResource();
		[Field( 0x10e )] private RangedAttackResource _morph = new RangedAttackResource();
		[Field( 0x10f )] private AbilityResource _paralyzed = new AbilityResource();
		[Field( 0x110 )] private AbilityResource _enslaved = new AbilityResource();
		[Field( 0x111 )] private CombatAbilityResource _spread_attack = new CombatAbilityResource();
		[Field( 0x112 )] private CombatAbilityResource _strangle = new CombatAbilityResource();
		[Field( 0x113 )] private AbilityResource _infected = new AbilityResource();
		[Field( 0x114 )] private CombatAbilityResource _infect = new CombatAbilityResource();
		[Field( 0x115 )] private CombatAbilityResource _resurrect = new CombatAbilityResource();
		[Field( 0x116 )] private CombatAbilityResource _animate_corpse = new CombatAbilityResource();
		[Field( 0x117 )] private AbilityResource _martyr = new AbilityResource();
		[Field( 0x118 )] private CombatAbilityResource _ram = new CombatAbilityResource();
		[Field( 0x119 )] private RangedAttackResource _pixie_dust = new RangedAttackResource();
		[Field( 0x11a )] private CombatAbilityResource _drain_will = new CombatAbilityResource();
		[Field( 0x11b )] private AbilityResource _will_drained = new AbilityResource();
		[Field( 0x11c )] private RangedAttackResource _frost_blowing = new RangedAttackResource();
		[Field( 0x11d )] private RangedAttackResource _shoot_javelins = new RangedAttackResource();
		[Field( 0x11e )] private AbilityResource _dragon_growth = new AbilityResource();
		[Field( 0x11f )] private EnchantmentAbilityResource _mighty_meek = new EnchantmentAbilityResource();
		[Field( 0x120 )] private AbilityResource _blinded = new AbilityResource();
		[Field( 0x121 )] private AbilityResource _rotting = new AbilityResource();
		[Field( 0x122 )] private AbilityResource _seeker = new AbilityResource();
		[Field( 0x123 )] private AbilityResource _draconian_growth = new AbilityResource();
		[Field( 0x124 )] private AbilityResource _polearm = new AbilityResource();
		[Field( 0x125 )] private EnchantmentAbilityResource _high_prayer = new EnchantmentAbilityResource();
		#endregion

		#endregion
	}
}
