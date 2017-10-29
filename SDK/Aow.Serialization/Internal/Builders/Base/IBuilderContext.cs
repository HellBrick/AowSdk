using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.Base
{
	interface IBuilderContext
	{
		DeserializeMethodParameters DeserializeParams { get; set; }
		SerializeMethodParameters SerializeParams { get; set; }
		ParameterExpression ShouldSkipValueParam { get; set; }
	}
}
