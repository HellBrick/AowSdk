using Aow2.Items;
using Aow2.Modding.Abilities;
using Aow2.Modding.MPE;
using Aow2.Modding.Skills;
using Aow2.Modding.Spells;
using Aow2.Modding.Units;

namespace Aow2.Modding
{
	public interface IModData
	{
		Animations Main { get; set; }
		int ID { get; set; }
		MpeSettings Mpe { get; set; }
		UnitResourceList Units { get; set; }
		UnitModelList UnitModels { get; set; }
		AbilityResourceList Abilities { get; set; }
		ItemLibrary Items { get; set; }
		RaceResourceList Races { get; set; }
		SkillResourceList Skills { get; set; }
		SpellResourceList Spells { get; set; }
	}
}
