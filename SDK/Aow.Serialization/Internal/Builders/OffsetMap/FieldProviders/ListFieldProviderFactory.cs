using System;
using System.Linq;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	internal class ListFieldProviderFactory: IFieldProviderFactory
	{
		public bool IsSupported( Type type ) => type.GetInterfaces().Any( i => ListFieldProvider.ImplementsIList( i ) ) &&
				!ListFieldProvider.IsListSuppressed( type );

		public IFieldProvider Create( Type type ) => new ListFieldProvider( type );
	}
}
