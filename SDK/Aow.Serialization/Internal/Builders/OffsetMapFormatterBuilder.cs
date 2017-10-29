using System;
using Aow2.Serialization.Internal.Builders.OffsetMap;

namespace Aow2.Serialization.Internal.Builders
{
	internal class OffsetMapFormatterBuilder: IFormatterBuilder
	{
		public bool CanCreate( FormatterRequest request ) => !request.IsPolymorph &&
				FieldProviderResolver.Instance.IsSupported( request.Type );

		public IFormatter Create( Type type )
		{
			OffsetMapBuilderContext context = new OffsetMapBuilderContext( type, FieldProviderResolver.Instance.GetProviders( type ) );
			return context.BuildFormatter();
		}
	}
}
