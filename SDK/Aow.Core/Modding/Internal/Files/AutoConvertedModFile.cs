using System.IO;

namespace Aow2.Modding.Internal.Files
{
	class AutoConvertedModFile<TNew, TLegacy> : IModFile<TNew> where TNew : new()
	{
		private string _newFileName;
		private string _legacyFileName;
		private IStorageStrategy<TNew> _newStorageStrategy;
		private IStorageStrategy<TLegacy> _legacyStorageStrategy;
		private IConverter<TLegacy, TNew> _converter;

		private IModFile<TNew> _newFile;

		public AutoConvertedModFile( string newFileName, string legacyFileName, IStorageStrategy<TNew> newStorageStrategy, IStorageStrategy<TLegacy> legacyStorageStrategy, IConverter<TLegacy, TNew> converter )
		{
			_newFileName = newFileName;
			_legacyFileName = legacyFileName;
			_newStorageStrategy = newStorageStrategy;
			_legacyStorageStrategy = legacyStorageStrategy;
			_converter = converter;
		}

		public TNew Data
		{
			get => _newFile.Data;
			set => _newFile.Data = value;
		}

		public bool IsParsed => _newFile.IsParsed;

		public string Folder
		{
			get
			{
				if ( _newFile == null )
					return null;

				return _newFile.Folder;
			}
			set
			{
				if ( _newFile != null )
				{
					//	initialization and conversion have been taken care of, so we simply operate on the new file now
					_newFile.Folder = value;
				}
				else
				{
					string newPath = Path.Combine( value, _newFileName );
					string legacyPath = Path.Combine( value, _legacyFileName );
					bool newFileExists = File.Exists( newPath );
					bool legacyFileExists = File.Exists( legacyPath );

					if ( !newFileExists && legacyFileExists )
					{
						using ( FileStream stream = new FileStream( legacyPath, FileMode.Open ) )
						{
							TLegacy legacyData = _legacyStorageStrategy.ReadData( stream );
							TNew newData = _converter.Convert( legacyData );

							_newFile = new ModFile<TNew>( _newFileName, _newStorageStrategy );
							_newFile.Folder = value;

							_newFile.Data = newData;
							_newFile.Save();
						}
					}
					else
					{
						//	legacy file is ignored in all 3 other cases
						_newFile = new ModFile<TNew>( _newFileName, _newStorageStrategy );
						_newFile.Folder = value;
					}
				}
			}
		}

		public string FileName => _newFile.FileName;

		public void Save() => _newFile.Save();

		public void Dispose() => _newFile.Dispose();
	}
}
