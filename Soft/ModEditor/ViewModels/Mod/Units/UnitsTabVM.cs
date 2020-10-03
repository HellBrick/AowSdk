using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Aow2.Modding.Units;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.AbilityResources.Internal;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Mod.Units
{
	class UnitsTabVM: NotificationObject, IModTab
	{
		public UnitsTabVM()
		{
			BusyIndicator = new Activity();
			_collection =  new ObservableCollection<UnitVM>();
			Items = new ListCollectionView( _collection );
			Items.CurrentChanged += SelectedUnitChanged;
		}

		public ModVM Mod { get; set; }
		internal IUnitPreviewProvider PreviewProvider { get; set; }
		internal IAbilityListProvider AbilityListProvider { get; set; }

		private ObservableCollection<UnitVM> _collection;
		public ListCollectionView Items { get; private set; }

		public UnitVM SelectedUnit => Items.CurrentItem as UnitVM;

		public Activity BusyIndicator { get; private set; }

		#region IModTab Members

		public string Header => "Units";

		public string IconResourceKey => "UnitTabIcon";

		public bool IsLoaded { get; set; }		

		public async Task LoadAsync()
		{
			BusyIndicator.ActivityName = "Loading...";
			BusyIndicator.IsBusy = true;

			await Task.Factory.StartNew( () => Load() );
			
			IsLoaded = true;
			BusyIndicator.IsBusy = false;
		}

		private void Load()
		{
			foreach ( UnitResource unit in Mod.Model.Data.Units.Values )
			{
				UnitVM newVM = CreateItem( unit );
				Items.Dispatcher.InvokeAsync( () => _collection.Add( newVM ), DispatcherPriority.Background );
			}
		}

		private UnitVM CreateItem( UnitResource unit ) => new UnitVM()
		{
			Model = unit,
			PreviewProvider = PreviewProvider,
			AbilityListProvider = AbilityListProvider
		};

		#endregion

		private void SelectedUnitChanged( object sender, EventArgs e ) => RaisePropertyChanged( () => SelectedUnit );
	}
}
