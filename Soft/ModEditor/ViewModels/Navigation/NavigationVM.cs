using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Navigation
{
	class NavigationVM: NotificationObject
	{
		public NavigationVM()
		{
			//CurrentMode = new ModListScreenVM();
		}

		private IMode _current;
		public IMode CurrentMode
		{
			get => _current;
			set
			{
				bool canNavigate = true;

				if ( ( _current != null ) )
					canNavigate = _current.OnNavigatingAway();

				if ( canNavigate )
					canNavigate = value.OnNavigatingTo();

				if ( canNavigate )
				{
					if ( _current != null )
						_current.MenuCommandsChanged -= OnMenuCommandsChanged;

					_current = value;
					_current.MenuCommandsChanged += OnMenuCommandsChanged;

					RaisePropertyChanged( () => CurrentMode );
					RaisePropertyChanged( () => HeaderPath );
					RaisePropertyChanged( () => MenuCommands );
				}
			}
		}

		private void OnMenuCommandsChanged( IMode sender, EventArgs e ) => RaisePropertyChanged( () => MenuCommands );

		public IEnumerable<NameDelegateCommand> MenuCommands
		{
			get
			{
				if ( CurrentMode == null )
					return Enumerable.Empty<NameDelegateCommand>();
				else
					return CurrentMode.MenuCommands;
			}
		}

		public string HeaderPath => CurrentMode != null ? CurrentMode.HeaderPath : String.Empty;

		public void OpenWindow( object viewModel )
		{
			Type windowType = _windowMap[viewModel.GetType()];
			Window newWindow = Activator.CreateInstance( windowType ) as Window;
			newWindow.DataContext = viewModel;
			newWindow.Closed += OnWindowClosed;

			newWindow.Show();
			
			if ( ActiveWindow != null )
				ActiveWindow.Close();

			if ( MainWindow != newWindow )
				MainWindow.Hide();

			ActiveWindow = newWindow;
		}

		void OnWindowClosed( object sender, EventArgs e )
		{
			Window window = sender as Window;
			ICloseable closeable = window.DataContext as ICloseable;

			if ( closeable != null )
				closeable.OnClosing();

			window.Closed -= OnWindowClosed;
			MainWindow.Show();
		}

		private Dictionary<Type, Type> _windowMap = new Dictionary<Type, Type>();

		public void RegisterWindow( Type viewModel, Type window ) => _windowMap.Add( viewModel, window );

		public Window ActiveWindow { get; set; }

		private Window _mainWindow;
		public Window MainWindow
		{
			get => _mainWindow;
			set
			{
				_mainWindow = value;
				_mainWindow.Activated += _mainWindow_Activated;
			}
		}

		void _mainWindow_Activated( object sender, EventArgs e )
		{
			IShowable showable = ( sender as Window ).DataContext as IShowable;
			if ( showable != null )
				showable.OnShow();
		}
	}

	public class OpenWindowEventArgs: EventArgs
	{
		public OpenWindowEventArgs( object viewModel ) => ViewModel = viewModel;

		public object ViewModel { get; set; }
	}
}
