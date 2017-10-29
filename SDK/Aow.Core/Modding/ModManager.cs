using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aow2.Modding
{
	public static class ModManager
	{
		public static IEnumerable<ModInfo> Mods
		{
			get
			{
				IEnumerable<string> subfolders = Directory.EnumerateDirectories( Environment.Instance.ModsFolderPath );
				return subfolders.Where( f => ModInfo.IsModFolder( f ) ).Select( f => new ModInfo( f ) );
			}
		}

		public static ModInfo ActiveMod
		{
			get
			{
				string fullPath = Environment.Instance.ActiveModFullPath;
				if ( !ModInfo.IsModFolder( fullPath ) )
					return null;

				return new ModInfo( fullPath );
			}
			set => Environment.Instance.ActiveModName = value.Name;
		}
	}
}
