using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aow.Modding.Abilities;

namespace ModEditor.ViewModels.Mod.Abilities.Internal
{
	static class AbilityViewModelFactory
	{
		private static Dictionary<Type, Func<AbilityResourceVM>> _factoryMethods = new Dictionary<Type, Func<AbilityResourceVM>>()
		{
			{ typeof( AbilityResource ), () => new AbilityResourceVM() },
			{ typeof( SnowWandererResource ), () => new AbilityResourceVM() },
			{ typeof( DurationAbilityResource ), () => new DurationAbilityResourceVM() },
			{ typeof( CombatAbilityResource ), () => new CombatAbilityResourceVM() },
			{ typeof( RangedAttackResource ), () => new RangedAbilityResourceVM() },
			{ typeof( AreaRangedAttackResource ), () => new AreaRangedAbilityVM() },
			{ typeof( EnchantmentAbilityResource ), () => new EnchantmentAbilityResourceVM() }
		};

		public static AbilityResourceVM Create( AbilityResource model )
		{
			Func<AbilityResourceVM> factoryMethod;
			if ( !_factoryMethods.TryGetValue( model.GetType(), out factoryMethod ) )
				throw new NotSupportedException( String.Format( "{0} is not supported.", model.GetType().Name ) );

			AbilityResourceVM viewModel = factoryMethod();
			viewModel.Model = model;
			return viewModel;
		}
	}
}
