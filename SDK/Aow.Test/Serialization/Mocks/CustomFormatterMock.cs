using System;
using System.IO;
using System.Text;
using Aow2.Serialization;

namespace Aow2.Test.Serialization.Mocks
{
	class CustomFormatterMock: ICustomFormatted, IEquatable<CustomFormatterMock>
	{
		public string Value { get; set; }

		#region ICustomFormatted Members

		public void Serialize( System.IO.Stream outStream )
		{
			StreamWriter writer = new StreamWriter( outStream, Encoding.ASCII ) { AutoFlush = true };
			writer.Write( Value );
		}

		public void Deserialize( System.IO.Stream inStream, long length )
		{
			byte[] bytes = new byte[length];
			inStream.Read( bytes, 0, (int) length );
			Value = Encoding.ASCII.GetString( bytes );
		}

		public bool ShouldBeOmitted() => String.IsNullOrEmpty( Value );

		#endregion

		#region IEquatable<CustomFormatterMock> Members

		public bool Equals( CustomFormatterMock other )
		{
			if ( other == null )
				return false;

			return Value.Equals( other.Value );
		}

		public override bool Equals( object obj ) => Equals( obj as CustomFormatterMock );

		public override int GetHashCode() => Value.GetHashCode();

		#endregion
	}
}
