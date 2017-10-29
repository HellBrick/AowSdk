using System;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal interface IFieldProviderFactory
	{
		bool IsSupported( Type type );
		IFieldProvider Create( Type type );
	}
}
