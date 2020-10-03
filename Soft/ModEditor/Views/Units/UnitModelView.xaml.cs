using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.Units;

namespace ModEditor.Views
{
	/// <summary>
	/// Interaction logic for UnitModelView.xaml
	/// </summary>
	public partial class UnitModelView: UserControl
	{
		public UnitModelView() => InitializeComponent();

		public static DependencyProperty UnitModelProperty = DependencyProperty.Register(
			"UnitModel",
			typeof( UnitModelVM ),
			typeof( UnitModelView ) );

		public UnitModelVM UnitModel
		{
			get => GetValue( UnitModelProperty ) as UnitModelVM;
			set => SetValue( UnitModelProperty, value );
		}
	}
}
