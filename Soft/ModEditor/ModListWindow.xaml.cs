using System.Windows;
using ModEditor.ViewModels;

namespace ModEditor
{
	/// <summary>
	/// Interaction logic for ModListWindow.xaml
	/// </summary>
	public partial class ModListWindow: Window
	{
		public ModListWindow()
		{
			Global.Navigation.MainWindow = this;
			InitializeComponent();
		}
	}
}
