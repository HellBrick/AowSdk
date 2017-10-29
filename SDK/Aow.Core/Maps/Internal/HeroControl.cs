using Aow2.Collections;
using Aow2.Units;

namespace Aow2.Maps.Internal
{
	[AowClass]
	internal class HeroControl
	{
		public HeroControl() => List = new AowList<Hero>();

		[Field( 0x14 )]
		public AowList<Hero> List { get; set; }
	}
}
