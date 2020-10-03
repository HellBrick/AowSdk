using Aow2.Modding.Skills;

namespace ModEditor.ViewModels.Mod.Skills
{
	public class MultiplierSkillVM: SkillVM
	{
		public new MultiplierSkillResource Model
		{
			get => base.Model as MultiplierSkillResource;
			set => base.Model = value;
		}

		public float Multiplier
		{
			get => Model.Multiplier;
			set { Model.Multiplier = value; RaisePropertyChanged( () => Multiplier ); }
		}
	}
}
