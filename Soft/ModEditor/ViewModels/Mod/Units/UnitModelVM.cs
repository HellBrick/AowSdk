using System;
using System.Windows.Media.Imaging;
using Aow2.Modding;
using Aow2.Modding.Internal.Graphics;
using Aow2.Modding.Units;
using Microsoft.Practices.Prism.ViewModel;
using Utils.Patterns.Singleton;

namespace ModEditor.ViewModels.Mod.Units
{
	public class UnitModelVM: NotificationObject
	{
		private IImageProvider _imageProvider = Singleton<ImageProvider>.Instance;

		public UnitModelVM() => _icon = new Lazy<BitmapSource>(
				() =>
				{
					ImageSequence sequence = Model.Animations.Preview;
					if ( sequence != null )
					{
						//	Doom bats fix
						while ( /*sequence.AnimationMode == AnimationMode.None &&*/ sequence.NextSequence != null )
							sequence = sequence.NextSequence as ImageSequence;

						string ilbPath = sequence.ImageLibraryFilename;
						int index = sequence.FrameTable[ 0 ];

						return _imageProvider.GetCroppedImage( ilbPath, index );
					}
					else
						return null;
				} );

		public UnitModel Model { get; set; }

		private Lazy<BitmapSource> _icon;
		public BitmapSource Icon => _icon.Value;
	}
}
