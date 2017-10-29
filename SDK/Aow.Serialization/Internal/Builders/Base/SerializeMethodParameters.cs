using System;
using System.IO;
using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.Base
{
	class SerializeMethodParameters
	{
		public SerializeMethodParameters() => Stream = Expression.Parameter( typeof( Stream ), "stream" );

		public ParameterExpression Stream { get; set; }
		public ParameterExpression Value { get; set; }

		public Type[] Types => new Type[] { Stream.Type, Value.Type };
	}
}
