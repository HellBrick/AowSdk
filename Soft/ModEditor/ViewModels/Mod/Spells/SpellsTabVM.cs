using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Aow2.Modding.Spells;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.Spells.Dependencies;
using ModEditor.ViewModels.Mod.Spells.Internal;
using Utils.Collections;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Mod.Spells
{
	class SpellsTabVM: NotificationObject, IModTab, ISpellListProvider
	{
		public SpellsTabVM()
		{
			BusyIndicator = new Activity( this );
			_collection = new ObservableDictionary<int, SpellVM>( item => item.ID );
			Items = new ListCollectionView( _collection );
			using ( Items.DeferRefresh() )
			{
				Items.CustomSort = new SpellComparer();
				Items.GroupDescriptions.Add( new DelegateGroupDescription<SpellVM>( s => s.Sphere ) );
			}
			Items.CurrentChanged += SelectedSkillChanged;
		}

		private void SelectedSkillChanged( object sender, EventArgs e ) => RaisePropertyChanged( () => SelectedSpell );

		#region Properties

		public ModVM Mod { get; set; }

		private ObservableDictionary<int, SpellVM> _collection;
		public ListCollectionView Items { get; private set; }

		public SpellVM SelectedSpell => Items.CurrentItem as SpellVM;

		public Activity BusyIndicator { get; private set; }

		#endregion

		#region IModTab Members

		public string Header => "Spells";

		public string IconResourceKey => "SpellTabIcon";

		public bool IsLoaded { get; set; }

		public Task LoadAsync() => Task.Factory.StartNew( () => Load() );

		private void Load()
		{
			foreach ( SpellResource spell in Mod.Model.Data.Spells.Values )
			{
				SpellVM newVM = CreateItem( spell );
				Items.Dispatcher.Invoke( () => _collection.Add( newVM ), DispatcherPriority.Background );
			}
			foreach ( SpellVM spell in _collection )
			{
				spell.SpellListProvider = this;
			}
		}

		private SpellVM CreateItem( SpellResource spell ) => SpellViewModelFactory.Create( spell );

		#endregion

		#region ISpellListProvider Members

		IList<SpellVM> ISpellListProvider.SpellList => Items.OfType<SpellVM>().ToList();

		Aow2.Modding.MPE.SpellGraph ISpellListProvider.SpellGraph => Mod.Model.Data.Mpe.SpellGraph;

		#endregion
	}
}
