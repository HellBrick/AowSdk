using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal interface IItemFieldContext: IFieldContext
	{
		ParameterExpression Item { get; }
	}
}
