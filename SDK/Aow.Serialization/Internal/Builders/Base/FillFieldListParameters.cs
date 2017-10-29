using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.Base
{
	class FillFieldListParameters
	{
		public FillFieldListParameters()
		{
			FieldLengths = Expression.Parameter( typeof( List<FieldLengthInfo> ), "fieldLengths" );
			DataStream = Expression.Parameter( typeof( MemoryStream ), "dataStream" );
		}

		public ParameterExpression FieldLengths { get; private set; }
		public ParameterExpression DataStream { get; private set; }
	}
}
