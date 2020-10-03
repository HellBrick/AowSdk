using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aow2.Modding;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod;
using ModEditor.ViewModels.Mod.AbilityResources;
using ModEditor.ViewModels.Mod.Game;
using ModEditor.ViewModels.Mod.Races;
using ModEditor.ViewModels.Mod.Skills;
using ModEditor.ViewModels.Mod.Spells;
using ModEditor.ViewModels.Mod.Units;
using ModEditor.ViewModels.Navigation;
using Utils.WPF.Models;

namespace ModEditor.ViewModels
{
	class ModVM: NotificationObject, IMode, ICloseable
	{
		public ModVM( AowMod model )
		{
			Model = model;
			Tabs = new List<IModTab>();

			UnitModelsTabVM unitModelsTab = new UnitModelsTabVM() { Mod = this };
			AbilitiesTabVM abilitiesTab = new AbilitiesTabVM() { Mod = this };

			abilitiesTab.LoadAsync().Wait();

			Tabs.Add( new GameSettingsTabVM() { Mod = this } );
			Tabs.Add( new UnitsTabVM() { Mod = this, PreviewProvider = unitModelsTab, AbilityListProvider = abilitiesTab } );
			Tabs.Add( abilitiesTab );
			Tabs.Add( new SpellsTabVM() { Mod = this } );
			Tabs.Add( new SkillsTabVM() { Mod = this } );
			Tabs.Add( new RaceTabVM() { Mod = this } );

			SelectedTab = Tabs.First();			
		}

		public AowMod Model { get; set; }

		public List<IModTab> Tabs { get; private set;}

		private IModTab _selectedTab;
		public IModTab SelectedTab
		{
			get => _selectedTab;
			set
			{
				_selectedTab = value;
				RaisePropertyChanged( () => SelectedTab );
				RaisePropertyChanged( () => SelectedTabIndex );

				IModTab tabLocal = SelectedTab;

				if ( !tabLocal.IsLoaded )
				{
					Task task = tabLocal.LoadAsync();
					task.ContinueWith( t => tabLocal.IsLoaded = true );
				}
			}
		}

		public int SelectedTabIndex
		{
			get => Tabs.IndexOf( SelectedTab );
			set => SelectedTab = Tabs[ value ];
		}

		async private Task LoadTab()
		{
			await SelectedTab.LoadAsync();
			SelectedTab.IsLoaded = true;
		}

		public void Copy( string newName )
		{
			Model.Copy( newName );
			RaisePropertyChanged( () => HeaderPath );
		}

		#region IMode Members

		public string HeaderPath => Model.Info.Name;

		public bool OnNavigatingTo() => true;

		public bool OnNavigatingAway()
		{
			Model.Dispose();
			return true;
		}

		public IEnumerable<NameDelegateCommand> MenuCommands
		{
			get
			{
				yield return new NameDelegateCommand( "Save", SaveCommand );
				//yield return new NameDelegateCommand( "To mod list", ToModListCommand );
			}
		}

		public event EventHandler<IMode, EventArgs> MenuCommandsChanged;
		private void RaiseMenuCommandsChanged()
		{
			EventHandler<IMode, EventArgs> handler = MenuCommandsChanged;
			if ( handler != null )
				handler( this, new EventArgs() );
		}

		#endregion

		#region Commands

		#region Return to mod list

		private DelegateCommand _cmdToModList;
		public DelegateCommand ToModListCommand
		{
			get
			{
				if ( _cmdToModList == null )
					_cmdToModList = new DelegateCommand( () => ReturnToModList() );

				return _cmdToModList;
			}
		}

		private void ReturnToModList() => Global.Navigation.CurrentMode = new ModListScreenVM();

		#endregion

		#region Save

		private DelegateCommand _cmdSave;
		public DelegateCommand SaveCommand
		{
			get
			{
				if ( _cmdSave == null )
					_cmdSave = new DelegateCommand( () => Save() );

				return _cmdSave;
			}
		}

		private void Save() => Model.Save();

		#endregion

		#endregion

		#region ICloseable Members

		public bool OnClosing()
		{
			Model.Dispose();
			return true;
		}

		#endregion
	}
}
