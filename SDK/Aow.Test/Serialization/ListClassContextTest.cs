using System.Collections.Generic;
using Aow.Test.Serialization.Resources;
using Aow2.Serialization.Internal.Builders.OffsetMap;
using Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders;
using Aow2.Test.Serialization.Mocks;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class ListClassContextTest: OffsetMapContextTest<ListClassMock>
	{
		internal override IEnumerable<IFieldProvider> Providers
		{
			get
			{
				yield return new ClassFieldProvider( typeof( ListClassMock ) );
				yield return new ListFieldProvider( typeof( ListClassMock ) );
			}
		}

		protected override ListClassMock TestValue
		{
			get
			{
				ListClassMock value = new ListClassMock();
				value.Field0A = 0xBB;
				value.Add( 0x42 );
				value.Add( 0x53 );
				value.Add( 0x64 );

				return value;
			}
		}

		protected override byte[] TestBytes => Files.ListClass;
	}
}
