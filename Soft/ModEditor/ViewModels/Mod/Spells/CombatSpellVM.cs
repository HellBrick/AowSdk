using Aow2;
using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells
{
	class CombatSpellVM : SpellVM
	{
		public new CombatSpellResource Model
		{
			get => base.Model as CombatSpellResource;
			set => base.Model = value;
		}

		public int Attack
		{
			get => Model.Attack;
			set { Model.Attack = value; RaisePropertyChanged( () => Attack ); }
		}

		public int Damage
		{
			get => Model.Damage;
			set { Model.Damage = value; RaisePropertyChanged( () => Damage ); }
		}

		public int Hits
		{
			get => Model.Hits;
			set { Model.Hits = value; RaisePropertyChanged( () => Hits ); }
		}

		public DamageType DamageType
		{
			get => Model.DamageType;
			set { Model.DamageType = value; RaisePropertyChanged( () => DamageType ); }
		}
	}
}
