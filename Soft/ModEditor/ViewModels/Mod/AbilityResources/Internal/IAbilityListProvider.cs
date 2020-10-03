using System.Collections.Generic;

namespace ModEditor.ViewModels.Mod.AbilityResources.Internal
{
	interface IAbilityListProvider
	{
		IList<AbilityResourceVM> AbilityList { get; }
	}
}
