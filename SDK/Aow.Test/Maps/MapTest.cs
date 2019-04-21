using Aow.Test.Maps.Resources;
using Aow2.Maps;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aow2.Test.Maps
{
	[TestClass]
	public class MapTest
	{
		[TestMethod]
		public void SaveRoundTrips()
		{
			AowMap original = AowMap.FromBytes( MapFiles.Save );
			AowMap roundTripped = AowMap.FromBytes( original.ToBytes() );

			roundTripped.Should().BeEquivalentTo( original );
		}
	}
}
