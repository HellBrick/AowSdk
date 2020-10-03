using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Aow2.Modding;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Navigation;
using Utils.Threading;
using Utils.WPF.Models;

namespace ModEditor.ViewModels
{
	class ModListScreenVM: NotificationObject, IMode, IShowable
	{
		private SpinLockHelper _refreshLock = new SpinLockHelper();

		public ModListScreenVM()
		{
			_collection = new ObservableCollection<ModInfoVM>();
			Items = new ListCollectionView( _collection );
			Task refreshTask = RefreshAsync();
		}

		private ObservableCollection<ModInfoVM> _collection;
		public ListCollectionView Items { get; private set; }

		#region IMode Members

		public string HeaderPath => "Available mods";

		public bool OnNavigatingTo()
		{
			Task refreshTask = RefreshAsync();
			return true;
		}

		public bool OnNavigatingAway() => true;

		public IEnumerable<NameDelegateCommand> MenuCommands
		{
			get { yield break; }
		}

		public event EventHandler<IMode, EventArgs> MenuCommandsChanged
		{
			add { }
			remove { }
		}

		#endregion

		#region Refresh

		private bool _isRefreshing;
		public bool IsRefreshing
		{
			get => _isRefreshing;
			set { _isRefreshing = value; RaisePropertyChanged( () => IsRefreshing ); }
		}

		async private Task RefreshAsync()
		{
			IsRefreshing = true;
			await Task.Factory.StartNew( () => DoRefresh() );			
			IsRefreshing = false;
		}

		private void DoRefresh() => _refreshLock.InvokeLocked( () =>
												 {
													 Items.Dispatcher.Invoke( () => _collection.Clear() );
													 foreach ( ModInfo modInfo in ModManager.Mods )
													 {
														 Items.Dispatcher.Invoke( () => _collection.Add( CreateItem( modInfo ) ), DispatcherPriority.Background );
													 }
													 ModInfoVM active = _collection.FirstOrDefault( m => m.IsActive );
													 if ( active != null )
														 Items.Dispatcher.Invoke( () => Items.MoveCurrentTo( active ), DispatcherPriority.Background );
												 } );

		#endregion

		#region Private

		private ModInfoVM CreateItem( ModInfo modInfo )
		{
			ModInfoVM newItem = new ModInfoVM() { Model = modInfo };
			newItem.Activated += OnItemActivated;
			newItem.Removed += OnItemRemoved;
			return newItem;
		}

		private void UnsubscribeFromItem( ModInfoVM item )
		{
			item.Activated -= OnItemActivated;
			item.Removed -= OnItemRemoved;
		}

		private void OnItemActivated( object sender, EventArgs e )
		{
			ModInfoVM newActive = sender as ModInfoVM;

			IEnumerable<ModInfoVM> anotherMods = _collection.Where( m => m != newActive );
			foreach ( ModInfoVM another in anotherMods )
				another.IsActive = false;
		}

		private void OnItemRemoved( object sender, EventArgs e )
		{
			ModInfoVM removed = sender as ModInfoVM;
			UnsubscribeFromItem( removed );
			Items.Dispatcher.Invoke( () => _collection.Remove( removed ), DispatcherPriority.Background );
		}

		#endregion

		#region IShowable Members

		public void OnShow()
		{
			Task refreshTask = RefreshAsync();
		}

		#endregion
	}
}
