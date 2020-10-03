using System;
using System.Collections.Generic;
using Aow2;

namespace ModEditor.ViewModels.Mod.Abilities
{
	static class AbilityViewModelFactory
	{
		private static Dictionary<Type, Func<Ability, AbilityVM>> _specialFactories = new Dictionary<Type, Func<Ability, AbilityVM>>()
		{
			//{ typeof( Ability ), a => new AbilityVM() }
		};

		public static AbilityVM Create( Ability model )
		{
			Func<Ability, AbilityVM> factoryMethod;
			if ( _specialFactories.TryGetValue( model.GetType(), out factoryMethod ) )
				return factoryMethod( model );
			else
				return new AbilityVM() { Model = model };
		}
	}
}
