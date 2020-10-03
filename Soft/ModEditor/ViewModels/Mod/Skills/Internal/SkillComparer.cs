using System.Collections;
using System.Collections.Generic;

namespace ModEditor.ViewModels.Mod.Skills.Internal
{
	class SkillComparer: IComparer<SkillVM>, IComparer
	{
		#region IComparer<SkillVM> Members

		public int Compare( SkillVM x, SkillVM y )
		{
			int result = 0;
			result = y.SkillPoints.CompareTo( x.SkillPoints );

			if ( result == 0 )
				result = x.Name.CompareTo( y.Name );

			return result;
		}

		#endregion

		#region IComparer Members

		int IComparer.Compare( object x, object y ) => Compare( x as SkillVM, y as SkillVM );

		#endregion
	}
}
