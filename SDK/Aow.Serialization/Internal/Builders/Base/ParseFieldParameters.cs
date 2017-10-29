using System;
using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.Base
{
	class ParseFieldParameters
	{
		public ParseFieldParameters( Type targetType )
		{
			Result = Expression.Parameter( targetType, "result" );
			OffsetRecord = Expression.Parameter( typeof( IReadOffsetRecord ), "offsetRecord" );
		}

		public ParameterExpression Result { get; set; }
		public ParameterExpression OffsetRecord { get; set; }
	}
}
