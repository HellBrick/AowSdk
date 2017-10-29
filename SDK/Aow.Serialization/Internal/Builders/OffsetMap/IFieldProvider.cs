using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal interface IFieldProvider
	{
		int StartingIndex { get; }

		Expression KeyEnumerableExpression( IOffsetMapBuilderContext context );
		Expression ShouldSkipFieldExpression( IFieldContext context );
		Expression SerializeFieldExpression( IFieldContext context );

		Expression DeserializeAndSaveFieldExpression( IFieldContext context );
	}
}
