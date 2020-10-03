using System;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.Skills.Dependencies;

namespace ModEditor.ViewModels.Mod.Skills
{
	/// <summary>
	/// View model for skill - check pair (used for editing requirements / exclusions).
	/// </summary>
	class SkillCkeckVM: NotificationObject
	{
		private IExclusionCheck _core;

		public SkillCkeckVM( IExclusionCheck core )
		{
			_core = core;
			_core.IsCheckedChanged += OnCoreCheckedChanged;
		}

		public SkillVM Owner { get; set; }
		public SkillVM Skill { get; set; }
		
		public bool IsChecked
		{
			get => _core.IsChecked;
			set { _core.SetChecked( value, Owner ); RaisePropertyChanged( () => IsChecked ); }
		}

		private void OnCoreCheckedChanged( SkillVM sender, EventArgs e )
		{
			/*
			 * If (sender == owner) we caught our own event: this is the check that was clicked by user.
			 * Otherwise, models were updated and all we need to do is to notify UI that property was changed.
			 * */

			if ( sender != Owner )
				RaisePropertyChanged( () => IsChecked );
		}

		public override string ToString()
		{
			if ( IsChecked )
				return "+ " + Skill.Name;
			else
				return Skill.Name;
		}
	}
}
