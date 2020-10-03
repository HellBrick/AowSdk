using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.Races;

namespace ModEditor.Views.Races
{
	/// <summary>
	/// Interaction logic for RaceView.xaml
	/// </summary>
	public partial class RaceView: UserControl
	{
		public RaceView() => InitializeComponent();

		#region Race

		public static DependencyProperty RaceProperty = DependencyProperty.Register(
			"Race",
			typeof( RaceVM ),
			typeof( RaceView ),
			new FrameworkPropertyMetadata()
			{
				PropertyChangedCallback = OnRaceChanged
			} );

		public RaceVM Race
		{
			get => (RaceVM) GetValue( RaceProperty );
			set => SetValue( RaceProperty, value );
		}

		private static void OnRaceChanged( DependencyObject obj, DependencyPropertyChangedEventArgs args )
		{
			RaceView source = obj as RaceView;
		}

		#endregion
	}
}
