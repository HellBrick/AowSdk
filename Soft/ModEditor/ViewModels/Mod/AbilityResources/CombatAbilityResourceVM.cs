using Aow2;
using Aow2.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	class CombatAbilityResourceVM : AbilityResourceVM
	{
		public new CombatAbilityResource Model
		{
			get => base.Model as CombatAbilityResource;
			set => base.Model = value;
		}

		public byte Attack
		{
			get => Model.Attack;
			set { Model.Attack = value; RaisePropertyChanged( () => Attack ); }
		}

		public byte Damage
		{
			get => Model.Damage;
			set { Model.Damage = value; RaisePropertyChanged( () => Damage ); }
		}

		public DamageType DamageType
		{
			get => Model.DamageType;
			set { Model.DamageType = value; RaisePropertyChanged( () => DamageType ); }
		}
	}
}
