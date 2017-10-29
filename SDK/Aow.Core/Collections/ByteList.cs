using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Aow2.Serialization;

namespace Aow2.Collections
{
	[SuppressListSerialization]
	internal class ByteList : List<byte>, ICustomFormatted
	{
		public byte DefaultValue { get; set; }

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat( "[Count = {0}, Default = {1}", Count, DefaultValue );
			if ( Count > 0 )
				result.AppendFormat( ", Elems: {0}", this.First() );

			foreach ( int value in this.Skip( 1 ) )
				result.AppendFormat( ", {0}", value );

			result.Append( "]" );
			return result.ToString();
		}

		#region ICustomFormatted Members

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter output = new BinaryWriter( outStream );

			output.Write( DefaultValue );
			output.Write( Count );

			foreach ( byte value in this )
				output.Write( value );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader input = new BinaryReader( inStream );

			DefaultValue = input.ReadByte();
			int count = input.ReadInt32();
			while ( Count < count )
			{
				byte value = input.ReadByte();
				if ( value == DefaultValue )
				{
					byte repeats = input.ReadByte();
					for ( int j = 0; j < repeats; j++ )
					{
						Add( value );
					}
				}
				else
				{
					Add( value );
				}
			}
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;

		#endregion
	}
}
