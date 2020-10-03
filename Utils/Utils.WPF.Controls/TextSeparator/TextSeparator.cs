using System.Windows;
using System.Windows.Controls;

namespace Utils.WPF.Controls
{
	public class TextSeparator : Control
	{
		static TextSeparator() => DefaultStyleKeyProperty.OverrideMetadata( typeof( TextSeparator ), new FrameworkPropertyMetadata( typeof( TextSeparator ) ) );

		public static DependencyProperty TextProperty = DependencyProperty.Register( "Text", typeof( string ), typeof( TextSeparator ) );
		public string Text
		{
			get => (string) GetValue( TextProperty );
			set => SetValue( TextProperty, value );
		}

		public static DependencyProperty TextStyleProperty = DependencyProperty.Register( "TextStyle", typeof( Style ), typeof( TextSeparator ) );
		public Style TextStyle
		{
			get => (Style) GetValue( TextStyleProperty );
			set => SetValue( TextStyleProperty, value );
		}

		public static DependencyProperty LineStyleProperty = DependencyProperty.Register( "LineStyle", typeof( Style ), typeof( TextSeparator ) );
		public Style LineStyle
		{
			get => (Style) GetValue( LineStyleProperty );
			set => SetValue( LineStyleProperty, value );
		}
	}
}
