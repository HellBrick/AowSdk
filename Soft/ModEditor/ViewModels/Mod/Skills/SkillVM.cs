using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Aow2.Modding;
using Aow2.Modding.Internal.Graphics;
using Aow2.Modding.Skills;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.Skills.Dependencies;
using ModEditor.ViewModels.Mod.Skills.Internal;
using Utils;
using Utils.Patterns.Singleton;

namespace ModEditor.ViewModels.Mod.Skills
{
	public class SkillVM: NotificationObject
	{
		private IImageProvider _imageProvider = Singleton<ImageProvider>.Instance;

		public SkillVM()
		{
			_exclusionListLazy = new Lazy<ListCollectionView>( () => CreateExclusionList() );

			_icon = new Lazy<BitmapSource>(
				() =>
				{
					Aow2.Collections.AowList<ImageSequence> imageData = Model.Icon;
					if ( imageData == null )
						return null;

					ImageSequence sequence = imageData.FirstOrDefault();
					if ( sequence == null )
						return null;

					if ( sequence.FrameTable.Count == 0 )
						return null;

					string ilbPath = sequence.ImageLibraryFilename;
					int index = sequence.FrameTable[0];

					return _imageProvider.GetImage( ilbPath, index );
				} );
		}

		public SkillResource Model { get; set; }
		internal ISkillListPrivider SkillListProvider { get; set; }

		#region Properties

		public int ID => Model.ID;

		public string Name
		{
			get => Model.Name;
			set { Model.Name = value; RaisePropertyChanged( () => Name ); }
		}

		public string Description
		{
			get => Model.Description;
			set { Model.Description = value; RaisePropertyChanged( () => Description ); }
		}

		public int SkillPoints
		{
			get => Model.SkillPoints;
			set { Model.SkillPoints = ClamSkillPoints( value ); RaisePropertyChanged( () => SkillPoints ); }
		}

		private sbyte ClamSkillPoints( int value )
		{
			if ( value > SByte.MaxValue )
				return SByte.MaxValue;

			if ( value < SByte.MinValue )
				return SByte.MinValue;

			return (sbyte) value;
		}

		public int ResearchPoints
		{
			get => Model.ResearchPoints;
			set { Model.ResearchPoints = value; RaisePropertyChanged( () => ResearchPoints ); }
		}

		private Lazy<ListCollectionView> _exclusionListLazy;
		public ListCollectionView ExclusionList => _exclusionListLazy.Value;

		private Lazy<BitmapSource> _icon;
		public BitmapSource Icon => _icon.Value;

		#endregion

		private ListCollectionView CreateExclusionList()
		{
			System.Collections.Generic.List<SkillCkeckVM> checks = SkillListProvider.Exclusions.GetChecks( this );
			ListCollectionView exclusionList = new ListCollectionView( checks );
			SkillComparer comparer = new SkillComparer();
			exclusionList.CustomSort = new DelegateComparer<SkillCkeckVM>( ( c1, c2 ) => comparer.Compare( c1.Skill, c2.Skill ) );

			return exclusionList;
		}	
	}
}
