using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells
{
	class PestilenceSpellVM : StormSpellVM
	{
		public new PestilenceSpellResource Model
		{
			get => base.Model as PestilenceSpellResource;
			set => base.Model = value;
		}

		public byte DispelResistance
		{
			get => Model.DispelResistance;
			set { Model.DispelResistance = value; RaisePropertyChanged( () => DispelResistance ); }
		}
	}
}
