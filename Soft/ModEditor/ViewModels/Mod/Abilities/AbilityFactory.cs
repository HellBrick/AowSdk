using System;
using System.Collections.Generic;
using Aow2;
using Aow2.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.Abilities
{
	internal static class AbilityFactory
	{
		private static Dictionary<AbilityID, Func<Ability>> _specialFactories = new Dictionary<AbilityID, Func<Ability>>()
		{
			//{ typeof( Ability ), a => new AbilityVM() }
		};

		public static Ability Create( AbilityResource resource )
		{
			Func<Ability> factoryMethod;
			if ( !_specialFactories.TryGetValue( resource.ID, out factoryMethod ) )
				factoryMethod = () => new Ability();

			Ability newAbility = factoryMethod();
			
			newAbility.AbilityResourceIndex = (int) resource.ID;
			newAbility.Level = 1;
			return newAbility;
		}
	}
}
