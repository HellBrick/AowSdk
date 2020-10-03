using System.Collections.Generic;

namespace ModEditor.ViewModels.Mod.Skills.Dependencies
{
	/// <summary>
	/// Used to simplify SkillVM -> SkillTabVM dependency.
	/// </summary>
	interface ISkillListPrivider
	{
		IList<SkillVM> Skills { get; }
		ExclusionGrid Exclusions { get; }
	}
}
