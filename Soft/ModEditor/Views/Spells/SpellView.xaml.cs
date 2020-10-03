using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.Spells;

namespace ModEditor.Views
{
	/// <summary>
	/// Interaction logic for SpellView.xaml
	/// </summary>
	public partial class SpellView: UserControl
	{
		public SpellView() => InitializeComponent();

		public static DependencyProperty SpellProperty = DependencyProperty.Register(
			"Spell",
			typeof( SpellVM ),
			typeof( SpellView ) );

		public SpellVM Spell
		{
			get => GetValue( SpellProperty ) as SpellVM;
			set => SetValue( SpellProperty, value );
		}
	}
}
