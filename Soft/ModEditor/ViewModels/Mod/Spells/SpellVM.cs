using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Aow2;
using Aow2.Modding;
using Aow2.Modding.Internal.Graphics;
using Aow2.Modding.Spells;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.Spells.Dependencies;
using Utils.Patterns.Singleton;
using Utils.WPF.Models;

namespace ModEditor.ViewModels.Mod.Spells
{
	public class SpellVM: NotificationObject
	{
		private IImageProvider _imageProvider = Singleton<ImageProvider>.Instance;

		public SpellVM() => _icon = new Lazy<BitmapSource>(
				() =>
				{
					ImageSequence sequence = Model._icon.FirstOrDefault();
					if ( sequence != null )
					{
						string ilbPath = sequence.ImageLibraryFilename;
						int index = sequence.FrameTable[ 0 ];

						return _imageProvider.GetImage( ilbPath, index );
					}
					else
						return null;
				} );

		public SpellResource Model { get; set; }

		private ISpellListProvider _provider;
		internal ISpellListProvider SpellListProvider
		{
			get => _provider;
			set
			{
				_provider = value;
				GenerateRequiredCheckList();
			}
		}

		private void GenerateRequiredCheckList()
		{
			_requiredCheckList = SpellListProvider.SpellList
				.Select( s => new SpellCheckVM( this, s ) )
				.ToList();

			RequiredCheckList = new ListCollectionView( _requiredCheckList );
			using ( RequiredCheckList.DeferRefresh() )
			{
				RequiredCheckList.CustomSort = new SpellCheckComparer();
				RequiredCheckList.GroupDescriptions.Add( new DelegateGroupDescription<SpellCheckVM>( s => s.Spell.Sphere ) );
			}
		}

		public int ID => Model.ID;

		public string Name => Model.Name;

		public string Description
		{
			get => Model.Description;
			set { Model.Description = value; RaisePropertyChanged( () => Description ); }
		}

		private Lazy<BitmapSource> _icon;
		public BitmapSource Icon => _icon.Value;

		public SphereMask Sphere
		{
			get => Model.Spheres;
			set { Model.Spheres = value; RaisePropertyChanged( () => Sphere ); }
		}

		public int Level
		{
			get => Model.Level;
			set { Model.Level = (byte)value; RaisePropertyChanged( () => Level ); }
		}

		public int ResearchPoints
		{
			get => Model.ResearchCost;
			set { Model.ResearchCost = value; RaisePropertyChanged( () => ResearchPoints ); }
		}

		public int Mana
		{
			get => Model.ManaCost;
			set { Model.ManaCost = value; RaisePropertyChanged( () => Mana ); RaisePropertyChanged( () => ManaUpkeepString ); }
		}

		public int Upkeep
		{
			get => Model.Upkeep;
			set { Model.Upkeep = value; RaisePropertyChanged( () => Upkeep ); RaisePropertyChanged( () => ManaUpkeepString ); }
		}

		public string ManaUpkeepString
		{
			get
			{
				if ( Upkeep == 0 )
					return Mana.ToString();
				else
					return String.Format( "{0}/{1}", Mana, Upkeep );
			}
		}

		public bool IsValidStartingSpell
		{
			get => Model.IsValidStartingSpell;
			set { Model.IsValidStartingSpell = value; RaisePropertyChanged( () => IsValidStartingSpell ); }
		}

		public HashSet<int> RequiredIDs => SpellListProvider.SpellGraph[ ID ];

		private List<SpellCheckVM> _requiredCheckList;
		public ListCollectionView RequiredCheckList { get; private set; }
	}
}
