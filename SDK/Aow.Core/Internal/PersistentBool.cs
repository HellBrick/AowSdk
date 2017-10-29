using System.IO;
using Aow2.Serialization;

namespace Aow2
{
	/// <summary>
	/// Represents an odd case of Aow's bool field, when default value is true and the field must not be omitted.
	/// </summary>
	public struct PersistentBool : ICustomFormatted
	{
		private bool _value;

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			writer.Write( _value );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			_value = reader.ReadBoolean();
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;

		public static implicit operator bool( PersistentBool value ) => value._value;

		public static implicit operator PersistentBool( bool value ) => new PersistentBool() { _value = value };
	}
}
