using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.Skills.Dependencies;
using ModEditor.ViewModels.Mod.Skills.Internal;
using Utils.Collections;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Mod.Skills
{
	class SkillsTabVM: NotificationObject, IModTab, ISkillListPrivider
	{
		public SkillsTabVM()
		{
			BusyIndicator = new Activity( this );
			_collection = new ObservableDictionary<int, SkillVM>( item => item.ID );
			Items = new ListCollectionView( _collection );
			Items.CustomSort = new SkillComparer();
			Items.CurrentChanged += SelectedSkillChanged;
		}

		#region Properties

		public ModVM Mod { get; set; }

		private ObservableDictionary<int, SkillVM> _collection;
		public ListCollectionView Items { get; private set; }

		public SkillVM SelectedSkill => Items.CurrentItem as SkillVM;

		public Activity BusyIndicator { get; private set; }

		#endregion

		#region IModTab Members

		public string Header => "Skills";

		public string IconResourceKey => "SkillTabIcon";

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
			//	Fkill skill list
			foreach ( Aow2.Modding.Skills.SkillResource skill in Mod.Model.Data.Skills.Values )
			{
				SkillVM newVM = CreateItem( skill );
				Items.Dispatcher.Invoke( () => _collection.Add( newVM ), DispatcherPriority.Background );
			}

			//	Create exclusion grid
			Exclusions = new ExclusionGrid( _collection.Values );
		}

		private SkillVM CreateItem( Aow2.Modding.Skills.SkillResource skill )
		{
			SkillVM viewModel = SkillViewModelFactory.Create( skill );
			viewModel.SkillListProvider = this;
			return viewModel;
		}

		#endregion

		#region ISkillListPrivider Members

		public IList<SkillVM> Skills => Items.OfType<SkillVM>().ToList();

		public ExclusionGrid Exclusions { get; private set; }

		#endregion

		private void SelectedSkillChanged( object sender, EventArgs e ) => RaisePropertyChanged( () => SelectedSkill );
	}
}
