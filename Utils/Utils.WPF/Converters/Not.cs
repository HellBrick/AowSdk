using System;

namespace Utils.WPF.Converters
{
	public class Not: ConverterBase<bool>
	{
		protected override object Convert( bool value, Type targetType, object param, System.Globalization.CultureInfo culture ) => !value;

		protected override bool ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) => !( (bool) value );
	}
}
