using System;
using System.Collections.Generic;
using Aow2.Modding.Spells;

namespace ModEditor.ViewModels.Mod.Spells.Internal
{
	class SpellViewModelFactory
	{
		private static Dictionary<Type, Func<SpellVM>> _factoryMethods = new Dictionary<Type, Func<SpellVM>>()
		{
			{ typeof( SpellResource ), () => new SpellVM() },
			{ typeof( StormSpellResource ), () => new StormSpellVM() },
			{ typeof( PestilenceSpellResource ), () => new PestilenceSpellVM() },
			{ typeof( AddObjectsSpellResource ), () => new AddObjectsSpellVM() },
			{ typeof( CityDamageSpellResource ), () => new CityDamageSpellVM() },
			{ typeof( CombatSpellResource ), () => new CombatSpellVM() },
			{ typeof( AreaCombatSpellResource ), () => new AreaCombatSpellVM() },
			{ typeof( DispelSpellResource ), () => new SpellVM() },
			{ typeof( DomainSpellResource ), () => new SpellVM() },
			{ typeof( SummonSpellResource ), () => new SpellVM() }
		};

		public static SpellVM Create( SpellResource model )
		{
			Func<SpellVM> factoryMethod;
			if ( !_factoryMethods.TryGetValue( model.GetType(), out factoryMethod ) )
				throw new NotSupportedException( String.Format( "{0} is not supported.", model.GetType().Name ) );

			SpellVM viewModel = factoryMethod();
			viewModel.Model = model;
			return viewModel;
		}
	}
}
