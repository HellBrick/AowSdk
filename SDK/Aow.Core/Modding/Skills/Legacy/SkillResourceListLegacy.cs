namespace Aow2.Modding.Skills.Legacy
{
	[AowClass]
	public class SkillResourceListLegacy
	{
		[Field( 0x15 )] public BonusSkillResource PeaceKeeper { get; set; }
		[Field( 0x17 )] public BonusSkillResource Explorer { get; set; }
		[Field( 0x18 )] public CityBonusSkillResource Expander { get; set; }
		[Field( 0x19 )] public MultiplierSkillResource Merchant { get; set; }
		[Field( 0x1a )] public CityBonusSkillResource Constructor { get; set; }
		[Field( 0x1b )] public MultiplierSkillResource Conqueror { get; set; }
		[Field( 0x1d )] public SkillResource Survivalist { get; set; }
		[Field( 0x1e )] public MultiplierSkillResource Scholar { get; set; }
		[Field( 0x1f )] public MultiplierSkillResource Channeler { get; set; }
		[Field( 0x20 )] public BonusSkillResource Anarchist { get; set; }
		[Field( 0x21 )] public SkillResource Decadence { get; set; }
		[Field( 0x22 )] public MultiplierSkillResource Pacifist { get; set; }
		[Field( 0x23 )] public MultiplierSkillResource Bureaucrat { get; set; }
		[Field( 0x25 )] public CityBonusSkillResource Technophobe { get; set; }
		[Field( 0x26 )] public SpellbookSkillResource Summoner { get; set; }
		[Field( 0x27 )] public SpellCastingResource SpellCasting { get; set; }
		[Field( 0x28 )] public SpellbookSkillResource Enchanter { get; set; }
		[Field( 0x29 )] public SpellbookSkillResource WarMage { get; set; }
	}
}
