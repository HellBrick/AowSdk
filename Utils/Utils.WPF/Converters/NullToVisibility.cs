using System;
using System.Windows;

namespace Utils.WPF.Converters
{
	public class NullToVisibility: ConverterBase<object>
	{
		public NullToVisibility()
		{
			IfNull = Visibility.Collapsed;
			IfNotNull = Visibility.Visible;
		}

		public Visibility IfNull { get; set; }
		public Visibility IfNotNull { get; set; }

		protected override object Convert( object value, Type targetType, object param, System.Globalization.CultureInfo culture )
		{
			if ( value == null )
				return IfNull;
			else
				return IfNotNull;
		}

		protected override object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) => throw new NotImplementedException();
	}
}
