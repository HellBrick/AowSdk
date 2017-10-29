using System.IO;
using Aow2.Serialization;

namespace Aow2.Graphics
{
	public class PackedImage : ICustomFormatted
	{
		public AowImage ImageInfo { get; set; }
		public byte[] ImageData { get; set; }

		public void Serialize( Stream outStream )
		{
			AowSerializer<AowImage> serializer = new AowSerializer<AowImage>( hasRootWrapper: false, forceClassID: true );
			serializer.Serialize( outStream, ImageInfo );
			outStream.Write( ImageData, 0, ImageData.Length );
		}

		public void Deserialize( Stream inStream, long length )
		{
			AowSerializer<AowImage> serializer = new AowSerializer<AowImage>( hasRootWrapper: false, forceClassID: true );
			ImageInfo = serializer.Deserialize( inStream, inStream.Position, length );
			ImageData = new BinaryReader( inStream ).ReadBytes( ImageInfo.DataLength );
		}

		public bool ShouldBeOmitted() => false;
	}
}
