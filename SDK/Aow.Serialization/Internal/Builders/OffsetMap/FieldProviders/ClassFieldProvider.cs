using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Aow2.Serialization.Internal.Builders.Base;
using Utils.Runtime;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	internal class ClassFieldProvider: IFieldProvider
	{
		private Type _type;
		private Dictionary<FieldBase, FieldInfo> _subFormatters;
		private List<FieldBase> _fields;
		private FieldInfo _keyListField;

		public ClassFieldProvider( Type type )
		{
			_type = type;

			FindFields();
			CreateSubFormatterFields();
		}

		private static IFieldProviderFactory _factory = new ClassFieldProviderFactory();
		public static IFieldProviderFactory Factory => _factory;

		internal static Func<IReadOffsetRecord, int> _readOffsetRecordKeySelector = rec => rec.ID;

		private static PropertyInfo _readRecordOffset = Reflection.Property( ( IReadOffsetRecord r ) => r.AbsoluteOffset );
		private static PropertyInfo _readRecordLength = Reflection.Property( ( IReadOffsetRecord r ) => r.Length );
		private static PropertyInfo _readRecordID = Reflection.Property( ( IReadOffsetRecord r ) => r.ID );

		private static MethodInfo _listAddMethods = Reflection.Method( ( List<FieldLengthInfo> list, FieldLengthInfo obj ) => list.Add( obj ) );
		private static ConstructorInfo _fieldLengthInfoCtor = Reflection.Constructor( ( int id, long length ) => new FieldLengthInfo( id, length ) );
		private static PropertyInfo _streamPositionProperty = Reflection.Property( ( Stream s ) => s.Position );
		private static PropertyInfo _taskResult = Reflection.Property( ( Task<IFormatter> t ) => t.Result );

		private readonly static ConstructorInfo _fieldAnalysisExceptionCtor = Reflection.Constructor( ( Type t, int id ) => new FieldAnalysisException( t, id ) );

		public int StartingIndex => 0;

		public Expression KeyEnumerableExpression( IOffsetMapBuilderContext context ) => Expression.Field( null, _keyListField );

		public Expression ShouldSkipFieldExpression( IFieldContext context )
		{
			ParameterExpression shouldSkip = Expression.Parameter( typeof( bool ), "shoudSkip" );

			return Expression.Block(
				new ParameterExpression[] { shouldSkip },

				Expression.Assign( shouldSkip, Expression.Constant( false ) ),

				Expression.Switch(
					context.Key,
					FieldCases(
						( field, formatter, formatterType ) =>
						Expression.Assign(
							shouldSkip,

							field.IsImportOnly ?
								Expression.Constant( true ) as Expression :
								Expression.Call(
									formatter,
									formatterType.GetMethod( "ShouldSkipField" ),
									field.CreateExpression( context.SerializeParams.Value ) ) )
						) ),

				shouldSkip
				);
		}

		public Expression SerializeFieldExpression( IFieldContext context ) => Expression.Switch(
				context.Key,
				FieldCases(
					( field, formatter, formatterType ) =>
					Expression.Call(
						formatter,
						formatterType.GetMethod( "Serialize" ),
						context.FillFieldListParams.DataStream,
						field.CreateExpression( context.SerializeParams.Value ) ) ) );

		public Expression DeserializeAndSaveFieldExpression( IFieldContext context ) => Expression.Switch(
				context.Key,

				defaultBody: Expression.Throw(
					Expression.New(
						_fieldAnalysisExceptionCtor,
						Expression.Constant( _type, typeof( Type ) ),
						Expression.Property( context.ParseFieldParams.OffsetRecord, _readRecordID )
					)
				),

				FieldCases(
					( field, formatter, formatterType ) =>
					Expression.Assign(
						field.CreateExpression( context.ParseFieldParams.Result ),
						Expression.Convert(
							Expression.Call(
								formatter,
								formatterType.GetMethod( "Deserialize" ),
								context.DeserializeParams.Stream,
								Expression.Property( context.ParseFieldParams.OffsetRecord, _readRecordOffset ),
								Expression.Property( context.ParseFieldParams.OffsetRecord, _readRecordLength ) ),
							field.Type ) )
				) );

		private const BindingFlags _fieldSearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | ~BindingFlags.DeclaredOnly;

		private void FindFields()
		{
			IEnumerable<FieldBase> fields = _type
				.GetFields( _fieldSearchFlags )
				.Select( f => new
				{
					Field = f,
					Attributes = f.GetCustomAttributes<FieldAttribute>()
				} )
				.SelectMany( rec => rec.Attributes.Select( attr => new Field( rec.Field, attr ) ) )
				.OfType<FieldBase>();

			IEnumerable<FieldBase> properties = _type
				.GetProperties( _fieldSearchFlags )
				.Select( p => new
				{
					Property = p,
					Attributes = p.GetCustomAttributes<FieldAttribute>()
				} )
				.SelectMany( rec => rec.Attributes.Select( attr => new Property( rec.Property, attr ) ) )
				.OfType<FieldBase>();

			_fields = fields
				.Concat( properties )
				.OrderBy( f => f.ID )
				.ToList();
		}

		private void CreateSubFormatterFields()
		{
			_subFormatters = new Dictionary<FieldBase, FieldInfo>();

			TypeBuilder helperBuilder = GeneratedAssembly.CreateType( _type.Name + "FormatterHelper" );

			Dictionary<string, Task<IFormatter>> values = new Dictionary<string, Task<IFormatter>>();
			Dictionary<string, FieldBase> fieldMap = new Dictionary<string, FieldBase>();

			foreach ( FieldBase field in _fields )
			{
				Task<IFormatter> formatterTask = FormatterManager.GetFormatterAsync( field.Type, field.Type.IsAbstract );

				Type fieldType = typeof( Task<IFormatter> );
				string fieldName = String.Format( "_0x{0}_formatter", field.ID.ToString( "x2" ) );
				FieldBuilder cachedFormatterField = helperBuilder.DefineField( fieldName, fieldType, FieldAttributes.Static | FieldAttributes.Public );

				values.Add( cachedFormatterField.Name, formatterTask );
				fieldMap.Add( cachedFormatterField.Name, field );
			}

			FieldBuilder keyListFieldBuilder = helperBuilder.DefineField( "_fieldIDs", typeof( List<int> ), FieldAttributes.Static | FieldAttributes.Public );

			Type helperType = helperBuilder.CreateType();

			foreach ( KeyValuePair<string, Task<IFormatter>> pair in values )
			{
				FieldInfo realFieldInfo = helperType.GetField( pair.Key );
				realFieldInfo.SetValue( null, pair.Value );
				_subFormatters.Add( fieldMap[pair.Key], realFieldInfo );
			}

			_keyListField = helperType.GetField( keyListFieldBuilder.Name );
			List<int> keys = _fields.Select( f => f.ID ).ToList();
			_keyListField.SetValue( null, keys );
		}

		private SwitchCase[] FieldCases( Func<FieldBase, Expression, Type, Expression> caseBody )
		{
			List<SwitchCase> caseExpressions = new List<SwitchCase>();

			foreach ( FieldBase field in _fields )
			{
				Type formatterType = typeof( Formatter<> ).MakeGenericType( field.Type );
				ParameterExpression binaryFormatter = Expression.Parameter( formatterType, "formatter" );

				Expression formatterExpression =
					Expression.TypeAs(
						Expression.Property(
							Expression.Field( null, _subFormatters[field] ),
							_taskResult ),
						formatterType );

				SwitchCase caseExpression = Expression.SwitchCase(
					Expression.Block(
						typeof( void ),
						new[] { binaryFormatter },

						Expression.Assign(
							binaryFormatter,
							formatterExpression ),

						caseBody( field, binaryFormatter, formatterType )
						),
					Expression.Constant( field.ID ) );

				caseExpressions.Add( caseExpression );
			}
			return caseExpressions.ToArray();
		}
	}
}
