using System;
using System.Windows;
using Aow2.Modding;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace ModEditor.ViewModels
{
	class ModInfoVM: NotificationObject
	{
		public ModInfo Model { get; set; }

		#region Properties

		public string Name => Model.Name;

		public bool IsActive
		{
			get => Model.IsActive;
			set
			{
				if ( value )
					ModManager.ActiveMod = Model;

				RaisePropertyChanged( () => IsActive );
				ActivateCommand.RaiseCanExecuteChanged();
				DeleteCommand.RaiseCanExecuteChanged();
			}
		}

		#endregion

		#region Events

		public event EventHandler Activated;
		private void RaiseActivated()
		{
			EventHandler handler = Activated;
			if ( handler != null )
				handler( this, new EventArgs() );
		}

		public event EventHandler Removed;
		private void RaiseRemoved()
		{
			EventHandler handler = Removed;
			if ( handler != null )
				handler( this, new EventArgs() );
		}

		#endregion

		#region Commands

		#region Edit

		private DelegateCommand _cmdEdit;
		public DelegateCommand EditCommand
		{
			get
			{
				if ( _cmdEdit == null )
					_cmdEdit = new DelegateCommand( () => Edit() );

				return _cmdEdit;
			}
		}

		private void Edit()
		{
			try
			{
				AowMod mod = Model.Open();
				ModVM modvm = new ModVM( mod );
				Global.Navigation.OpenWindow( modvm );
			}
			catch
			{
			}
		}

		#endregion

		#region Activate

		private DelegateCommand _cmdActivate;
		public DelegateCommand ActivateCommand
		{
			get
			{
				if ( _cmdActivate == null )
					_cmdActivate = new DelegateCommand(
						() => Activate(),
						() => IsActive == false );

				return _cmdActivate;
			}
		}

		private void Activate()
		{
			IsActive = true;
			if ( IsActive )
				RaiseActivated();
		}

		#endregion

		#region Delete

		private DelegateCommand _cmdDelete;
		public DelegateCommand DeleteCommand
		{
			get
			{
				if ( _cmdDelete == null )
					_cmdDelete = new DelegateCommand(
						() => Delete(),
						() => IsActive == false );

				return _cmdDelete;
			}
		}

		private void Delete()
		{
			MessageBoxResult result = MessageBox.Show(
				String.Format( "Are you sure you want to delete mod '{0}'", Name ),
				"Deleting mod",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Question );

			if ( result == MessageBoxResult.OK )
			{
				Model.Delete();
				RaiseRemoved();
			}
		}

		#endregion

		#endregion
	}
}
