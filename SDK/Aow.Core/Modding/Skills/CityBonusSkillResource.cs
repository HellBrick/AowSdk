namespace Aow2.Modding.Skills
{
	[AowClass( ID = 0x11cb005 )]
	public class CityBonusSkillResource : SkillResource
	{
		[Field( 0xA0 )] public int OutpostBonus { get; set; }
		[Field( 0xA4 )] public int VillageBonus { get; set; }
		[Field( 0xA8 )] public int TownBonus { get; set; }
		[Field( 0xAC )] public int CityBonus { get; set; }
	}
}
