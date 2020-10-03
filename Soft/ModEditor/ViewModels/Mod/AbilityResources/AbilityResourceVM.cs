using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Aow2;
using Aow2.Items;
using Aow2.Modding.Abilities;
using Aow2.Units;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.AbilityResources.Internal;
using Utils;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	public class AbilityResourceVM : NotificationObject
	{
		public AbilityResource Model { get; set; }

		private IAbilityListProvider _provider;
		internal IAbilityListProvider AbilityListProvider
		{
			get => _provider;
			set
			{
				_provider = value;
				GenerateCheckList();
			}
		}

		private IComparer _maskCheckComparer = new ChainComparer<AbilityMaskCheckVM>(
			new DelegateComparer<AbilityMaskCheckVM>( ( c1, c2 ) => -1 * c1.IsChecked.CompareTo( c2.IsChecked ) ),
			new DelegateComparer<AbilityMaskCheckVM>( ( c1, c2 ) => String.Compare( c1.Name, c2.Name, StringComparison.InvariantCultureIgnoreCase ) ) );

		private void GenerateCheckList()
		{
			_maskedCheckList = AbilityListProvider.AbilityList
				.Select( s => new AbilityMaskCheckVM( this, s ) )
				.ToList();

			MaskedCheckList = new ListCollectionView( _maskedCheckList );
			MaskedCheckList.CustomSort = _maskCheckComparer;
		}

		public AbilityID ID => Model.ID;

		public string Name => Model.Name;

		public string Description
		{
			get => Model.Description;
			set { Model.Description = value; RaisePropertyChanged( () => Description ); }
		}

		public int UpgradeCost
		{
			get => Model.UpgradeCost;
			set { Model.UpgradeCost = value; RaisePropertyChanged( () => UpgradeCost ); }
		}

		public int ForgeCost
		{
			get => Model.ForgeCost;
			set { Model.ForgeCost = value; RaisePropertyChanged( () => ForgeCost ); }
		}

		public Aow2.Races Races
		{
			get => Model.Races;
			set { Model.Races = value; RaisePropertyChanged( () => Races ); }
		}

		public ItemTypes ItemTypes
		{
			get => Model.ItemTypes;
			set { Model.ItemTypes = value; RaisePropertyChanged( () => ItemTypes ); }
		}

		public HeroClasses HeroClasses
		{
			get => Model.HeroClasses;
			set { Model.HeroClasses = value; RaisePropertyChanged( () => HeroClasses ); }
		}

		public bool IsForgeable
		{
			get => Model.IsForgeable;
			set { Model.IsForgeable = value; RaisePropertyChanged( () => IsForgeable ); }
		}

		private List<AbilityMaskCheckVM> _maskedCheckList;
		public ListCollectionView MaskedCheckList { get; private set; }
	}
}
