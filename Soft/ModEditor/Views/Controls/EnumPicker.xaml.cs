using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModEditor.Views.Controls
{
	/// <summary>
	/// Interaction logic for EnumPicker.xaml
	/// </summary>
	public partial class EnumPicker : UserControl
	{
		public EnumPicker() => InitializeComponent();

		#region Dependency properties

		#region Value

		public static DependencyProperty ValueProperty = DependencyProperty.Register(
			"Value",
			typeof( Enum ),
			typeof( EnumPicker ),
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
			typeof( EnumPicker ),
			new FrameworkPropertyMetadata()
			{
				PropertyChangedCallback = OnTypeChanged
			} );

		public Type Type
		{
			get => (Type) GetValue( TypeProperty );
			set => SetValue( TypeProperty, value );
		}

		private static void OnTypeChanged( DependencyObject obj, DependencyPropertyChangedEventArgs args )
		{
			EnumPicker control = obj as EnumPicker;
			Type newType = args.NewValue as Type;
			List<Enum> newValues = Enum.GetValues( newType ).OfType<Enum>().ToList();

			control.SetValue( _itemsPropertyKey, newValues );
		}

		#endregion

		#region Items

		private static DependencyPropertyKey _itemsPropertyKey = DependencyProperty.RegisterReadOnly(
				"Items",
				typeof( List<Enum> ),
				typeof( EnumPicker ),
				new FrameworkPropertyMetadata() );

		public static DependencyProperty ItemsProperty = _itemsPropertyKey.DependencyProperty;
		public List<Enum> Items => (List<Enum>) GetValue( ItemsProperty );

		#endregion

		#endregion
	}
}
