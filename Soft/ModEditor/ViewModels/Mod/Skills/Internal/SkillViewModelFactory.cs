using System;
using System.Collections.Generic;
using Aow2.Modding.Skills;

namespace ModEditor.ViewModels.Mod.Skills.Internal
{
	static class SkillViewModelFactory
	{
		private static readonly Dictionary<Type, Func<SkillVM>> _creators = new Dictionary<Type, Func<SkillVM>>()
		{
			{ typeof( SkillResource ), () => new SkillVM() },
			{ typeof( BonusSkillResource ), () => new BonusSkillVM() },
			{ typeof( MultiplierSkillResource ), () => new MultiplierSkillVM() },
			{ typeof( CityBonusSkillResource ), () => new CitySkillVM() },
			{ typeof( SpellbookSkillResource ), () => new SpellbookSkillVM() },
			{ typeof( SpellCastingResource ), () => new SpellCastingSkillVM() }
		};

		public static SkillVM Create( SkillResource resource )
		{
			Func<SkillVM> creator;
			if ( !_creators.TryGetValue( resource.GetType(), out creator ) )
				throw new NotSupportedException( String.Format( "{0} is not supported.", resource.GetType().Name ) );

			SkillVM viewModel = creator();
			viewModel.Model = resource;
			return viewModel;
		}
	}
}
