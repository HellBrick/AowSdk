namespace Aow2.Cryptography
{
	/// <summary>
	/// Computes the CRC value for .pfs files
	/// </summary>
	public class CrcPfs : Crc32
	{
		public override void Initialize()
		{
			base.Initialize();
			_crc2 = _initial2;
		}

		uint _crc2;

		protected override void HashCore( byte[] array, int ibStart, int cbSize )
		{
			for ( int i = ibStart; i < cbSize; i++ )
			{
				Crc = ( Crc >> 8 ) ^ CrcTable[ array[ i ] ^ Crc & 0xff ];
				_crc2 = ( _crc2 >> 8 ) ^ CrcTable[ _crc2 & 0xff ];
			}
		}

		protected override byte[] HashFinal()
		{
			Crc = Crc ^ _crc2;
			return base.HashFinal();
		}

		private const uint _initial2 = 0x4c9dd581;
	}
}
