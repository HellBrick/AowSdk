using System.IO;
using Aow.Test.Serialization.Resources;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Serialization.Logging;
using Aow2.Test.Serialization.Mocks;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class PolymorphFormatterTest
	{
		#region Common

		private IFormatterBuilder _builder = new PolymorphClassFormatterBuilder();
		private Formatter<AbstractMock> _formatter;

		private DerivedOne _derivedOne = new DerivedOne() { A0 = 0x42, B0 = 0x53 };

		[SetUp]
		public void Initialize() => _formatter = _builder.Create( typeof( AbstractMock ) ) as Formatter<AbstractMock>;

		#endregion

		[Test]
		public void Write()
		{
			using ( MemoryStream memory = new MemoryStream() )
			{
				_formatter.Serialize( memory, _derivedOne );
				CollectionAssert.AreEqual( Files.DerivedOne, memory.ToArray() );
			}
		}

		[Test]
		public void Read()
		{
			using ( MemoryStream memory = new MemoryStream( Files.DerivedOne ) )
			{
				AbstractMock deserialized = _formatter.Deserialize( memory, 0, memory.Length, NoOpSerializationLogger.Instance );
				Assert.AreEqual( _derivedOne, deserialized );
			}
		}
	}
}
