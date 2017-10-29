using Aow2.Modding.Collections;

namespace Aow2.Modding
{
	[AowClass]
	public abstract class EResource : IResourceItem
	{
		[Field( 0x0c )] public int ID { get; set; }

		[Field( 0x0a )] public int _int0a;
		[Field( 0x0b )] public int _int0b;
	}
}
