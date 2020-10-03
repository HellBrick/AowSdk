using System;
using System.Threading.Tasks;

namespace ModEditor.ViewModels.Mod
{
	[Obsolete("This is a temp class and should be removed when all stubs are replaced by real tabs")]
	class TabStub: IModTab
	{
		public TabStub( string header )
		{
			Header = header;
			IconResourceKey = Header.Replace( " ", "" ) + "TabIcon";
		}

		#region IModTab Members

		public string Header { get; private set; }

		public string IconResourceKey { get; private set; }

		public bool IsLoaded { get; set; }

		public Task LoadAsync() => Task.Factory.StartNew( () => IsLoaded = true );

		#endregion
	}
}
