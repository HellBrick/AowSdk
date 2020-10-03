using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells
{
	class StormSpellVM : SpellVM
	{
		public new StormSpellResource Model
		{
			get => base.Model as StormSpellResource;
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
	}
}
