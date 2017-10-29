namespace Aow2.Modding.Internal.Files
{
	internal class PfsFile<T> : ModFile<T> where T : new()
	{
		public PfsFile( string filename )
			: base( filename, new PfsStorageStrategy<T>() )
		{
		}
	}
}
