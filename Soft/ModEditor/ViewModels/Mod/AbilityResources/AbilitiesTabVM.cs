using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.AbilityResources.Internal;
using Utils;
using Utils.Collections;
using Utils.Threading;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	class AbilitiesTabVM : NotificationObject, IModTab, IAbilityListProvider
	{
		private ManualResetEvent _loadedEvent = new ManualResetEvent( false );
		private SpinLockHelper _lock = new SpinLockHelper();

		public AbilitiesTabVM()
		{
			BusyIndicator = new Activity( this );
			_collection = new ObservableDictionary<int, AbilityResourceVM>( item => (int) item.ID );
			Items = new ListCollectionView( _collection );
			using ( Items.DeferRefresh() )
			{
				Items.CustomSort = new DelegateComparer<AbilityResourceVM>( ( a1, a2 ) => String.Compare( a1.Name, a2.Name ) );
				
				DelegateGroupDescription<AbilityResourceVM> groupByLetter = new DelegateGroupDescription<AbilityResourceVM>( a => a.Name[0] );
				Items.GroupDescriptions.Add( groupByLetter );
			}
			Items.CurrentChanged += SelectedSkillChanged;
		}

		private void SelectedSkillChanged( object sender, EventArgs e ) => RaisePropertyChanged( () => SelectedAbility );

		#region Properties

		public ModVM Mod { get; set; }

		private ObservableDictionary<int, AbilityResourceVM> _collection;
		public ListCollectionView Items { get; private set; }

		public AbilityResourceVM SelectedAbility => Items.CurrentItem as AbilityResourceVM;

		public Activity BusyIndicator { get; private set; }

		#endregion

		#region IModTab Members

		public string Header => "Abilities";

		public string IconResourceKey => "AbilitiesTabIcon";

		public bool IsLoaded { get; set; }

		public Task LoadAsync() => Task.Factory.StartNew( () => Load() );

		private void Load()
		{
			foreach ( Aow2.Modding.Abilities.AbilityResource ability in Mod.Model.Data.Abilities.Values )
			{
				AbilityResourceVM newVM = AbilityViewModelFactory.Create( ability );
				_abilityListHelper.Add( newVM );
				Items.Dispatcher.InvokeAsync( () => _collection.Add( newVM ), DispatcherPriority.Background );
			}

			_abilityListHelper.Sort( new DelegateComparer<AbilityResourceVM>( ( a1, a2 ) => a1.Name.CompareTo( a2.Name ) ) );

			foreach ( AbilityResourceVM ability in _abilityListHelper )
			{
				ability.AbilityListProvider = this;
			}
			IsLoaded = true;
			_loadedEvent.Set();
		}
		

		#endregion

		#region IAbilityListProvider Members

		private List<AbilityResourceVM> _abilityListHelper = new List<AbilityResourceVM>();
		IList<AbilityResourceVM> IAbilityListProvider.AbilityList => _abilityListHelper;

		#endregion
	}
}
