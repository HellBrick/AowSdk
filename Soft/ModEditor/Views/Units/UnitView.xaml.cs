using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.Units;

namespace ModEditor.Views
{
	/// <summary>
	/// Interaction logic for UnitView.xaml
	/// </summary>
	public partial class UnitView: UserControl
	{
		public UnitView() => InitializeComponent();

		public static DependencyProperty UnitProperty = DependencyProperty.Register(
			"Unit",
			typeof( UnitVM ),
			typeof( UnitView ) );

		public UnitVM Unit
		{
			get => GetValue( UnitProperty ) as UnitVM;
			set => SetValue( UnitProperty, value );
		}
	}
}
