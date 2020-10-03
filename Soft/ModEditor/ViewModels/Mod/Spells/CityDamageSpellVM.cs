using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells
{
	class CityDamageSpellVM : SpellVM
	{
		public new CityDamageSpellResource Model
		{
			get => base.Model as CityDamageSpellResource;
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
