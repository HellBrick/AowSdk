using System;

namespace Utils.Graphics
{
	public class UInt16StrideArray : StrideArray<ushort>
	{
		public UInt16StrideArray( int width, int height )
			: base( width, height, 8 * sizeof( ushort ) )
		{
		}

		protected override unsafe void FixBufferAndExecute( Action<IntPtr> action )
		{
			fixed ( void* ptr = &OutBuffer[ 0, 0 ] )
			{
				action( new IntPtr( ptr ) );
			}
		}
	}

	public class ByteStrideArray : StrideArray<byte>
	{
		public ByteStrideArray( int width, int height )
			: base( width, height, 8 * sizeof( byte ) )
		{
		}

		protected override unsafe void FixBufferAndExecute( Action<IntPtr> action )
		{
			fixed ( void* ptr = &OutBuffer[ 0, 0 ] )
			{
				action( new IntPtr( ptr ) );
			}
		}
	}
}
