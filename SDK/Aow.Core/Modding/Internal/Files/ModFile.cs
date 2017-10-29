using System;
using System.IO;
using System.Threading;

namespace Aow2.Modding.Internal.Files
{
	internal class ModFile<T> : IModFile<T> where T : new()
	{
		private const string _backupExtension = ".bak";

		public ModFile( string filename, IStorageStrategy<T> storageStrategy )
		{
			FileName = filename;
			_storageStrategy = storageStrategy;
			_data = new Lazy<T>( () => _storageStrategy.ReadData( _stream ), LazyThreadSafetyMode.ExecutionAndPublication );
		}

		private Stream _stream;
		private Lazy<T> _data;
		private IStorageStrategy<T> _storageStrategy;

		public T Data
		{
			get => _data.Value;
			set
			{
				_data = new Lazy<T>( () => value );
				T tmp = _data.Value; //	ensures that the value is marked as created
			}
		}

		public bool IsParsed => _data.IsValueCreated;

		private string _folder;
		public string Folder
		{
			get => _folder;
			set
			{
				//	If ModFile has been connected to a physical file, we must copy it
				if ( _folder != null )
					File.Copy( Path.Combine( _folder, FileName ), Path.Combine( value, FileName ), overwrite: true );

				_folder = value;

				if ( _stream != null )
					_stream.Dispose();

				bool fileExisted = File.Exists( Path.Combine( _folder, FileName ) );
				OpenStream();

				//	If ModFile is initialized and file doesn't exist yet, we must create default file.
				if ( !fileExisted )
				{
					Data = new T();
					Save();
				}
			}
		}

		private void OpenStream() => _stream = new FileStream( Path.Combine( Folder, FileName ), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read );

		public string FileName { get; private set; }

		public void Save()
		{
			if ( IsParsed )
			{
				string path = Path.Combine( Folder, FileName );
				string backupPath = path + _backupExtension;

				if ( File.Exists( path ) )
					File.Copy( path, backupPath, true );

				try
				{
					_stream.SetLength( 0 );
					_storageStrategy.WriteData( _stream, Data );

					_stream.Position = 0;
					_storageStrategy.CrcAlgorithm.ComputeHash( _stream );
					_stream.Write( _storageStrategy.CrcAlgorithm.Hash, 0, _storageStrategy.CrcAlgorithm.Hash.Length );

					if ( File.Exists( backupPath ) )
						File.Delete( backupPath );
				}
				catch
				{
					_stream.Close();

					if ( File.Exists( backupPath ) )
						File.Copy( backupPath, path );

					OpenStream();
				}
			}
		}

		public void Dispose()
		{
			if ( _stream != null )
				_stream.Dispose();
		}
	}
}
