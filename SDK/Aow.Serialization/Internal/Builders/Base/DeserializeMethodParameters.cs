using System;
using System.IO;
using System.Linq.Expressions;
using Aow2.Serialization.Logging;

namespace Aow2.Serialization.Internal.Builders.Base
{
	class DeserializeMethodParameters
	{
		public DeserializeMethodParameters()
		{
			Stream = Expression.Parameter( typeof( Stream ), "stream" );
			Length = Expression.Parameter( typeof( long ), "length" );
			Offset = Expression.Parameter( typeof( long ), "offset" );
			Logger = Expression.Parameter( typeof( ISerializationLogger ), "logger" );
		}

		public ParameterExpression Stream { get; private set; }
		public ParameterExpression Length { get; private set; }
		public ParameterExpression Offset { get; private set; }
		public ParameterExpression Logger { get; }

		public Type[] Types => new Type[] { Stream.Type, Length.Type, Offset.Type, Logger.Type };
	}
}
