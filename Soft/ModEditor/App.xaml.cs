using System.Windows;
using ModEditor.ViewModels;

namespace ModEditor
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App: Application
	{
		protected override void OnStartup( StartupEventArgs e )
		{
			Global.Navigation.RegisterWindow( typeof( ModVM ), typeof( ModWindow ) );
			//Global.Navigation.OpenWindowRequested += OnOpenWindowRequested;

			base.OnStartup( e );
		}

		//private void OnOpenWindowRequested( object sender, ViewModels.Navigation.OpenWindowEventArgs e )
		//{
		//	throw new NotImplementedException();
		//}
	}
}
