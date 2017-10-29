using System;
using System.IO;
using Microsoft.Win32;

namespace Aow2
{
	public sealed class Environment : IEnvironment
	{
		#region Construction

		private Environment()
		{
		}

		static Environment() => Instance = new Environment();

		#endregion

		#region Public

		public static IEnvironment Instance { get; set; }

		#endregion

		#region IEnvironment Members

		string IEnvironment.ImagesFolder => Path.Combine( RootFolder, _imagesSubfolder );

		string IEnvironment.ModsFolderPath => Path.Combine( AowDocumentsFolder, _modsSubfolder );

		string IEnvironment.ActiveModName
		{
			get => new DirectoryInfo( ActiveModRegistryValue ).Name;
			set
			{
				string path = String.Concat( Path.DirectorySeparatorChar, value, Path.DirectorySeparatorChar );
				ActiveModRegistryValue = path;
			}
		}

		string IEnvironment.ActiveModFullPath => ( this as IEnvironment ).ModsFolderPath + ActiveModRegistryValue;

		#endregion

		#region Internal

		private const string _rkeyGeneralSettings = @"Software\Triumph Studios\Age of Wonders Shadow Magic\General";
		private const string _rkeyMpe = @"Software\Triumph Studios\Age of Wonders Shadow Magic\MP ++  ";
		private const string _rvalueRootFolder = "Root Directory";
		private const string _rvalueCurrentMod = "Resources Name";

		private const string _aowDocsSubfolder = "Age of Wonders - MP Evolution 2";
		private const string _modsSubfolder = "Mods";

		private const string _imagesSubfolder = "Images";

		private Lazy<string> _rootFolder = new Lazy<string>(
			() =>
			{
				RegistryKey r_key = Registry.CurrentUser.OpenSubKey( _rkeyGeneralSettings );
				return (string) r_key.GetValue( _rvalueRootFolder );
			} );

		private string RootFolder => _rootFolder.Value;

		private string AowDocumentsFolder
		{
			get
			{
				string docs = System.Environment.GetFolderPath( System.Environment.SpecialFolder.CommonDocuments );
				return Path.Combine( docs, _aowDocsSubfolder );
			}
		}

		private string ActiveModRegistryValue
		{
			get
			{
				RegistryKey r_key = Registry.CurrentUser.OpenSubKey( _rkeyMpe );
				return (string) r_key.GetValue( _rvalueCurrentMod );
			}
			set
			{
				RegistryKey r_key = Registry.CurrentUser.OpenSubKey( _rkeyMpe, true );
				r_key.SetValue( _rvalueCurrentMod, value, RegistryValueKind.String );
			}
		}

		#endregion
	}
}
