using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Aow2;
using Aow2.Modding;
using Microsoft.Practices.Prism.ViewModel;

namespace ModEditor.ViewModels.Mod.Races
{
	class RaceTabVM : NotificationObject, IModTab
	{
		public RaceTabVM()
		{
			_collection = new ObservableCollection<RaceVM>();
			Items = new ListCollectionView( _collection );
			Items.CurrentChanged += SelectedItemChanged;
		}

		#region Properties

		public ModVM Mod { get; set; }
		public RaceResourceList RaceSettings { get; private set; }

		//internal IUnitPreviewProvider PreviewProvider { get; set; }
		//internal IAbilityListProvider AbilityListProvider { get; set; }

		private ObservableCollection<RaceVM> _collection;
		public ListCollectionView Items { get; private set; }

		public RaceVM SelectedItem => Items.CurrentItem as RaceVM;

		#endregion

		#region IModTab Members

		public string Header => "Races";

		public string IconResourceKey => "RaceTabIcon";

		public bool IsLoaded { get; set; }

		public async Task LoadAsync()
		{
			await Task.Factory.StartNew( () => Load() );
			IsLoaded = true;
		}

		private void Load()
		{
			for ( int i = 0; i < 15; i++ )
			{
				RaceVM newRace = CreateItem( i );
				Items.Dispatcher.Invoke( () => _collection.Add( newRace ) );
			}
		}

		private RaceVM CreateItem( int index )
		{
			RaceVM newRace = new RaceVM()
			{
				Race = (Race) index,
				MpeSettings = Mod.Model.Data.Mpe,
				RaceSettings = Mod.Model.Data.Races[index]
			};
			return newRace;
		}

		#endregion

		private void SelectedItemChanged( object sender, EventArgs e ) => RaisePropertyChanged( () => SelectedItem );
	}
}
