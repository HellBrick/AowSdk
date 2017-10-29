using System.IO;
using System.Security.Cryptography;

namespace Aow2.Modding.Internal.Files
{
	interface IStorageStrategy<T>
	{
		void WriteData( Stream outStream, T data );
		T ReadData( Stream inStream );
		HashAlgorithm CrcAlgorithm { get; }
	}
}
