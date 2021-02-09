using System;
using System.Collections.Generic;
using System.IO;
using Aow2.Serialization.Internal;
using Aow2.Serialization.Internal.Builders.OffsetMap;
using Aow2.Serialization.Logging;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public abstract class OffsetMapContextTest<T>
	{
		#region Common

		protected Type _type;
		private OffsetMapBuilderContext _context;
		private Formatter<T> _formatter;

		[SetUp]
		public void Initialize()
		{
			_type = typeof( T );

			_context = new OffsetMapBuilderContext( _type, Providers );
			_formatter = _context.BuildFormatter() as Formatter<T>;
		}

		internal abstract IEnumerable<IFieldProvider> Providers { get; }
		protected abstract T TestValue { get; }
		protected abstract byte[] TestBytes { get; }

		#endregion

		[Test]
		public void Read()
		{
			using ( MemoryStream stream = new MemoryStream( TestBytes ) )
			{
				T deserialized = _formatter.Deserialize( stream, 0, stream.Length, NoOpSerializationLogger.Instance );
				Assert.AreEqual( TestValue, deserialized );
			}
		}

		[Test]
		public void Write()
		{
			using ( MemoryStream stream = new MemoryStream() )
			{
				_formatter.Serialize( stream, TestValue );
				byte[] serialized = stream.ToArray();

				CollectionAssert.AreEqual( TestBytes, serialized );
			}
		}
	}
}
