using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells
{
	public class AddObjectsSpellVM: SpellVM
	{
		public new AddObjectsSpellResource Model
		{
			get => base.Model as AddObjectsSpellResource;
			set => base.Model = value;
		}

		public int Radius
		{
			get => Model.Radius;
			set { Model.Radius = value; RaisePropertyChanged( () => Radius ); }
		}
	}
}
