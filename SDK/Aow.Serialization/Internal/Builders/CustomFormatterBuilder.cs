using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Aow2.Serialization.Logging;
using Utils.Runtime;

namespace Aow2.Serialization.Internal.Builders
{
	class CustomFormatterBuilder: IFormatterBuilder
	{
		private static MethodInfo _serializeMethod = Reflection.Method( ( ICustomFormatted obj, Stream stream ) => obj.Serialize( stream ) );
		private static MethodInfo _deserializeMethod = Reflection.Method( ( ICustomFormatted obj, Stream stream, long length ) => obj.Deserialize( stream, length ) );
		private static MethodInfo _shouldSkipMethod = Reflection.Method( ( ICustomFormatted obj ) => obj.ShouldBeOmitted() );
		private static PropertyInfo _streamPositionProperty = Reflection.Property( ( Stream obj ) => obj.Position );
		private static MethodInfo _logBlobMethod = Reflection.Method( ( ISerializationLogger logger, Stream stream, long offset, long length ) => logger.LogBlob( stream, offset, length ) );

		public bool CanCreate( FormatterRequest request ) => !request.IsPolymorph &&
				typeof( ICustomFormatted ).IsAssignableFrom( request.Type );

		public IFormatter Create( Type type )
		{
			Type formatterType = typeof( EditableFormatter<> ).MakeGenericType( type );
			IEditableFormatter formatter = Activator.CreateInstance( formatterType ) as IEditableFormatter;

			formatter.SerializationDelegate = CreateWriteMethod( type );
			formatter.DeserializationDelegate = CreateReadMethod( type );
			formatter.ShouldSkipFieldDelegate = CreateShouldSkipMethod( type );

			return formatter as IFormatter;
		}

		private static Delegate CreateWriteMethod( Type type )
		{
			ParameterExpression stream = Expression.Parameter( typeof( Stream ), "stream" );
			ParameterExpression value = Expression.Parameter( type, "value" );

			Expression writeBody = Expression.Call( value, _serializeMethod, stream );
			LambdaExpression writeLambda = Expression.Lambda( writeBody, new ParameterExpression[] { stream, value } );

			return writeLambda.Compile();
		}

		private Delegate CreateReadMethod( Type type )
		{
			ParameterExpression stream = Expression.Parameter( typeof( Stream ), "stream" );
			ParameterExpression offset = Expression.Parameter( typeof( long ), "offset" );
			ParameterExpression length = Expression.Parameter( typeof( long ), "length" );
			ParameterExpression logger = Expression.Parameter( typeof( ISerializationLogger ), "logger" );

			ParameterExpression result = Expression.Parameter( type, "result" );

			Expression readBody =
				Expression.Block(
					new ParameterExpression[] { result },

					Expression.Call( logger, _logBlobMethod, stream, offset, length ),

					Expression.Assign(
						Expression.Property( stream, _streamPositionProperty ),
						offset ),

					Expression.Assign( result, Expression.New( type ) ),

					Expression.Call( result, _deserializeMethod, stream, length ),

					result
				);

			LambdaExpression readLambda = Expression.Lambda( readBody, new ParameterExpression[] { stream, offset, length, logger } );

			return readLambda.Compile();
		}

		private Delegate CreateShouldSkipMethod( Type type )
		{
			ParameterExpression value = Expression.Parameter( type, "value" );

			Expression shouldSkipExpression;

			if ( type.IsValueType )
			{
				shouldSkipExpression = Expression.Call( value, _shouldSkipMethod );
			}
			else
			{
				shouldSkipExpression =
					Expression.Condition(
						Expression.NotEqual( value, Expression.Constant( null ) ),
						Expression.Call( value, _shouldSkipMethod ),
						Expression.Constant( true ) );
			}

			LambdaExpression writeLambda = Expression.Lambda( shouldSkipExpression, new ParameterExpression[] { value } );

			return writeLambda.Compile();
		}
	}
}
