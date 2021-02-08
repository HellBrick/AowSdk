using System;
using System.IO;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Serialization.Logging;
using Aow2.Test.Serialization.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aow2.Test.Serialization
{
	[TestClass]
	public class EnumFormatterTest
	{
		#region Common

		private static IFormatterBuilder _builder;
		private static Formatter<EnumMock> _formatter;
		private static EnumMock _value = EnumMock.Death;

		[ClassInitialize]
		public static void ClassInitialize( TestContext context )
		{
			_builder = new EnumFormatterBuilder();
			_formatter = _builder.Create( typeof( EnumMock ) ) as Formatter<EnumMock>;
		}

		#endregion

		[TestMethod]
		public void CanBuild() => Assert.IsTrue( _builder.CanCreate( new FormatterRequest( typeof( EnumMock ), false ) ) );

		[TestMethod]
		public void Read()
		{
			byte[] bytes = BitConverter.GetBytes( (int) _value );
			using ( MemoryStream stream = new MemoryStream( bytes ) )
			{
				EnumMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				Assert.AreEqual( _value, deserialized );
			}
		}

		[TestMethod]
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
