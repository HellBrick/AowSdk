using System;
using System.IO;
using Aow.Test.Serialization.Resources;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Test.Serialization.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aow2.Test.Serialization
{
	[TestClass]
	public class DicitonaryFormatterTest
	{
		#region Common

		private IFormatterBuilder _builder;
		private Formatter<DictionaryMock> _formatter;

		private DictionaryMock _simpleList = new DictionaryMock()
		{
			{ 0x21, new ListItemMock() { AA = 0x42 } },
			{ 0x24, new ListItemMock() { AA = 0x43 } }
		};

		private DictionaryMock _polymorphList = new DictionaryMock()
		{
			{ 0x21, new ListItemMock() { AA = 0x42 } },
			{ 0x24, new DerivedListItemMock() { AA = 0x43, BB = 0x65 } }
		};

		[TestInitialize]
		public void Initialize()
		{
			_builder = new OffsetMapFormatterBuilder();
			_formatter = _builder.Create( typeof( DictionaryMock ) ) as Formatter<DictionaryMock>;
		}

		#endregion

		[TestMethod]
		public void CanCreate()
		{
			Type type = typeof( DictionaryMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : false );
			Assert.IsTrue( _builder.CanCreate( request ) );
		}

		[TestMethod]
		public void CanNotCreateIfSuppressed()
		{
			Type type = typeof( SuppressedDictionaryMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : false );
			Assert.IsFalse( _builder.CanCreate( request ) );
		}

		[TestMethod]
		public void CanNotCreateIfPolymorph()
		{
			Type type = typeof( DictionaryMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : true );
			Assert.IsFalse( _builder.CanCreate( request ) );
		}

		[TestMethod]
		public void SimpleRead()
		{
			using ( MemoryStream stream = new MemoryStream( Files.Dictionary ) )
			{
				DictionaryMock deserialized = _formatter.Deserialize( stream, 0, stream.Length );
				CollectionAssert.AreEqual( _simpleList.Dictionary, deserialized.Dictionary );
			}
		}

		[TestMethod]
		public void SimpleWrite()
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _simpleList );
				byte[] serialized = stream.ToArray();
				CollectionAssert.AreEqual( Files.Dictionary, serialized );
			}
		}

		[TestMethod]
		public void PolymorphRead()
		{
			using ( MemoryStream stream = new MemoryStream( Files.PolymorphDictionary ) )
			{
				DictionaryMock deserialized = _formatter.Deserialize( stream, 0, stream.Length );
				CollectionAssert.AreEqual( _polymorphList.Dictionary, deserialized.Dictionary );
			}
		}

		[TestMethod]
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
