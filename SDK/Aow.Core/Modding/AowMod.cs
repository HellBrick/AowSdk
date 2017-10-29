using System;
using System.IO;

namespace Aow2.Modding
{
	public class AowMod : IDisposable
	{
		public AowMod( ModInfo modInfo )
		{
			if ( !ModInfo.IsModFolder( modInfo.FullPath ) )
				throw new FileNotFoundException( String.Format( "Can't find main resource file in folder {0}.", modInfo.FullPath ) );

			Info = modInfo;
			Data = new ModData( this );
		}

		public ModInfo Info { get; private set; }
		public ModData Data { get; private set; }

		public void MakeActive() => ModManager.ActiveMod = Info;

		public void Save() => Data.Save();

		public void Copy( string newName )
		{
			ModInfo oldInfo = Info;

			string fullPath = Path.Combine( Environment.Instance.ModsFolderPath, newName );
			if ( Directory.Exists( fullPath ) )
				throw new ArgumentException( "This name is already taken." );

			Info = new ModInfo( fullPath );
			Directory.CreateDirectory( Info.FullPath );

			foreach ( string file in Directory.EnumerateFiles( oldInfo.FullPath, "*", SearchOption.TopDirectoryOnly ) )
			{
				string filename = Path.GetFileName( file );
				File.Copy( file, Path.Combine( Info.FullPath, filename ) );
			}

			Data.LinkToNewFolder( fullPath );
		}

		public void Rename( string newName )
		{
			string oldFolder = Info.FullPath;
			bool wasActive = Info.IsActive;

			Copy( newName );

			if ( wasActive )
				ModManager.ActiveMod = Info;

			Directory.Delete( oldFolder, true );
		}

		public void Dispose() => Data.Dispose();
	}
}
