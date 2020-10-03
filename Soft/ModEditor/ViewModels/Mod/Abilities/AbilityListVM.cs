using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Aow2;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.AbilityResources;
using ModEditor.ViewModels.Mod.AbilityResources.Internal;

namespace ModEditor.ViewModels.Mod.Abilities
{
	public class AbilityListVM: NotificationObject
	{
		private IList<Ability> _source;

		public AbilityListVM( IList<Ability> source )
		{
			_source = source;

			Abilities = new ObservableCollection<AbilityVM>( _source.Select( a => CreateItem( a ) ) );
			Abilities.CollectionChanged += OnCollectionChanged;
		}

		internal IAbilityListProvider ListProvider { get; set; }
		public IList<AbilityResourceVM> AllAbilities => ListProvider.AbilityList;

		private AbilityVM CreateItem( Ability a )
		{
			AbilityVM vm = AbilityViewModelFactory.Create( a );
			vm.Removed += OnAbilityRemoved;
			vm.IsEditingChanged += OnAbilityIsEditingChanged;

			return vm;
		}

		public ObservableCollection<AbilityVM> Abilities { get; private set; }

		#region Commands

		#region Add

		private DelegateCommand<AbilityResourceVM> _cmdAdd;
		public DelegateCommand<AbilityResourceVM> AddCommand
		{
			get
			{
				if ( _cmdAdd == null )
					_cmdAdd = new DelegateCommand<AbilityResourceVM>( a => Add( a ) );

				return _cmdAdd;
			}
		}

		private void Add( AbilityResourceVM selectedAbility )
		{
			Ability newAbility = AbilityFactory.Create( selectedAbility.Model );
			AbilityVM newVM = CreateItem( newAbility );
			Abilities.Add( newVM );
		}

		#endregion

		#endregion

		private void OnCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
		{
			switch ( e.Action )
			{
				case NotifyCollectionChangedAction.Remove:
					OnAbilityReallyRemoved( e );
					break;

				case NotifyCollectionChangedAction.Add:
					OnAbilityAdded( e );
					break;

				default:
					throw new NotImplementedException();
			}
		}

		private void OnAbilityAdded( NotifyCollectionChangedEventArgs e )
		{
			IEnumerable<Ability> addedAbilities = e.NewItems.OfType<AbilityVM>().Select( vm => vm.Model );
			foreach ( Ability ability in addedAbilities )
			{
				_source.Add( ability );
			}
		}

		private void OnAbilityReallyRemoved( NotifyCollectionChangedEventArgs e )
		{
			IEnumerable<Ability> removedAbilities = e.OldItems.OfType<AbilityVM>().Select( vm => vm.Model );
			foreach ( Ability ability in removedAbilities )
			{
				_source.Remove( ability );
			}
		}

		private void OnAbilityRemoved( AbilityVM sender, EventArgs e )
		{
			sender.Removed -= OnAbilityRemoved;
			sender.IsEditingChanged -= OnAbilityIsEditingChanged;
			Abilities.Remove( sender );
		}

		private void OnAbilityIsEditingChanged( AbilityVM sender, EventArgs e )
		{
			if ( sender.IsEditing )
			{
				//	collapse all other abilities
				foreach ( AbilityVM ability in Abilities.Where( a => a != sender ) )
				{
					ability.IsEditing = false;
				}
			}
		}
	}
}
