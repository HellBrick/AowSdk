using System;
using System.Collections.Generic;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Navigation
{
	interface IMode
	{
		string HeaderPath { get; }

		bool OnNavigatingTo();
		bool OnNavigatingAway();

		IEnumerable<NameDelegateCommand> MenuCommands { get; }
		event EventHandler<IMode, EventArgs> MenuCommandsChanged;
	}
}
