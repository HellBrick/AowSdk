namespace Aow2.Modding.Internal.Files
{
	internal class AhrFile<T> : ModFile<T> where T : new()
	{
		public AhrFile( string filename )
			: base( filename, new AhrStorageStrategy<T>() )
		{
		}
	}
}
