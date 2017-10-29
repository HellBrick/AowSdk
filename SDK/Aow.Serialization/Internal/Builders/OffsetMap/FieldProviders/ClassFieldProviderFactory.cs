using System;
using System.Reflection;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	class ClassFieldProviderFactory: IFieldProviderFactory
	{
		public bool IsSupported( Type type )
		{
			AowClassAttribute classAttr = type.GetCustomAttribute<AowClassAttribute>( inherit : false );
			return classAttr != null;
		}

		public IFieldProvider Create( Type type ) => new ClassFieldProvider( type );
	}
}
