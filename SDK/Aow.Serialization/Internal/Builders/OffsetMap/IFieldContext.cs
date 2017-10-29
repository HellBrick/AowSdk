using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal interface IFieldContext: IOffsetMapBuilderContext
	{
		ParameterExpression Key { get; }
	}
}
