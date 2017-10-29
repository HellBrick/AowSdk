namespace Aow2.Modding.Skills
{
	[AowClass( ID = 0x11cb001 )]
	public class MultiplierSkillResource : SkillResource
	{
		public MultiplierSkillResource() => Multiplier = 1.0f;

		[Field( 0x9f )] public float Multiplier { get; set; }
	}
}
