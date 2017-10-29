using System;
using System.Security.Cryptography;

namespace Aow2.Cryptography
{
	/// <summary>
	/// Computes the CRC 32 value for the input data
	/// </summary>
	public class Crc32 : HashAlgorithm
	{
		public Crc32( uint generating_polynom = _defaultPolynom, uint initial = _defaultInitial )
		{
			GeneratingPolynom = generating_polynom;
			Initial = initial;

			Initialize();
		}

		public uint GeneratingPolynom { get; protected set; }
		public uint Initial { get; protected set; }

		private const uint _defaultPolynom = 0x0EDB88320;
		private const uint _defaultInitial = 0xFFFFFFFF;

		protected uint[] CrcTable = new uint[ 256 ];
		protected uint Crc;  //	должно запоминаться между блоками

		#region HashAlgoritm

		protected override void HashCore( byte[] array, int ibStart, int cbSize )
		{
			for ( int i = ibStart; i < cbSize; i++ )
				Crc = ( Crc >> 8 ) ^ CrcTable[ array[ i ] ^ Crc & 0xff ];
		}

		protected override byte[] HashFinal() => BitConverter.GetBytes( ~Crc );

		public override void Initialize()
		{
			CrcTable = CalculateTable( GeneratingPolynom );
			Crc = Initial;
		}

		private static uint[] CalculateTable( uint polynom )
		{
			uint[] result = new uint[ 256 ];
			for ( uint i = 0; i < 256; i++ )
			{
				uint entry = i;

				for ( int j = 0; j < 8; j++ )
					if ( ( entry & 1 ) == 1 )
						entry = ( entry >> 1 ) ^ polynom;
					else
						entry = entry >> 1;

				result[ i ] = entry;
			}

			return result;
		}

		#endregion
	}
}
