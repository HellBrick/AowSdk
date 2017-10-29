using System;
using System.IO;
using System.Text;

using Aow2.Serialization;

namespace Aow2
{
	public abstract class PascalString : ICustomFormatted, IEquatable<PascalString>, IEquatable<string>
	{
		protected PascalString() => Value = "";

		public string Value { get; set; }

		protected abstract void WriteLength( BinaryWriter out_stream );
		protected abstract int ReadLength( BinaryReader in_stream );
		protected abstract PascalString FromString( string str );

		#region Equality and operators

		public override bool Equals( object obj ) => Equals( obj as PascalString );

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==( PascalString string1, PascalString string2 )
		{
			if ( Object.ReferenceEquals( string1, null ) )
				return Object.ReferenceEquals( string2, null );

			return string1.Equals( string2 );
		}

		public static bool operator !=( PascalString string1, PascalString string2 ) => !( string1 == string2 );

		public override string ToString() => Value;

		public static implicit operator string( PascalString pascal_string ) => pascal_string != null ? pascal_string.Value : null;

		#endregion

		#region ICustomFormatted Members

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );

			WriteLength( writer );
			writer.Write( Encoding.Default.GetBytes( Value ) );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );

			int stringLength = ReadLength( reader );
			byte[] rawString = reader.ReadBytes( stringLength );
			Value = Encoding.Default.GetString( rawString );
		}

		bool ICustomFormatted.ShouldBeOmitted() => String.IsNullOrEmpty( Value );

		#endregion

		#region IEquatable<PascalString> Members

		public bool Equals( PascalString other )
		{
			if ( other == null )
				return false;

			return String.Equals( Value, other.Value );
		}

		#endregion

		#region IEquatable<string> Members

		public bool Equals( string other ) => String.Equals( Value, other );

		#endregion
	}

	public class LongPascalString : PascalString
	{
		public static implicit operator LongPascalString( string str ) => new LongPascalString() { Value = str };

		protected override void WriteLength( BinaryWriter out_stream ) => out_stream.Write( (int) Value.Length );

		protected override int ReadLength( BinaryReader in_stream ) => in_stream.ReadInt32();

		protected override PascalString FromString( string str ) => new LongPascalString() { Value = str };
	}

	public class ShortPascalString : PascalString
	{
		public static implicit operator ShortPascalString( string str ) => new ShortPascalString() { Value = str };

		protected override void WriteLength( BinaryWriter out_stream )
		{
			if ( Value.Length < 0xFF )
				out_stream.Write( (byte) Value.Length );
			else
			{
				out_stream.Write( (byte) 0xff );
				out_stream.Write( (int) Value.Length );
			}
		}

		protected override int ReadLength( BinaryReader in_stream )
		{
			byte first_byte = in_stream.ReadByte();
			return first_byte != 0xff ? first_byte : in_stream.ReadInt32();
		}

		protected override PascalString FromString( string str ) => new ShortPascalString() { Value = str };
	}
}
