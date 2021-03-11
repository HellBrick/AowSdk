using System;
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
	public class DicitonaryFormatterTest
	{
		#region Common

		private IFormatterBuilder _builder;
		private Formatter<DictionaryMock> _formatter;

		private DictionaryMock _simpleList = new DictionaryMock()
		{
			{ 0x21, new ListItemMock() { I33 = 0x42 } },
			{ 0x24, new ListItemMock() { I33 = 0x43 } }
		};

		private DictionaryMock _polymorphList = new DictionaryMock()
		{
			{ 0x21, new ListItemMock() { I33 = 0x42 } },
			{ 0x24, new DerivedListItemMock() { I33 = 0x43, I44 = 0x65 } }
		};

		[SetUp]
		public void Initialize()
		{
			_builder = new OffsetMapFormatterBuilder();
			_formatter = _builder.Create( typeof( DictionaryMock ) ) as Formatter<DictionaryMock>;
		}

		#endregion

		[Test]
		public void CanCreate()
		{
			Type type = typeof( DictionaryMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : false );
			Assert.IsTrue( _builder.CanCreate( request ) );
		}

		[Test]
		public void CanNotCreateIfSuppressed()
		{
			Type type = typeof( SuppressedDictionaryMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : false );
			Assert.IsFalse( _builder.CanCreate( request ) );
		}

		[Test]
		public void CanNotCreateIfPolymorph()
		{
			Type type = typeof( DictionaryMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : true );
			Assert.IsFalse( _builder.CanCreate( request ) );
		}

		[Test]
		public void SimpleRead()
		{
			using ( MemoryStream stream = new MemoryStream( Files.Dictionary ) )
			{
				DictionaryMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				CollectionAssert.AreEqual( _simpleList.Dictionary, deserialized.Dictionary );
			}
		}

		[Test]
		public void SimpleWrite()
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _simpleList );
				byte[] serialized = stream.ToArray();
				CollectionAssert.AreEqual( Files.Dictionary, serialized );
			}
		}

		[Test]
		public void PolymorphRead()
		{
			using ( MemoryStream stream = new MemoryStream( Files.PolymorphDictionary ) )
			{
				DictionaryMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				CollectionAssert.AreEqual( _polymorphList.Dictionary, deserialized.Dictionary );
			}
		}

		[Test]
		public void PolymorphWrite()
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _polymorphList );
				byte[] serialized = stream.ToArray();
				CollectionAssert.AreEqual( Files.PolymorphDictionary, serialized );
			}
		}
	}
}
