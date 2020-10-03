using Aow2.Modding.Skills;

namespace ModEditor.ViewModels.Mod.Skills
{
	public class CitySkillVM: SkillVM
	{
		public new CityBonusSkillResource Model
		{
			get => base.Model as CityBonusSkillResource;
			set => base.Model = value;
		}

		public int OutpostBonus
		{
			get => Model.OutpostBonus;
			set { Model.OutpostBonus = value; RaisePropertyChanged( () => OutpostBonus ); }
		}

		public int VillageBonus
		{
			get => Model.VillageBonus;
			set { Model.VillageBonus = value; RaisePropertyChanged( () => VillageBonus ); }
		}

		public int TownBonus
		{
			get => Model.TownBonus;
			set { Model.TownBonus = value; RaisePropertyChanged( () => TownBonus ); }
		}

		public int CityBonus
		{
			get => Model.CityBonus;
			set { Model.CityBonus = value; RaisePropertyChanged( () => CityBonus ); }
		}
	}
}
