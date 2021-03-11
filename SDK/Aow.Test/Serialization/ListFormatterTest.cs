using System;
using System.IO;
using Aow.Test.Serialization.Resources;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Serialization.Logging;
using Aow2.Test.Serialization.Mocks;
using FluentAssertions;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class ListFormatterTest
	{
		#region Common

		private IFormatterBuilder _builder;
		private Formatter<ListMock> _formatter;

		private ListMock _simpleList = new ListMock(
			new ListItemMock() { I33 = 0x42 },
			new ListItemMock() { I33 = 0x43 } );

		private ListMock _polymorphList = new ListMock(
			new ListItemMock() { I33 = 0x42 },
			new DerivedListItemMock() { I33 = 0x43, I44 = 0x65 } );

		[SetUp]
		public void Initialize()
		{
			_builder = new OffsetMapFormatterBuilder();
			_formatter = _builder.Create( typeof( ListMock ) ) as Formatter<ListMock>;
		}

		#endregion

		[Test]
		public void CanCreate()
		{
			Type type = typeof( ListMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : false );
			Assert.IsTrue( _builder.CanCreate( request ) );
		}

		[Test]
		public void CanNotCreateIfSuppressed()
		{
			Type type = typeof( SuppressedListMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : false );
			Assert.IsFalse( _builder.CanCreate( request ) );
		}

		[Test]
		public void CanNotCreateIfPolymorph()
		{
			Type type = typeof( ListMock );
			FormatterRequest request = new FormatterRequest( type, isPolymorph : true );
			Assert.IsFalse( _builder.CanCreate( request ) );
		}

		[Test]
		public void SimpleRead()
		{
			using ( MemoryStream stream = new MemoryStream( Files.ListItem ) )
			{
				ListMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				CollectionAssert.AreEqual( _simpleList.List, deserialized.List );
			}
		}

		[Test]
		public void SimpleWrite()
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _simpleList );
				byte[] serialized = stream.ToArray();
				CollectionAssert.AreEqual( Files.ListItem, serialized );
			}
		}

		[Test]
		public void PolymorphRead()
		{
			using ( MemoryStream stream = new MemoryStream( Files.PolymorphListItem ) )
			{
				ListMock deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				CollectionAssert.AreEqual( _polymorphList.List, deserialized.List );
			}
		}

		[Test]
		public void PolymorphWrite()
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, _polymorphList );
				byte[] serialized = stream.ToArray();
				CollectionAssert.AreEqual( Files.PolymorphListItem, serialized );
			}
		}

		[Test]
		public void ListWithLeadingNullsRoundTrips()
		{
			ListMock original = new ListMock( null, null, null, new ListItemMock() { I33 = 0x42 } );

			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, original );

				stream.Position = 0;
				ListMock roundTripped = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );

				roundTripped.Should().BeEquivalentTo( original );
			}
		}
	}
}
