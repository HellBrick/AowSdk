using System.Collections;
using System.Collections.Generic;

namespace ModEditor.ViewModels.Mod.Spells
{
	class SpellComparer: IComparer<SpellVM>, IComparer
	{
		#region IComparer<SpellVM> Members

		public int Compare( SpellVM x, SpellVM y )
		{
			int result = x.Sphere.CompareTo( y.Sphere );
			if ( result != 0 )
				return result;

			result = x.Level.CompareTo( y.Level );
			if ( result != 0 )
				return result;

			return x.Name.CompareTo( y.Name );
		}

		#endregion

		#region IComparer Members

		int IComparer.Compare( object x, object y ) => Compare( x as SpellVM, y as SpellVM );

		#endregion
	}

	class SpellCheckComparer: IComparer<SpellCheckVM>, IComparer
	{
		private SpellComparer _innerComparer = new SpellComparer();

		#region IComparer<SpellCheckVM> Members

		public int Compare( SpellCheckVM x, SpellCheckVM y ) => _innerComparer.Compare( x.Spell, y.Spell );

		#endregion

		#region IComparer Members

		public int Compare( object x, object y ) => Compare( x as SpellCheckVM, y as SpellCheckVM );

		#endregion
	}
}
