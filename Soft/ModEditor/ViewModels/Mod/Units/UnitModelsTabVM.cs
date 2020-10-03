using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Aow2.Modding.Units;
using Microsoft.Practices.Prism.ViewModel;
using Utils.Collections;
using Utils.Threading;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Mod.Units
{
	class UnitModelsTabVM: NotificationObject, IModTab, IUnitPreviewProvider
	{
		public UnitModelsTabVM()
		{
			BusyIndicator = new Activity( this );
			_collection = new ObservableDictionary<int, UnitModelVM>( vm => vm.Model.ID );
			Items = new ListCollectionView( _collection );
			Items.CurrentChanged += SelectedModelChanged;
		}

		public ModVM Mod { get; set; }

		private bool _loadingStarted = false;
		private SpinLockHelper _loadingLock = new SpinLockHelper();

		private ObservableDictionary<int, UnitModelVM> _collection;
		public ListCollectionView Items { get; private set; }

		public UnitModelVM SelectedItem => Items.CurrentItem as UnitModelVM;

		public Activity BusyIndicator { get; private set; }

		#region IModTab Members

		public string Header => "Unit models";

		public string IconResourceKey => "UnitModelsTabIcon";

		public bool IsLoaded { get; set; }		

		public async Task LoadAsync()
		{
			if ( !_loadingStarted )
			{
				await _loadingLock.InvokeLocked( async () =>
					{
						if ( !_loadingStarted )
						{
							_loadingStarted = true;

							BusyIndicator.ActivityName = "Loading...";
							BusyIndicator.IsBusy = true;

							await Task.Factory.StartNew( () => Load() );

							IsLoaded = true;
							BusyIndicator.IsBusy = false;
						}
					} );
			}
		}

		private void Load()
		{
			foreach ( UnitModel unit in Mod.Model.Data.UnitModels.Values )
			{
				UnitModelVM newVM = CreateItem( unit );
				UnitPreviewHandle previewHandle = _previewHandles.GetOrAdd( unit.ID, new UnitPreviewHandle() );
				previewHandle.UnitModel = newVM;

				Items.Dispatcher.InvokeAsync( () => _collection.Add( newVM ), DispatcherPriority.Background );
			}
		}

		private UnitModelVM CreateItem( UnitModel model ) => new UnitModelVM() { Model = model };

		#endregion

		#region IUnitPreviewProvider Members

		private class UnitPreviewHandle
		{
			private ManualResetEvent _waitHandle = new ManualResetEvent( false );

			private UnitModelVM _unitModel;
			public UnitModelVM UnitModel
			{
				get
				{
					_waitHandle.WaitOne();
					return _unitModel;
				}
				set
				{
					_unitModel = value;
					_waitHandle.Set();
				}
			}
		}

		private ConcurrentDictionary<int, UnitPreviewHandle> _previewHandles = new ConcurrentDictionary<int, UnitPreviewHandle>();

		BitmapSource IUnitPreviewProvider.this[int modelIndex]
		{
			get
			{
				Task loadTask = LoadAsync();

				UnitPreviewHandle previewHandle = _previewHandles.GetOrAdd( modelIndex, new UnitPreviewHandle() );
				return previewHandle.UnitModel.Icon;
			}
		}

		#endregion

		private void SelectedModelChanged( object sender, EventArgs e ) => RaisePropertyChanged( () => SelectedItem );
	}
}
