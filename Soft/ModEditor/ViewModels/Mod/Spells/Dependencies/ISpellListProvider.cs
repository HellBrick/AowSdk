using System.Collections.Generic;
using Aow2.Modding.MPE;

namespace ModEditor.ViewModels.Mod.Spells.Dependencies
{
	interface ISpellListProvider
	{
		IList<SpellVM> SpellList { get; }
		SpellGraph SpellGraph { get; }
	}
}
