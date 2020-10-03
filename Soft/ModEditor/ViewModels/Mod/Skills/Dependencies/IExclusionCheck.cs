using System;

namespace ModEditor.ViewModels.Mod.Skills.Dependencies
{
	interface IExclusionCheck
	{
		bool IsChecked { get; }
		void SetChecked( bool value, SkillVM senderCheckerOwner );
		event EventHandler<SkillVM, EventArgs> IsCheckedChanged;
	}
}
