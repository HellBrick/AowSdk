using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace Utils.WPF.Controls
{
	public class EnumEditor : Control
	{
		static EnumEditor() => DefaultStyleKeyProperty.OverrideMetadata( typeof( EnumEditor ), new FrameworkPropertyMetadata( typeof( EnumEditor ) ) );

		#region Dependency properties

		#region Value

		public static DependencyProperty ValueProperty = DependencyProperty.Register(
			"Value",
			typeof( Enum ),
			typeof( EnumEditor ),
			new FrameworkPropertyMetadata()
			{
				BindsTwoWayByDefault = true,
				DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				PropertyChangedCallback = OnValueChanged
			} );

		[Category( "Enum" )]
		public Enum Value
		{
			get => (Enum) GetValue( ValueProperty );
			set => SetValue( ValueProperty, value );
		}

		private static void OnValueChanged( DependencyObject obj, DependencyPropertyChangedEventArgs args )
		{
			EnumEditor editor = obj as EnumEditor;
			editor.SetFlags( args.NewValue as Enum );
		}

		#endregion

		#region Type

		public static DependencyProperty TypeProperty = DependencyProperty.Register(
			"Type",
			typeof( Type ),
			typeof( EnumEditor ),
			new FrameworkPropertyMetadata()
			{
				PropertyChangedCallback = OnTypeChanged
			} );

		[Category( "Enum" )]
		public Type Type
		{
			get => (Type) GetValue( TypeProperty );
			set => SetValue( TypeProperty, value );
		}

		private static void OnTypeChanged( DependencyObject obj, DependencyPropertyChangedEventArgs args )
		{
			EnumEditor editor = obj as EnumEditor;
			List<EnumFlagItem> items = editor.ExtractFlags();
			editor.SetValue( _itemsPropertyKey, items );
			editor.Value = Enum.ToObject( editor.Type, 0L ) as Enum;
		}

		#endregion

		#region Flags

		public static DependencyProperty FlagsProperty = DependencyProperty.Register( "Flags", typeof( List<Enum> ), typeof( EnumEditor ) );
		
		[Category( "Enum" )]
		public List<Enum> Flags
		{
			get => (List<Enum>) GetValue( FlagsProperty );
			set => SetValue( FlagsProperty, value );
		}

		#endregion

		#region Items

		private static DependencyPropertyKey _itemsPropertyKey = DependencyProperty.RegisterReadOnly(
				"Items",
				typeof( List<EnumFlagItem> ),
				typeof( EnumEditor ),
				new FrameworkPropertyMetadata() );

		public static DependencyProperty ItemsProperty = _itemsPropertyKey.DependencyProperty;
		public List<EnumFlagItem> Items => (List<EnumFlagItem>) GetValue( ItemsProperty );

		#endregion

		#region ItemTemplate

		public static DependencyProperty ItemTemplateProperty = DependencyProperty.Register( "ItemTemplate", typeof( DataTemplate ), typeof( EnumEditor ) );

		[Category( "Layout" )]
		public DataTemplate ItemTemplate
		{
			get => (DataTemplate) GetValue( ItemTemplateProperty );
			set => SetValue( ItemTemplateProperty, value );
		}

		#endregion

		#endregion

		private List<EnumFlagItem> ExtractFlags()
		{
			List<EnumFlagItem> list = ExtractPowerOf2Flags()
				.Select( e => new EnumFlagItem( e ) )
				.OfType<EnumFlagItem>()
				.ToList();

			foreach ( EnumFlagItem EnumFlagItem in list )
				EnumFlagItem.PropertyChanged += OnEnumFlagItemPropertyChanged;

			return list;
		}

		/// <summary>
		/// Exctracts enum values that are power of 2 from the current type.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<Enum> ExtractPowerOf2Flags()
		{
			List<Enum> allValues = Enum.GetValues( Type ).OfType<Enum>().OrderBy( e => e ).ToList();
			long maxValue = Convert.ToInt64( allValues[allValues.Count - 1] );
			long power = 1;

			foreach ( Enum valueEnum in allValues )
			{
				long valueLong = Convert.ToInt64( valueEnum );
				if ( valueLong > 0 )	//	negative values are ignored
				{
					unchecked
					{
						//	shift power until it reaches or exceeds the current value (or overflows and resets to negative)
						while ( power < valueLong && power > 0 )
							power <<= 1;

						if ( valueLong == power )
							yield return valueEnum;
					}
				}
			}
		}

		/// <summary>
		/// Sets flags according to the specified value.
		/// </summary>
		/// <param name="newValue"></param>
		private void SetFlags( Enum newValue )
		{
			if ( newValue != null )	//	I don't know why, but it happens at design-time
			{
				foreach ( EnumFlagItem EnumFlagItem in Items )
				{
					bool mustBeChecked = newValue.HasFlag( EnumFlagItem.Value );
					bool isChecked = EnumFlagItem.IsChecked;

					if ( mustBeChecked != isChecked )
						EnumFlagItem.IsChecked = mustBeChecked;
				}
			}
		}

		private void OnEnumFlagItemPropertyChanged( object sender, PropertyChangedEventArgs args )
		{
			if ( args.PropertyName == EnumFlagItem.IsCheckedProperty )
				OnFlagIsCheckedChanged( sender as EnumFlagItem );
		}

		private void OnFlagIsCheckedChanged( EnumFlagItem flagItem )
		{
			Enum changedFlagEnum = flagItem.Value;
			bool wasChecked = Value.HasFlag( changedFlagEnum );
			bool isChecked = flagItem.IsChecked;

			if ( wasChecked != isChecked )
			{
				long oldValue = Convert.ToInt64( Value );
				long flagValue = Convert.ToInt64( changedFlagEnum );

				long newValue;
				if ( isChecked )
					newValue = oldValue + flagValue;
				else
					newValue = oldValue - flagValue;

				Value = Enum.ToObject( Type, newValue ) as Enum;
			}
		}
	}
}
