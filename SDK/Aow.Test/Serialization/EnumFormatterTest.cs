using System;
using System.IO;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Serialization.Logging;
using Aow2.Test.Serialization.Mocks;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class EnumFormatterTest
	{
		#region Common

		private static IFormatterBuilder _builder;
		private static Formatter<EnumMock> _formatter;
		private static EnumMock _value = EnumMock.Death;

		[OneTimeSetUp]
		public static void ClassInitialize()
		{
			_builder = new EnumFormatterBuilder();
			_formatter = _builder.Create( typeof( EnumMock ) ) as Formatter<EnumMock>;
		}

		#endregion

		[Test]
		public void CanBuild() => Assert.IsTrue( _builder.CanCreate( new FormatterRequest( typeof( EnumMock ), false ) ) );

		[Test]
		public void Read()
		{
			byte[] bytes = BitConverter.GetBytes( (int) _value );
			using ( MemoryStream stream = new MemoryStream( bytes ) )
			{
				EnumMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				Assert.AreEqual( _value, deserialized );
			}
		}

		[Test]
		public void Write()
		{
			byte[] bytes = BitConverter.GetBytes( (int) _value );
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _value );
				byte[] serialized = stream.ToArray();

				CollectionAssert.AreEqual( bytes, serialized );
			}
		}
	}
}
