using Aow2.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	class DurationAbilityResourceVM : AbilityResourceVM
	{
		public new DurationAbilityResource Model
		{
			get => base.Model as DurationAbilityResource;
			set => base.Model = value;
		}

		public byte Duration
		{
			get => Model.Duration;
			set { Model.Duration = value; RaisePropertyChanged( () => Duration ); }
		}
	}
}
