using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal class FieldContext: IFieldContext
	{
		private IOffsetMapBuilderContext _baseContext;
		private ParameterExpression _key;

		public FieldContext( IOffsetMapBuilderContext baseContext, ParameterExpression key )
		{
			_baseContext = baseContext;
			_key = key;
		}

		public System.Linq.Expressions.ParameterExpression Key => _key;

		public Base.FillFieldListParameters FillFieldListParams => _baseContext.FillFieldListParams;

		public Base.ParseFieldParameters ParseFieldParams => _baseContext.ParseFieldParams;

		public Base.DeserializeMethodParameters DeserializeParams
		{
			get => _baseContext.DeserializeParams;
			set => _baseContext.DeserializeParams = value;
		}

		public Base.SerializeMethodParameters SerializeParams
		{
			get => _baseContext.SerializeParams;
			set => _baseContext.SerializeParams = value;
		}

		public System.Linq.Expressions.ParameterExpression ShouldSkipValueParam
		{
			get => _baseContext.ShouldSkipValueParam;
			set => _baseContext.ShouldSkipValueParam = value;
		}
	}
}
