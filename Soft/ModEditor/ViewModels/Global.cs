using ModEditor.ViewModels.Navigation;
using Utils.Patterns.Singleton;

namespace ModEditor.ViewModels
{
	static class Global
	{
		public static NavigationVM Navigation => Singleton<NavigationVM>.Instance;
	}
}
