using Aow2.Collections;

namespace Aow2.Items
{
	[AowClass]
	public class ItemLibrary : AowDictionary<Item>
	{
		public string Name
		{
			get => _name;
			set => _name = value;
		}

		[Field( 0x14 )] private ShortPascalString _name = "";
		[Field( 0x15 )] public int u_15;
	}
}
