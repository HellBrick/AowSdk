using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Aow2.Serialization.Internal.Builders.Base;

namespace Aow2.Serialization.Internal.Builders
{
	class EnumFormatterBuilder: IFormatterBuilder
	{
		public bool CanCreate( FormatterRequest request ) => request.Type.IsEnum;

		public IFormatter Create( Type type )
		{
			Type underlyingType = type.GetEnumUnderlyingType();
			IFormatter underlyingFormatter = FormatterManager.GetFormatterAsync( underlyingType, false ).Result;

			Context context = new Context( type, underlyingFormatter, underlyingType );
			return context.BuildFormatter();
		}

		private class Context: BuilderContext
		{
			public Context( Type targetType, IFormatter underlyingFormatter, Type underlyingType )
				: base( targetType )
			{
				UnderlyingFormatter = underlyingFormatter;
				UnderlyingType = underlyingType;
			}

			public Type UnderlyingType { get; set; }
			public IFormatter UnderlyingFormatter { get; set; }
			public FieldInfo UnderlyingFormatterField { get; set; }

			protected override void Initialize()
			{
				base.Initialize();
				CreateHelperClass();
			}

			protected override Expression CreateSerializeExpression() => Expression.Block(
					Expression.Call(
						Expression.Field( null, UnderlyingFormatterField ),
						UnderlyingFormatterField.FieldType.GetMethod( "Serialize" ),
						SerializeParams.Stream,
						Expression.Convert( SerializeParams.Value, UnderlyingType )
						)
					);

			protected override Expression CreateDeserializeExpression() => Expression.Block(
					Expression.Convert(
						Expression.Call(
							Expression.Field( null, UnderlyingFormatterField ),
							UnderlyingFormatterField.FieldType.GetMethod( "Deserialize" ),
							DeserializeParams.Stream,
							DeserializeParams.Offset,
							DeserializeParams.Length
							),
						TargetType ) );

			protected override Expression CreateShouldSkipExpression() => Expression.Constant( false );

			private const string _helperFormatterField = "UnderlyingFormatter";

			private void CreateHelperClass()
			{
				TypeBuilder helperBuilder = GeneratedAssembly.CreateType( TargetType.Name + "FormatterHelper" );
				FieldBuilder fieldBuilder = helperBuilder.DefineField( _helperFormatterField, UnderlyingFormatter.GetType(), FieldAttributes.Public | FieldAttributes.Static );

				Type helperType = helperBuilder.CreateType();

				UnderlyingFormatterField = helperType.GetField( _helperFormatterField );
				UnderlyingFormatterField.SetValue( null, UnderlyingFormatter );
			}
		}
	}
}
