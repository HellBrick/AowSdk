using System.IO;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Logging;

namespace Aow2.Serialization
{
	public class AowSerializer<T>
	{
		private bool _isAbstract;
		private bool _hasRootWrapper;

		private IFormatter<T> _formatter;

		public AowSerializer()
			: this( hasRootWrapper : false, forceClassID : false )
		{
		}

		public AowSerializer( bool hasRootWrapper )
			: this( hasRootWrapper : hasRootWrapper, forceClassID : false )
		{
		}

		public AowSerializer( bool hasRootWrapper, bool forceClassID )
		{
			_isAbstract = typeof( T ).IsAbstract || forceClassID;
			_hasRootWrapper = hasRootWrapper;

			_formatter = FormatterManager.GetFormatter<T>( isPolymorph : _isAbstract );

			if ( _hasRootWrapper )
				_formatter = new WrappingFormatterProxy( _formatter );
		}

		public T Deserialize( Stream inStream ) => Deserialize( inStream, NoOpSerializationLogger.Instance );

		public T Deserialize( Stream inStream, ISerializationLogger logger ) => Deserialize( inStream, 0, inStream.Length, logger );

		public T Deserialize( Stream inStream, long offset, long length ) => Deserialize( inStream, offset, length, NoOpSerializationLogger.Instance );

		public T Deserialize( Stream inStream, long offset, long length, ISerializationLogger logger ) => _formatter.Deserialize( inStream, offset, length, logger );

		public void Serialize( Stream outStream, T value ) => _formatter.Serialize( outStream, value );

		private class WrappingFormatterProxy: IFormatter<T>
		{
			private static readonly byte[] _wrapperBytes = { 0x01, 0x00, 0x00 };
			private IFormatter<T> _wrappedFormatter;

			public WrappingFormatterProxy( IFormatter<T> formatter ) => _wrappedFormatter = formatter;

			public void Serialize( Stream outStream, T value )
			{
				outStream.Write( _wrapperBytes, 0, _wrapperBytes.Length );
				_wrappedFormatter.Serialize( outStream, value );
			}

			public T Deserialize( Stream inStream, long offset, long length, ISerializationLogger logger ) => _wrappedFormatter.Deserialize( inStream, offset + _wrapperBytes.Length, length - _wrapperBytes.Length, logger );
		}
	}
}
