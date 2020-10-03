using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModEditor.Style
{
	partial class SectionedEditor
	{
		private void ParamTextBoxOnGotKeyboardFocus( object sender, RoutedEventArgs e )
		{
			TextBox textBox = sender as TextBox;
			textBox.SelectAll();
		}

		private void ParamTextBoxOnMouseCapture( object sender, MouseEventArgs e )
		{
			TextBox textBox = sender as TextBox;
			if ( !textBox.IsKeyboardFocusWithin )
			{
				textBox.Focus();
				e.Handled = true;
			}
		}
	}
}
