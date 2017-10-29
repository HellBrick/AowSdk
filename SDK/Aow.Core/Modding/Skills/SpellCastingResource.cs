namespace Aow2.Modding.Skills
{
	[AowClass( ID = 0x11cb004 )]
	public class SpellCastingResource: SkillResource
	{
		[Field(0x28)] public int Level2Research { get; set; }
		[Field(0x29)] public int Level3Research { get; set; }
		[Field(0x2a)] public int Level4Research { get; set; }
		[Field(0x2b)] public int Level5Research { get; set; }
	}
}
