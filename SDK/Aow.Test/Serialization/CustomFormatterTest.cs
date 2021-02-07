using System;
using System.IO;
using System.Text;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Serialization.Logging;
using Aow2.Test.Serialization.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aow2.Test.Serialization
{
	[TestClass]
	public class CustomFormatterTest
	{
		#region Common

		private static IFormatterBuilder _builder;
		private static Formatter<CustomFormatterMock> _formatter;
		private static CustomFormatterMock _value = new CustomFormatterMock() { Value = "Fourty-two" };

		[ClassInitialize]
		public static void ClassInitialize( TestContext context )
		{
			_builder = new CustomFormatterBuilder();
			_formatter = _builder.Create( typeof( CustomFormatterMock ) ) as Formatter<CustomFormatterMock>;
		}

		#endregion

		[TestMethod]
		public void CanBuild() => Assert.IsTrue( _builder.CanCreate( new FormatterRequest( typeof( CustomFormatterMock ), isPolymorph: false ) ) );

		[TestMethod]
		public void Read()
		{
			byte[] bytes = Encoding.ASCII.GetBytes( _value.Value );
			using ( MemoryStream stream = new MemoryStream( bytes ) )
			{
				CustomFormatterMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				Assert.AreEqual( _value, deserialized );
			}
		}

		[TestMethod]
		public void Write()
		{
			byte[] bytes = Encoding.ASCII.GetBytes( _value.Value );
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _value );
				byte[] serialized = stream.ToArray();

				CollectionAssert.AreEqual( bytes, serialized );
			}
		}

		[TestMethod]
		public void ShouldSkip()
		{
			CustomFormatterMock skipped = new CustomFormatterMock() { Value = String.Empty };
			Assert.IsTrue( _formatter.ShouldSkipField( skipped ) );
		}

		[TestMethod]
		public void ShouldSkipStruct()
		{
			Formatter<CustomFormattedStruct> formatter = _builder.Create( typeof( CustomFormattedStruct ) ) as Formatter<CustomFormattedStruct>;
			Assert.IsFalse( formatter.ShouldSkipField( new CustomFormattedStruct() ) );
		}
	}
}
