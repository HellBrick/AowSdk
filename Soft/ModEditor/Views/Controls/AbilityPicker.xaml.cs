using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.AbilityResources;

namespace ModEditor.Views.Controls
{
	/// <summary>
	/// Interaction logic for AbilityPicker.xaml
	/// </summary>
	public partial class AbilityPicker: UserControl
	{
		public AbilityPicker() => InitializeComponent();

		#region Dependency properties

		#region AbilityList

		public static DependencyProperty AbilityListProperty = DependencyProperty.Register(
			"AbilityList",
			typeof( List<AbilityResourceVM> ),
			typeof( AbilityPicker ),
			new FrameworkPropertyMetadata()
			{
				PropertyChangedCallback = OnAbilityListChanged
			} );

		public List<AbilityResourceVM> AbilityList
		{
			get => (List<AbilityResourceVM>) GetValue( AbilityListProperty );
			set => SetValue( AbilityListProperty, value );
		}

		private static void OnAbilityListChanged( DependencyObject obj, DependencyPropertyChangedEventArgs args )
		{
			AbilityPicker source = obj as AbilityPicker;
		}

		#endregion

		#endregion
	}
}
