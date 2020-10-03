using System.Threading.Tasks;

namespace ModEditor.ViewModels.Mod
{
	interface IModTab
	{
		string Header { get; }
		string IconResourceKey { get; }

		bool IsLoaded { get; set; }
		Task LoadAsync();
	}
}
