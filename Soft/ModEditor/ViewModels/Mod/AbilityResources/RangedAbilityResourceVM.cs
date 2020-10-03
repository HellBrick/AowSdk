using Aow2.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	class RangedAbilityResourceVM : CombatAbilityResourceVM
	{
		public new RangedAttackResource Model
		{
			get => base.Model as RangedAttackResource;
			set => base.Model = value;
		}

		public int Shots
		{
			get => Model.Shots;
			set { Model.Shots = value; RaisePropertyChanged( () => Shots ); }
		}

		public ShootRange Range
		{
			get => Model.Range;
			set { Model.Range = value; RaisePropertyChanged( () => Range ); }
		}
	}
}
