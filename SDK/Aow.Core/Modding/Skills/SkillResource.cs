using Aow2.Collections;
using Aow2.Modding.Collections;

namespace Aow2.Modding.Skills
{
	[AowClass( ID = 0x11cb000 )]
	public class SkillResource : IResourceItem
	{
		public SkillResource()
		{
			ExcludedSkillIDs = new IntegerList();
			RequiredSkillIDs = new IntegerList();
		}

		[Field( 0x16 )] public sbyte SkillPoints { get; set; }
		[Field( 0x1a )] public int ResearchPoints { get; set; }

		[Field( 0x15 )] public IntegerList ExcludedSkillIDs { get; protected set; }
		[Field( 0x17 )] public IntegerList RequiredSkillIDs { get; protected set; }

		[Field( 0x14 )]
		protected LongPascalString _description = "";
		public string Description
		{
			get => _description;
			set => _description = value;
		}

		[Field( 0x90 )]
		protected ShortPascalString _name = "";
		public string Name
		{
			get => _name;
			set => _name = value;
		}

		[Field( 0x91 )]
		public int ID { get; set; }

		[Field( 0x93 )] public bool IsAllowedOnStart { get; set; }
		[Field( 0x94 )] public bool IsAllowedToResearch { get; set; }

		/// <summary>
		/// If set to true, specifies that the skill is a level-up and can be picked as a 2nd negative skill.
		/// </summary>
		[Field( 0x95 )]
		public bool IsLevelUp { get; set; }

		[Field( 0x80 )]
		public SkillEffect Effect { get; set; }

		public void MutualExclude( SkillResource excludedSkill )
		{
			if ( !ExcludedSkillIDs.Contains( excludedSkill.ID ) )
				ExcludedSkillIDs.Add( excludedSkill.ID );

			if ( !excludedSkill.ExcludedSkillIDs.Contains( ID ) )
				excludedSkill.ExcludedSkillIDs.Add( ID );
		}

		public void MutualUnexclude( SkillResource unexcludedSkill )
		{
			if ( ExcludedSkillIDs.Contains( unexcludedSkill.ID ) )
				ExcludedSkillIDs.Remove( unexcludedSkill.ID );

			if ( unexcludedSkill.ExcludedSkillIDs.Contains( ID ) )
				unexcludedSkill.ExcludedSkillIDs.Remove( ID );
		}

		[Field( 0x0a )] public int u_0a;
		[Field( 0x0b )] public int u_0b;
		[Field( 0x0c )] public int u_0c;

		[Field( 0x19 )] public AowList<ImageSequence> Icon { get; set; }

		public override string ToString() => Name;
	}
}
