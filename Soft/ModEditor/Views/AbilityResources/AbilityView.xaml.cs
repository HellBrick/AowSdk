using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.AbilityResources;

namespace ModEditor.Views.AbilityResources
{
	/// <summary>
	/// Interaction logic for AbilityView.xaml
	/// </summary>
	public partial class AbilityView : UserControl
	{
		public AbilityView() => InitializeComponent();

		public static DependencyProperty AbilityProperty = DependencyProperty.Register(
			"Ability",
			typeof( AbilityResourceVM ),
			typeof( AbilityView ) );

		public AbilityResourceVM Ability
		{
			get => GetValue( AbilityProperty ) as AbilityResourceVM;
			set => SetValue( AbilityProperty, value );
		}
	}
}
