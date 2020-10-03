using Aow2.Modding.Skills;

namespace ModEditor.ViewModels.Mod.Skills
{
	public class SpellCastingSkillVM: SkillVM
	{
		public new SpellCastingResource Model
		{
			get => base.Model as SpellCastingResource;
			set => base.Model = value;
		}

		public int Level2Research
		{
			get => Model.Level2Research;
			set { Model.Level2Research = value; RaisePropertyChanged( () => Level2Research ); }
		}

		public int Level3Research
		{
			get => Model.Level3Research;
			set { Model.Level3Research = value; RaisePropertyChanged( () => Level3Research ); }
		}

		public int Level4Research
		{
			get => Model.Level4Research;
			set { Model.Level4Research = value; RaisePropertyChanged( () => Level4Research ); }
		}

		public int Level5Research
		{
			get => Model.Level5Research;
			set { Model.Level5Research = value; RaisePropertyChanged( () => Level5Research ); }
		}
	}
}
