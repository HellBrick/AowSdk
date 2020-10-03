using System.Security;

namespace Utils.Collections
{
	static class ArrayExtensions
	{
		[SecuritySafeCritical]
		public unsafe static void Fill( this byte[,] arr, byte value )
		{
			fixed ( byte* ptr = &arr[0, 0] )
			{
				for ( int i = 0; i < arr.Length; i++ )
					*( ptr + i ) = value;
			}
		}

		[SecuritySafeCritical]
		public unsafe static void Fill( this sbyte[,] arr, sbyte value )
		{
			fixed ( sbyte* ptr = &arr[0, 0] )
			{
				for ( int i = 0; i < arr.Length; i++ )
					*( ptr + i ) = value;
			}
		}

		[SecuritySafeCritical]
		public unsafe static void Fill( this short[,] arr, short value )
		{
			fixed ( short* ptr = &arr[0, 0] )
			{
				for ( int i = 0; i < arr.Length; i++ )
					*( ptr + i ) = value;
			}
		}

		[SecuritySafeCritical]
		public unsafe static void Fill( this ushort[,] arr, ushort value )
		{
			fixed ( ushort* ptr = &arr[0, 0] )
			{
				for ( int i = 0; i < arr.Length; i++ )
					*( ptr + i ) = value;
			}
		}
	}
}
