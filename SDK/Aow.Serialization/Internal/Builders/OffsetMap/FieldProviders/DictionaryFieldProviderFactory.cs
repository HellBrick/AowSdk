using System;
using System.Linq;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	internal class DictionaryFieldProviderFactory: IFieldProviderFactory
	{
		public bool IsSupported( Type type )
			=> type.GetInterfaces().Any( t => DictionaryFieldProvider.ImplementsIDictionary( t ) )
			&& !ListFieldProvider.IsListSuppressed( type );

		public IFieldProvider Create( Type type ) => new DictionaryFieldProvider( type );
	}
}
