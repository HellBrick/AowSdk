using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders;
using Aow2.Test.Serialization.Mocks;
using System.IO;
using Aow.Test.Serialization.Resources;
using Aow2.Serialization.Logging;

namespace Aow2.Test.Serialization
{
	[TestClass]
	public class PlainClassFormatterTest
	{
		#region Common

		private static IFormatterBuilder _builder;
		private static Formatter<PlainClassMock> _plainClassFormatter;
		private static Formatter<ImportOnlyClassMock> _importOnlyFormatter;
		private static Formatter<ClassWithAbstractField> _abstractFieldFormatter;

		private PlainClassMock _trueObject = new PlainClassMock()
		{
			FieldA = 0x10,
			FieldB = true,
			FieldC = 0x12,
			FieldD = 0x13
		};

		private PlainClassMock _falseObject = new PlainClassMock()
		{
			FieldA = 0x10,
			FieldB = false,
			FieldC = 0x12,
			FieldD = 0x13
		};

		private ImportOnlyClassMock _importOnlyObject = new ImportOnlyClassMock()
		{
			ImportOnly = 0x99
		};

		private ClassWithAbstractField _abstractFieldValue = new ClassWithAbstractField()
		{
			Field = new DerivedTwo()
			{
				A0 = 0x42,
				B0 = 0x53,
				C0 = 0x64
			}
		};

		[ClassInitialize]
		public static void ClassInitialize( TestContext context )
		{
			_builder = new OffsetMapFormatterBuilder();
			_plainClassFormatter = _builder.Create( typeof( PlainClassMock ) ) as Formatter<PlainClassMock>;
			_importOnlyFormatter = _builder.Create( typeof( ImportOnlyClassMock ) ) as Formatter<ImportOnlyClassMock>;
			_abstractFieldFormatter = _builder.Create( typeof( ClassWithAbstractField ) ) as Formatter<ClassWithAbstractField>;
		}

		#endregion

		[TestMethod]
		public void CircularTypeReference()
		{
			try
			{
				IFormatter formatter = _builder.Create( typeof( Circular ) );
			}
			catch ( StackOverflowException )
			{
				Assert.Fail( "Stack overflow when resolving a circular type reference." );
			}
		}

		[TestMethod]
		public void CanCreate()
		{
			bool canCreate = _builder.CanCreate( new FormatterRequest( typeof( PlainClassMock ), isPolymorph : false ) );
			Assert.IsTrue( canCreate, "Builder failed to recognize class." );
		}

		[TestMethod]
		public void Read()
		{
			PlainClassMock expected = _trueObject;

			using ( MemoryStream memory = new MemoryStream( Files.PlainClass ) )
			{
				PlainClassMock deserialized = _plainClassFormatter.Deserialize( memory, 0L, 0L, NoOpSerializationLogger.Instance );
				Assert.AreEqual( expected, deserialized );
			}
		}

		[TestMethod]
		public void Write()
		{
			byte[] expected = Files.PlainClass;

			using ( MemoryStream memory = new MemoryStream() )
			{
				_plainClassFormatter.Serialize( memory, _trueObject );
				CollectionAssert.AreEqual( expected, memory.ToArray() );
			}
		}

		[TestMethod]
		public void ReadWithFieldSkipped()
		{
			PlainClassMock expected = _falseObject;

			using ( MemoryStream memory = new MemoryStream( Files.PlainClassFalse ) )
			{
				PlainClassMock deserialized = _plainClassFormatter.Deserialize( memory, 0L, 0L, NoOpSerializationLogger.Instance );
				Assert.AreEqual( expected, deserialized );
			}
		}

		[TestMethod]
		public void WriteWithFieldSkipped()
		{
			byte[] expected = Files.PlainClassFalse;

			using ( MemoryStream memory = new MemoryStream() )
			{
				_plainClassFormatter.Serialize( memory, _falseObject );
				CollectionAssert.AreEqual( expected, memory.ToArray() );
			}
		}

		[TestMethod]
		public void NullIsSkipped() => Assert.IsTrue( _plainClassFormatter.ShouldSkipField( null ) );

		[TestMethod]
		public void ImportOnlyIsSkippedWhenWriting()
		{
			byte[] expected = Files.ImportOnlyClass;
			using ( MemoryStream memory = new MemoryStream() )
			{
				_importOnlyFormatter.Serialize( memory, _importOnlyObject );
				CollectionAssert.AreEqual( expected, memory.ToArray() );
			}
		}

		[TestMethod]
		public void AbstractFieldRead()
		{
			ClassWithAbstractField expected = _abstractFieldValue;

			using ( MemoryStream memory = new MemoryStream( Files.ClassWithAbstractField ) )
			{
				ClassWithAbstractField deserialized = _abstractFieldFormatter.Deserialize( memory, 0L, 0L, NoOpSerializationLogger.Instance );
				Assert.AreEqual( expected, deserialized );
			}
		}

		[TestMethod]
		public void AbstractFieldWrite()
		{
			byte[] expected = Files.ClassWithAbstractField;

			using ( MemoryStream memory = new MemoryStream() )
			{
				_abstractFieldFormatter.Serialize( memory, _abstractFieldValue );
				CollectionAssert.AreEqual( expected, memory.ToArray() );
			}
		}
	}
}
