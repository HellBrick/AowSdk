using System.Collections.Generic;
using Aow.Test.Serialization.Resources;
using Aow2.Serialization.Internal.Builders.OffsetMap;
using Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders;
using Aow2.Test.Serialization.Mocks;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class DictionaryClassTest: OffsetMapContextTest<DictionaryClassMock>
	{
		internal override IEnumerable<IFieldProvider> Providers
		{
			get
			{
				yield return new DictionaryFieldProvider( typeof( DictionaryClassMock ) );
				yield return new ClassFieldProvider( typeof( DictionaryClassMock ) );
			}
		}

		protected override DictionaryClassMock TestValue
		{
			get
			{
				DictionaryClassMock value = new DictionaryClassMock();
				value.Field0A = 0xBB;
				value.Add( 0x00, 0x42 );
				value.Add( 0x10, 0x53 );
				value.Add( 0x20, 0x64 );

				return value;
			}
		}

		protected override byte[] TestBytes => Files.DictionaryClass;
	}
}
