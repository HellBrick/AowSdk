using Aow2.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	class EnchantmentAbilityResourceVM: AbilityResourceVM
	{
		public new EnchantmentAbilityResource Model
		{
			get => base.Model as EnchantmentAbilityResource;
			set => base.Model = value;
		}

		public sbyte AttackBonus
		{
			get => Model.AttackBonus;
			set { Model.AttackBonus = value; RaisePropertyChanged( () => AttackBonus ); }
		}

		public sbyte DamageBonus
		{
			get => Model.DamageBonus;
			set { Model.DamageBonus = value; RaisePropertyChanged( () => DamageBonus ); }
		}

		public sbyte DefenseBonus
		{
			get => Model.DefenseBonus;
			set { Model.DefenseBonus = value; RaisePropertyChanged( () => DefenseBonus ); }
		}

		public sbyte ResistanceBonus
		{
			get => Model.ResistanceBonus;
			set { Model.ResistanceBonus = value; RaisePropertyChanged( () => ResistanceBonus ); }
		}
	}
}
