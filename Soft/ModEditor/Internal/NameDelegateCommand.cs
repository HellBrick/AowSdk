using System;
using Microsoft.Practices.Prism.Commands;

namespace Utils.WPF.Models
{
	class NameDelegateCommand: DelegateCommand
	{
		public NameDelegateCommand( string name, DelegateCommand baseCommand )
			: base( baseCommand.Execute, baseCommand.CanExecute ) => Name = name;

		public NameDelegateCommand( string name, Action execute, Func<bool> canExecute )
			: base( execute, canExecute ) => Name = name;

		public string Name { get; set; }
	}
}
