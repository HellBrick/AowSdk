using System;
using System.IO;
using System.Linq;
using System.Text;
using Aow2.Serialization;

namespace Aow2
{
	[AowClass( ID = -1 )]
	public class UnknownData : ICustomFormatted, IEquatable<UnknownData>
	{
		public byte[] Data { get; set; }

		#region IEquatable<UnknownData> Members

		public bool Equals( UnknownData other ) => Data.SequenceEqual( other.Data );

		public override bool Equals( object obj )
		{
			UnknownData other = obj as UnknownData;
			if ( other == null )
				return false;

			return Equals( other );
		}

		public override int GetHashCode() => Data.GetHashCode();

		#endregion

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			if ( Data.Length > 0 )
			{
				result.Append( Data[ 0 ].ToString( "x2" ) );
				for ( int i = 1; i < Data.Length; i++ )
					result.AppendFormat( " {0}", Data[ i ].ToString( "x2" ) );
			}
			return result.ToString();
		}

		#region ICustomFormatted Members

		void ICustomFormatted.Serialize( Stream outStream ) => outStream.Write( Data, 0, Data.Length );

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader input = new BinaryReader( inStream );
			Data = input.ReadBytes( (int) length );
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;

		#endregion
	}

	public class UnknownEnum : UnknownData
	{
	}

	public class UnknownPersistent : UnknownData
	{
	}
}
