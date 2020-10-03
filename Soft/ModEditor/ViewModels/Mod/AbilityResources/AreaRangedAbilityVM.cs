using Aow2.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	class AreaRangedAbilityVM : RangedAbilityResourceVM
	{
		public new AreaRangedAttackResource Model
		{
			get => base.Model as AreaRangedAttackResource;
			set => base.Model = value;
		}

		public int Radius
		{
			get => Model.Radius;
			set { Model.Radius = value; RaisePropertyChanged( () => Radius ); }
		}
	}
}
