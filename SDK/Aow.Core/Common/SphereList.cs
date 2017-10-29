using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Aow2.Serialization;

namespace Aow2
{
	public class SphereList : ICustomFormatted
	{
		public SphereList()
		{
		}

		private byte[] _list = new byte[ 6 ];

		public Sphere this[ int index ]
		{
			get
			{
				ValidateIndexRange( index );
				return (Sphere) _list[ index ];
			}
			set => _list[ index ] = (byte) value;
		}

		public int SphereCount( Sphere sphere ) => _list.Where( b => b == (byte) sphere ).Count();

		private void ValidateIndexRange( int index )
		{
			if ( ( index < 0 ) || ( index > 5 ) )
				throw new ArgumentOutOfRangeException( "index", "Index in SphereList must be 0..5" );
		}

		public override string ToString()
		{
			IEnumerable<Sphere> uniqueSpheres = _list.Distinct().Cast<Sphere>();
			StringBuilder result = new StringBuilder();

			Sphere first = uniqueSpheres.First();
			result.AppendFormat( "{0} x{1}", first, SphereCount( first ) );

			foreach ( Sphere sphere in uniqueSpheres.Skip( 1 ) )
				result.AppendFormat( ", {0} x{1}", sphere, SphereCount( sphere ) );

			return result.ToString();
		}

		#region ICustomFormatted Members

		void ICustomFormatted.Serialize( Stream outStream )
		{
			for ( int i = 0; i < 6; i++ )
				outStream.Write( _list, 0, 6 );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			_list = reader.ReadBytes( 6 );
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;

		#endregion
	}
}
