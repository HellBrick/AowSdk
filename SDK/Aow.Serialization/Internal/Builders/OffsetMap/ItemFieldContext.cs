using System.Linq.Expressions;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	class ItemFieldContext: IItemFieldContext
	{
		private IFieldContext _baseContext;

		public ItemFieldContext( IOffsetMapBuilderContext builderContext, ParameterExpression key, ParameterExpression item )
			: this( new FieldContext( builderContext, key ), item )
		{
		}

		public ItemFieldContext( IFieldContext fieldContext, ParameterExpression item )
		{
			_baseContext = fieldContext;
			Item = item;
		}

		public ParameterExpression Item { get; private set; }

		public ParameterExpression Key => _baseContext.Key;

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

		public ParameterExpression ShouldSkipValueParam
		{
			get => _baseContext.ShouldSkipValueParam;
			set => _baseContext.ShouldSkipValueParam = value;
		}
	}
}
