using System;
using System.Windows.Markup;

namespace Utils.WPF
{
	public class EnumTypeValues: MarkupExtension
	{
		private Type _type;

		public EnumTypeValues( Type type ) => _type = type;

		public override object ProvideValue( IServiceProvider serviceProvider ) => Enum.GetValues( _type );
	}
}
