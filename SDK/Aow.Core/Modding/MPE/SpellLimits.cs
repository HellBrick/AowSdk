using System.IO;
using Aow2.Serialization;

namespace Aow2.Modding.MPE
{
	public class SpellLimits : ICustomFormatted
	{
		private byte[,] _array = new byte[ 6, 4 ]
		{
			{3, 1, 0, 0},
			{4, 2, 1, 0},
			{5, 3, 2, 0},
			{6, 4, 3, 1},
			{7, 5, 4, 2},
			{0x7F, 0x7F, 0x7F, 0x7F}
		};

		public byte this[ int spherePicks, int spellLevel ]
		{
			get => _array[ spherePicks, spellLevel ];
			set => _array[ spherePicks, spellLevel ] = value;
		}

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			for ( int i = 0; i < 6; i++ )
			{
				for ( int j = 0; j < 4; j++ )
				{
					writer.Write( this[ i, j ] );
				}
			}
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			for ( int i = 0; i < 6; i++ )
			{
				for ( int j = 0; j < 4; j++ )
				{
					this[ i, j ] = reader.ReadByte();
				}
			}
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;
	}
}
