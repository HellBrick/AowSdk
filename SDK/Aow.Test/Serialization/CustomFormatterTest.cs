using System;
using System.IO;
using System.Text;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Serialization.Logging;
using Aow2.Test.Serialization.Mocks;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class CustomFormatterTest
	{
		#region Common

		private static IFormatterBuilder _builder;
		private static Formatter<CustomFormatterMock> _formatter;
		private static CustomFormatterMock _value = new CustomFormatterMock() { Value = "Fourty-two" };

		[OneTimeSetUp]
		public static void ClassInitialize()
		{
			_builder = new CustomFormatterBuilder();
			_formatter = _builder.Create( typeof( CustomFormatterMock ) ) as Formatter<CustomFormatterMock>;
		}

		#endregion

		[Test]
		public void CanBuild() => Assert.IsTrue( _builder.CanCreate( new FormatterRequest( typeof( CustomFormatterMock ), isPolymorph: false ) ) );

		[Test]
		public void Read()
		{
			byte[] bytes = Encoding.ASCII.GetBytes( _value.Value );
			using ( MemoryStream stream = new MemoryStream( bytes ) )
			{
				CustomFormatterMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				Assert.AreEqual( _value, deserialized );
			}
		}

		[Test]
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

		[Test]
		public void ShouldSkip()
		{
			CustomFormatterMock skipped = new CustomFormatterMock() { Value = String.Empty };
			Assert.IsTrue( _formatter.ShouldSkipField( skipped ) );
		}

		[Test]
		public void ShouldSkipStruct()
		{
			Formatter<CustomFormattedStruct> formatter = _builder.Create( typeof( CustomFormattedStruct ) ) as Formatter<CustomFormattedStruct>;
			Assert.IsFalse( formatter.ShouldSkipField( new CustomFormattedStruct() ) );
		}
	}
}
