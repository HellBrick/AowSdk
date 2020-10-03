using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace Utils.WPF.Converters
{
	public abstract class ConverterBase<TValue>: MarkupExtension, IValueConverter
	{
		#region Virtual

		protected abstract object Convert( TValue value, Type targetType, object param, System.Globalization.CultureInfo culture );
		protected abstract TValue ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture );

		#endregion

		#region MarkupExtension

		public override object ProvideValue( IServiceProvider serviceProvider ) => this;

		#endregion

		#region IValueConverter Members

		object IValueConverter.Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) => Convert( (TValue) value, targetType, parameter, culture );

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) => ConvertBack( value, targetType, parameter, culture );

		#endregion
	}
}
