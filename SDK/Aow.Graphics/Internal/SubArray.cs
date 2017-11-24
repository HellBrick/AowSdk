namespace Aow2.Graphics.Internal
{
	public unsafe class SubArray
	{
		public SubArray( byte* source, int sourceWidth, int offsetY, int offsetX )
		{
			_subArrayPtr = source + offsetY * sourceWidth + offsetX;
			_rowWidth = sourceWidth;
		}

		private byte* _subArrayPtr;
		private int _rowWidth;

		public byte this[ int y, int x ]
		{
			get => *Offset( x, y );
			set => *Offset( x, y ) = value;
		}

		private byte* Offset( int x, int y ) => _subArrayPtr + y * _rowWidth + x;
	}
}
