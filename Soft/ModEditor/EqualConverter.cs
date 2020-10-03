using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace ModEditor
{
	public class EqualConverter: MarkupExtension, IMultiValueConverter
	{
		#region IMultiValueConverter members

		public object Convert( object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture )
		{
			object first = values.FirstOrDefault();
			if ( first == null )
				return false;

			foreach ( object obj in values.Skip( 1 ) )
			{
				if ( !first.Equals( obj ) )
					return false;
			}

			return true;
		}

		public object[] ConvertBack( object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture ) => throw new NotImplementedException();

		#endregion

		public override object ProvideValue( IServiceProvider serviceProvider ) => this;
	}
}
