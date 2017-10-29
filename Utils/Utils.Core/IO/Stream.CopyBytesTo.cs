using System.IO;

namespace Utils.IO
{
	public static class StreamExtensions
	{
		public static void CopyBytesTo( this Stream source, Stream destination, int count )
		{
			byte[] bytes = new byte[count];
			source.Read( bytes, 0, count );
			destination.Write( bytes, 0, count );
		}
	}
}
