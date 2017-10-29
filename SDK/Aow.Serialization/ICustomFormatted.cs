using System.IO;

namespace Aow2.Serialization
{
	public interface ICustomFormatted
	{
		void Serialize( Stream outStream );
		void Deserialize( Stream inStream, long length );
		bool ShouldBeOmitted();
	}
}
