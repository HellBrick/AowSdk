using System.IO;
using Aow2.Collections;
using Aow2.Serialization;

namespace Aow2.Graphics.FX
{
	public class FXFile
	{
		private AowSerializer<AowList<ParticleEffect>> _serializer = new AowSerializer<AowList<ParticleEffect>>( hasRootWrapper: true );

		public FXFile( string filePath )
		{
			File = filePath;

			using ( FileStream stream = new FileStream( File, FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				Effects = _serializer.Deserialize( stream );
			}
		}

		public string File { get; private set; }
		public AowList<ParticleEffect> Effects { get; private set; }

		public void SaveToFile( string filePath )
		{
			using ( FileStream stream = new FileStream( filePath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				SaveToStream( stream );
			}
		}

		public void SaveToStream( Stream stream ) => _serializer.Serialize( stream, Effects );
	}
}
