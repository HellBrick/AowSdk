namespace Aow2.Modding
{
	internal class ModDataStruct : IModData
	{
		public Animations Main { get; set; }

		public int ID { get; set; }

		public MPE.MpeSettings Mpe { get; set; }

		public Units.UnitResourceList Units { get; set; }

		public Units.UnitModelList UnitModels { get; set; }

		public Abilities.AbilityResourceList Abilities { get; set; }

		public Items.ItemLibrary Items { get; set; }

		public RaceResourceList Races { get; set; }

		public Skills.SkillResourceList Skills { get; set; }

		public Spells.SpellResourceList Spells { get; set; }
	}
}
