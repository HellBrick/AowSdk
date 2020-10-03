using System;
using System.Windows;
using System.Windows.Controls;

namespace ModEditor.Views.Abilities
{
	/// <summary>
	/// Interaction logic for AbilityListView.xaml
	/// </summary>
	public partial class AbilityListView: UserControl
	{
		public AbilityListView() => InitializeComponent();

		private void OnPopupOpened( object sender, EventArgs e )
		{
			UIElement list = LogicalTreeHelper.FindLogicalNode( this, "_abilityPickerList" ) as UIElement;
			list.Focus();
		}
	}
}
