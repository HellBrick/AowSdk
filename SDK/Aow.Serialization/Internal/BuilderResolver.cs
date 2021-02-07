using System.Collections.Generic;
using System.Linq;
using Aow2.Serialization.Internal.Builders;

namespace Aow2.Serialization.Internal
{
	internal static class BuilderResolver
	{
		private static List<IFormatterBuilder> _builders = new List<IFormatterBuilder>()
		{
			new CustomFormatterBuilder(),
			new PrimitiveTypeFormatterBuilder(),
			new BoolFormatterBuilder(),
			new NullableFormatterBuilder(),
			new OffsetMapFormatterBuilder(),
			new PolymorphClassFormatterBuilder(),
			new EnumFormatterBuilder()
		};

		public static IFormatterBuilder GetBuilder( FormatterRequest request )
		{
			IFormatterBuilder builder = _builders.FirstOrDefault( b => b.CanCreate( request ) );
			if ( builder == null )
				throw new SerializationTypeBinaryAnalysisException( request.Type );

			return builder;
		}
	}
}
