using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;
using Aow2.Serialization.Internal.Builders.Base;
using System.Reflection.Emit;
using Utils.Runtime;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	abstract class EnumerationFieldProvider: IFieldProvider
	{
		private FieldInfo _itemFormatterField;
		private Type _itemFormatterType;

		public EnumerationFieldProvider( Type type )
		{
			Type = type;

			ItemType = GetItemType();
			FindStartingIndex();
			CreateItemFormatter();
		}

		private void FindStartingIndex()
		{
			ListStartingIndexAttribute startingIndexAttr = Type.GetCustomAttribute<ListStartingIndexAttribute>();
			_startingIndex = startingIndexAttr != null ? startingIndexAttr.StartingID : 0;
		}

		private void CreateItemFormatter()
		{
			Task<IFormatter> itemFormatter = FormatterManager.GetFormatterAsync( ItemType, isPolymorph : true );
			Type formatterType = typeof( Task<IFormatter> );
			string fieldName = "ItemFormatter";

			TypeBuilder helperBuilder = GeneratedAssembly.CreateType( Type.Name + "FormatterHelper" );
			FieldBuilder cachedFormatterField = helperBuilder.DefineField( fieldName, formatterType, FieldAttributes.Static | FieldAttributes.Public );

			Type helperType = helperBuilder.CreateType();

			FieldInfo realFieldInfo = helperType.GetField( fieldName );
			realFieldInfo.SetValue( null, itemFormatter );

			_itemFormatterField = realFieldInfo;
			_itemFormatterType = typeof( Formatter<> ).MakeGenericType( ItemType );
		}

		protected Type Type { get; private set; }
		protected Type ItemType { get; private set; }

		private static PropertyInfo _readRecordOffset = Reflection.Property( ( IReadOffsetRecord r ) => r.AbsoluteOffset );
		private static PropertyInfo _readRecordLength = Reflection.Property( ( IReadOffsetRecord r ) => r.Length );
		private static PropertyInfo _taskResult = Reflection.Property( ( Task<IFormatter> t ) => t.Result );

		private int _startingIndex;
		public int StartingIndex => _startingIndex;

		public abstract Expression KeyEnumerableExpression( IOffsetMapBuilderContext context );

		public Expression ShouldSkipFieldExpression( IFieldContext context ) => Expression.Call(
				FormatterExpression(),
				_itemFormatterType.GetMethod( "ShouldSkipField" ),
				GetItemExpression( context ) );

		private Expression FormatterExpression() => Expression.TypeAs(
				Expression.Property(
					Expression.Field( null, _itemFormatterField ),
					_taskResult ),
				_itemFormatterType );

		public Expression SerializeFieldExpression( IFieldContext context ) => Expression.Call(
				FormatterExpression(),
				_itemFormatterType.GetMethod( "Serialize" ),
				context.FillFieldListParams.DataStream,
				GetItemExpression( context ) );

		public Expression DeserializeAndSaveFieldExpression( IFieldContext context )
		{
			ParameterExpression item = Expression.Parameter( ItemType, "item" );

			return Expression.Block(
				new ParameterExpression[] { item },

				Expression.Assign(
					item,
					Expression.Call(
						FormatterExpression(),
						_itemFormatterType.GetMethod( "Deserialize" ),
						context.DeserializeParams.Stream,
						Expression.Property( context.ParseFieldParams.OffsetRecord, _readRecordOffset ),
						Expression.Property( context.ParseFieldParams.OffsetRecord, _readRecordLength ) ) ),

				AddItemExpression( new ItemFieldContext( context, item ) ) );
		}

		protected abstract Type GetItemType();
		protected abstract Expression GetItemExpression( IFieldContext context );

		protected abstract Expression AddItemExpression( IItemFieldContext context );
	}
}
