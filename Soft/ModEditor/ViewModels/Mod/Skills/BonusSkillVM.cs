using Aow2.Modding.Skills;

namespace ModEditor.ViewModels.Mod.Skills
{
	public class BonusSkillVM: SkillVM
	{
		public new BonusSkillResource Model
		{
			get => base.Model as BonusSkillResource;
			set => base.Model = value;
		}

		public int Bonus
		{
			get => Model.Bonus;
			set { Model.Bonus = value; RaisePropertyChanged( () => Bonus ); }
		}
	}
}
