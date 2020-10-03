using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModEditor.Views.Controls
{
	/// <summary>
	/// Interaction logic for EnumDropdown.xaml
	/// </summary>
	public partial class EnumDropdown : UserControl
	{
		public EnumDropdown() => InitializeComponent();

		#region Dependency properties

		#region Value

		public static DependencyProperty ValueProperty = DependencyProperty.Register(
			"Value",
			typeof( Enum ),
			typeof( EnumDropdown ),
			new FrameworkPropertyMetadata()
			{
				BindsTwoWayByDefault = true,
				DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			} );

		public Enum Value
		{
			get => (Enum) GetValue( ValueProperty );
			set => SetValue( ValueProperty, value );
		}

		#endregion

		#region Type

		public static DependencyProperty TypeProperty = DependencyProperty.Register(
			"Type",
			typeof( Type ),
			typeof( EnumDropdown ) );

		public Type Type
		{
			get => (Type) GetValue( TypeProperty );
			set => SetValue( TypeProperty, value );
		}

		#endregion

		#region Text

		public static DependencyProperty TextProperty = DependencyProperty.Register(
			"Text",
			typeof( string ),
			typeof( EnumDropdown ),
			new FrameworkPropertyMetadata()
			{
				BindsTwoWayByDefault = true,
				DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			} );

		public string Text
		{
			get => (string) GetValue( TextProperty );
			set => SetValue( TextProperty, value );
		}

		#endregion

		#endregion

		private void DropdownClick( object sender, RoutedEventArgs e ) => _spherePopup.IsOpen = !_spherePopup.IsOpen;
	}
}
