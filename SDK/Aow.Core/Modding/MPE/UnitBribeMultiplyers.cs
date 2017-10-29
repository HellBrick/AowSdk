using System;
using System.IO;
using Aow2.Serialization;

namespace Aow2.Modding.MPE
{
	public class UnitBribeMultiplyers: ICustomFormatted
	{
		private readonly float[] _multiplyers = { 1.25f, 1.25f, 1.5f, 1.5f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f };

		public float this[int unitLevel]
		{
			get
			{
				if ( unitLevel < 0 )
					return _multiplyers[0];

				if ( unitLevel >= _multiplyers.Length )
					return _multiplyers[_multiplyers.Length - 1];

				return _multiplyers[unitLevel];
			}
			set
			{
				if ( unitLevel < 0 || unitLevel >= _multiplyers.Length )
					throw new IndexOutOfRangeException( "This unit level is not supported" );

				_multiplyers[unitLevel] = value;
			}
		}

		public void Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			foreach ( float value in _multiplyers )
				writer.Write( value );
		}

		public void Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			for ( int i = 0; i < length / 4; i++ )
				_multiplyers[i] = reader.ReadSingle();
		}

		public bool ShouldBeOmitted() => false;
	}
}
