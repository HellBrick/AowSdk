using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells
{
	class AreaCombatSpellVM : CombatSpellVM
	{
		public new AreaCombatSpellResource Model
		{
			get => base.Model as AreaCombatSpellResource;
			set => base.Model = value;
		}

		public int Radius
		{
			get => Model.Radius;
			set => Model.Radius = value;
		}
	}
}
