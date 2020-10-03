using System;
using System.ComponentModel;

namespace Utils.WPF.Models
{
	class DelegateGroupDescription<T>: GroupDescription
	{
		private Func<T, object> _groupGetter;

		public DelegateGroupDescription( Func<T, object> groupGetter ) => _groupGetter = groupGetter;

		public override object GroupNameFromItem( object item, int level, System.Globalization.CultureInfo culture )
		{
			T elem = ( T )item;
			return _groupGetter( elem );
		}
	}
}
