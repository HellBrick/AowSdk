using System.Collections;
using System.Collections.Generic;

namespace Utils
{
	class ChainComparer<T>: IComparer<T>, IComparer
	{
		private readonly List<IComparer<T>> _comparers;

		public ChainComparer() => _comparers = new List<IComparer<T>>();

		public ChainComparer( params IComparer<T>[] comparers ) => _comparers = new List<IComparer<T>>( comparers );

		public void AddComparer( IComparer<T> comparer ) => _comparers.Add( comparer );

		#region IComparer<T> Members

		public int Compare( T x, T y )
		{
			foreach ( IComparer<T> comp in _comparers )
			{
				int result = comp.Compare( x, y );
				if ( result != 0 )
					return result;
			}

			return 0;
		}

		#endregion

		#region IComparer Members

		int IComparer.Compare( object x, object y ) => Compare( (T) x, (T) y );

		#endregion
	}
}
