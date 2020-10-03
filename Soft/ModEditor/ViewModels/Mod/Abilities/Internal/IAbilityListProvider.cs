using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModEditor.ViewModels.Mod.Abilities.Internal
{
	interface IAbilityListProvider
	{
		IList<AbilityResourceVM> AbilityList { get; }
	}
}
