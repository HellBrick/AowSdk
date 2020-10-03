using System.Windows.Media.Imaging;

namespace ModEditor.ViewModels.Mod.Units
{
	interface IUnitPreviewProvider
	{
		BitmapSource this[int modelIndex] { get; }
	}
}
