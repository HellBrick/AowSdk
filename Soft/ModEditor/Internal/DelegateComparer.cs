using System;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
	class DelegateComparer<T> : IComparer<T>, IComparer
	{
		private Func<T, T, int> _comparer;

		public DelegateComparer( Func<T, T, int> compareDelegate )
		{
			if ( compareDelegate == null )
				throw new ArgumentNullException( "compareDelegate" );

			_comparer = compareDelegate;
		}

		#region IComparer<T> Members

		public int Compare( T x, T y ) => _comparer( x, y );

		#endregion

		#region IComparer Members

		int IComparer.Compare( object x, object y ) => Compare( (T) x, (T) y );

		#endregion
	}
}
