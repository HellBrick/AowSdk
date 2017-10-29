using System.IO;
using System.Security.Cryptography;
using Aow2.Cryptography;
using Aow2.Serialization;

namespace Aow2.Modding.Internal.Files
{
	class PfsStorageStrategy<T> : IStorageStrategy<T>
	{
		private static readonly byte[] _signature = { 0x1C, 0xDF, 0x44, 0x21 };
		private static readonly int _signatureLength = _signature.Length;

		private AowSerializer<T> _serializer = new AowSerializer<T>( hasRootWrapper: true );

		public void WriteData( Stream outStream, T data )
		{
			outStream.Write( _signature, 0, _signatureLength );
			_serializer.Serialize( outStream, data );
		}

		public T ReadData( Stream inStream ) => _serializer.Deserialize( inStream, _signatureLength, inStream.Length - 4 - _signatureLength );

		private HashAlgorithm _hash = new CrcPfs();
		public HashAlgorithm CrcAlgorithm => _hash;
	}
}
