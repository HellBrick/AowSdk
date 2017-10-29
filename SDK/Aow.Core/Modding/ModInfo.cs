using System;
using System.IO;

namespace Aow2.Modding
{
	public class ModInfo
	{
		private const string _signalFile = "Resource.ahr";

		public ModInfo( string fullFolderPath )
		{
			Name = new DirectoryInfo( fullFolderPath ).Name;
			FullPath = Path.Combine( Environment.Instance.ModsFolderPath, Name );
		}

		public string Name { get; private set; }
		public string FullPath { get; private set; }

		public bool IsActive => Name == Environment.Instance.ActiveModName;

		public AowMod Open() => new AowMod( this );

		public static bool IsModFolder( string fullFolderPath )
		{
			string signalFilePath = Path.Combine( fullFolderPath, _signalFile );
			return File.Exists( signalFilePath );
		}

		public override string ToString()
		{
			if ( !IsActive )
				return Name;
			else
				return Name + " *";
		}

		public void Delete()
		{
			if ( IsActive )
				throw new InvalidOperationException( "Can't delete active mod." );

			Directory.Delete( FullPath, true );
		}
	}
}
