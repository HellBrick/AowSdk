using System.IO;
using Aow2.Serialization;

namespace Aow2.Modding.MPE
{
	public abstract class RaceDictionary<T> : ICustomFormatted
	{
		private const int _raceCount = 15;
		private T[] _array;

		protected RaceDictionary() => _array = DefaultValues;

		public T this[ Race race ]
		{
			get
			{
				if ( race == Race.None )
					return NullValue;

				return _array[ (int) race ];
			}
			set
			{
				if ( race != Race.None )
					_array[ (int) race ] = value;
			}
		}

		protected abstract T[] DefaultValues { get; }

		/// <summary>
		/// Returned for <see cref="Race.None"/>.
		/// </summary>
		protected abstract T NullValue { get; }

		protected abstract void WriteValue( BinaryWriter writer, T value );
		protected abstract T ReadValue( BinaryReader reader );

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			foreach ( T value in _array )
				WriteValue( writer, value );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			for ( int i = 0; i < _raceCount; i++ )
				_array[ i ] = ReadValue( reader );
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;
	}
}
