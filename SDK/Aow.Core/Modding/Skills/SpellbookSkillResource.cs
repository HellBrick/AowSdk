namespace Aow2.Modding.Skills
{
	[AowClass( ID = 0x11cb003 )]
	public class SpellbookSkillResource: SkillResource
	{
		public SpellbookSkillResource()
		{
			ResearchMod = 0.8f;
			ManaMod = 0.8f;
			ChanceMod = 1.5f;
		}

		[Field(0x9f)] public float ResearchMod { get; set; }
		[Field(0xa0)] public float ManaMod { get; set; }
		[Field(0xa1)] public float ChanceMod { get; set; }
	}
}
