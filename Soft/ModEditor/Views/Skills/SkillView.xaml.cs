using System.Windows;
using System.Windows.Controls;
using ModEditor.ViewModels.Mod.Skills;

namespace ModEditor.Views
{
	/// <summary>
	/// Interaction logic for SkillView.xaml
	/// </summary>
	public partial class SkillView: UserControl
	{
		public SkillView() => InitializeComponent();

		public static DependencyProperty SkillProperty = DependencyProperty.Register(
			"Skill",
			typeof( SkillVM ),
			typeof( SkillView ) );

		public SkillVM Skill
		{
			get => GetValue( SkillProperty ) as SkillVM;
			set => SetValue( SkillProperty, value );
		}
	}
}
