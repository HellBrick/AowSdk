using System.Text;

namespace Aow2.Cryptography
{
	public static class PasswordCryptor
	{
		public static string Encode( string password )
		{
			byte[] pass = Encoding.Default.GetBytes( password );
			return Encode( pass );
		}

		public static string Decode( string encryptedPassword ) => Encode( encryptedPassword );

		public static string Encode( byte[] password )
		{
			byte[] result = new byte[ password.Length ];

			int starting_chaos_level = password.Length - 2;
			for ( int i = 0; i < password.Length; ++i )
				result[ i ] = CryptByte( password[ i ], starting_chaos_level + i );

			return Encoding.Default.GetString( result );
		}

		private static byte[] CryptTable( int chaos_level )
		{
			byte[] table = new byte[ 256 ];
			for ( int i = 0; i < 256; ++i )
				table[ i ] = (byte) i;

			if ( chaos_level == -1 )
			{
				for ( int i = 0; i < 256; ++i )
				{
					int group = i / 8;
					int number = i % 8;
					table[ i ] = (byte) ( ( group + 1 ) * 8 - ( number + 1 ) );
				}
			}
			else
			{
				if ( ( chaos_level & 8 ) == 0 )
					SwapBlocks( table, 8 );

				for ( int i = 0; i < 8; ++i )
				{
					if ( ( chaos_level & ( 1 << i ) ) != 0 )
					{
						int swap_block_size = SwapBlockSize( i );
						SwapBlocks( table, swap_block_size );
					}
				}
			}

			return table;
		}

		private static void SwapBlocks( byte[] table, int swap_group_size )
		{
			for ( int i = 0; i < 256; i += 2 * swap_group_size )
				for ( int j = 0; j < swap_group_size; ++j )
					SwapCode( table, i + j, i + j + swap_group_size );
		}

		private static void SwapCode( byte[] table, int x, int y )
		{
			byte temp = table[ x ];
			table[ x ] = table[ y ];
			table[ y ] = temp;
		}

		private static byte CryptByte( byte character, int chaos_level )
		{
			byte[] crypt_table = CryptTable( chaos_level );
			return crypt_table[ character ];
		}

		private static int SwapBlockSize( int bit_number )
		{
			if ( bit_number != 3 )
				return 1 << bit_number;
			else
				return 16;
		}
	}
}
