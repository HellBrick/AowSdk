using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Aow2.Serialization;
using Utils.Text;

namespace Aow2.Collections
{
	[SuppressListSerialization]
	public class IntegerList : List<int>, ICustomFormatted
	{
		public int DefaultValue { get; set; }

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
			//	A dirty hack to fix the idiocy of AirElemental's icon frame table
			while ( this.Any( x => x == DefaultValue ) )
			{
				DefaultValue--;
			}

			BinaryWriter output = new BinaryWriter( outStream );

			output.Write( DefaultValue );
			output.Write( Count );

			foreach ( int value in this )
				output.Write( value );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader input = new BinaryReader( inStream );

			DefaultValue = input.ReadInt32();
			int count = input.ReadInt32();
			while ( Count < count )
			{
				int value = input.ReadInt32();
				if ( value == DefaultValue )
				{
					int repeats = input.ReadInt32();
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

		private static void Trace( Stream inStream, BinaryReader input, long length )
		{
			byte[] bytes = input.ReadBytes( (int) length );
			string line = new StringBuilder().AppendCollection( bytes.Select( b => b.ToString( "x2" ) ), " " ).ToString();
			Debug.WriteLine( line );

			inStream.Position -= length;
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;

		#endregion
	}
}
