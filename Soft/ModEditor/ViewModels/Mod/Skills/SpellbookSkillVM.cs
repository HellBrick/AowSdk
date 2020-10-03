using Aow2.Modding.Skills;

namespace ModEditor.ViewModels.Mod.Skills
{
	public class SpellbookSkillVM: SkillVM
	{
		public new SpellbookSkillResource Model
		{
			get => base.Model as SpellbookSkillResource;
			set => base.Model = value;
		}

		public float ResearchMod
		{
			get => Model.ResearchMod;
			set { Model.ResearchMod = value; RaisePropertyChanged( () => ResearchMod ); }
		}

		public float ManaMod
		{
			get => Model.ManaMod;
			set { Model.ManaMod = value; RaisePropertyChanged( () => ManaMod ); }
		}

		public float ChanceMod
		{
			get => Model.ChanceMod;
			set { Model.ChanceMod = value; RaisePropertyChanged( () => ChanceMod ); }
		}
	}
}
