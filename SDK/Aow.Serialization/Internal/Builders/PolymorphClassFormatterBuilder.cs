using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Aow2.Serialization.Internal.Builders.Base;
using Aow2.Serialization.Internal.Builders.Polymorph;
using Aow2.Serialization.Logging;
using Utils.Runtime;

namespace Aow2.Serialization.Internal.Builders
{
	class PolymorphClassFormatterBuilder: IFormatterBuilder
	{
		public bool CanCreate( FormatterRequest request ) => request.IsPolymorph;

		public IFormatter Create( Type type )
		{
			Context context = new Context( type );
			return context.BuildFormatter();
		}

		private static readonly MethodInfo _objectGetType = Reflection.Method( ( object obj ) => obj.GetType() );
		private static readonly PropertyInfo _inheritanceCacheInstance = Reflection.Property( () => InheritanceCache.Instance );
		private static readonly MethodInfo _inheritanceCacheGet = Reflection.Method( ( InheritanceCache cache, Type type ) => cache.GetOrCreateNode( type ) );
		private static readonly PropertyInfo _intNullValue = Reflection.Property( ( int? x ) => x.Value );
		private static readonly PropertyInfo _intNullHasValue = Reflection.Property( ( int? x ) => x.HasValue );
		private static readonly PropertyInfo _inheritanceNodeClassID = Reflection.Property( ( InheritanceNode n ) => n.ClassID );
		private static readonly ConstructorInfo _invalidOperation = Reflection.Constructor( ( string m ) => new InvalidOperationException( m ) );
		private static readonly ConstructorInfo _newBinaryWriter = Reflection.Constructor( ( Stream s ) => new BinaryWriter( s ) );
		private static readonly MethodInfo _binaryWriterWriteInt = Reflection.Method( ( BinaryWriter w, int i ) => w.Write( i ) );
		private static readonly MethodInfo _getFormatter = Reflection.Method( ( Type t, bool b ) => FormatterManager.GetFormatterAsync( t, b ) );
		private static readonly PropertyInfo _taskResult = Reflection.Property( ( Task<IFormatter> t ) => t.Result );
		private static readonly MethodInfo _serialize = Reflection.Method( ( IFormatter f, Stream s, object o ) => f.Serialize( s, o ) );

		private static readonly ConstructorInfo _newBinaryReader = Reflection.Constructor( ( Stream s ) => new BinaryReader( s ) );
		private static readonly MethodInfo _binaryReaderReadInt32 = Reflection.Method( ( BinaryReader r ) => r.ReadInt32() );
		private static readonly MethodInfo _nodeFindSubclass = Reflection.Method( ( InheritanceNode n, int i ) => n.FindSubclass( i ) );
		private static readonly PropertyInfo _nodeType = Reflection.Property( ( InheritanceNode n ) => n.Type );
		private static readonly MethodInfo _deserialize = Reflection.Method( ( IFormatter f, Stream s, long l1, long l2, ISerializationLogger lgr ) => f.Deserialize( s, l1, l2, lgr ) );
		private static readonly PropertyInfo _streamPosition = Reflection.Property( ( Stream s ) => s.Position );
		private static readonly MethodInfo _logClassId = Reflection.Method( ( ISerializationLogger l, int id ) => l.LogClassId( id ) );

		private class Context: BuilderContext
		{
			private FieldInfo _targetTypeField;

			public Context( Type targetType )
				: base( targetType )
			{
			}

			protected override Expression CreateSerializeExpression()
			{
				ParameterExpression type = Expression.Parameter( typeof( Type ), "objectType" );
				ParameterExpression classID = Expression.Parameter( typeof( int ), "classID" );
				ParameterExpression node = Expression.Parameter( typeof( InheritanceNode ), "node" );
				ParameterExpression writer = Expression.Parameter( typeof( BinaryWriter ), "writer" );
				ParameterExpression formatter = Expression.Parameter( typeof( IFormatter ), "formatter" );

				BlockExpression block = Expression.Block(
					new ParameterExpression[] { type, node, classID, writer, formatter },

					Expression.Assign(
						type,
						Expression.Call( SerializeParams.Value, _objectGetType ) ),

					Expression.Assign(
						node,
						Expression.Call(
							Expression.Property( null, _inheritanceCacheInstance ),
							_inheritanceCacheGet,
							type ) ),

					Expression.IfThen(
						Expression.Not(
							Expression.Property(
								Expression.Property( node, _inheritanceNodeClassID ),
								_intNullHasValue ) ),

						Expression.Throw( Expression.New( typeof( InvalidOperationException ) ) ) ),

					Expression.Assign(
						classID,
						Expression.Property( Expression.Property( node, _inheritanceNodeClassID ), _intNullValue ) ),

					Expression.Call(
						Expression.New( _newBinaryWriter, SerializeParams.Stream ),
						_binaryWriterWriteInt,
						classID ),

					Expression.Assign(
						formatter,
						Expression.Property(
							Expression.Call( null, _getFormatter, type, Expression.Constant( false ) ),
							_taskResult ) ),

					Expression.Call(
						formatter,
						_serialize,
						SerializeParams.Stream,
						SerializeParams.Value )
					);

				return block;
			}

			protected override Expression CreateDeserializeExpression()
			{
				CreateTargetTypeField();

				ParameterExpression classID = Expression.Parameter( typeof( int ), "classID" );
				ParameterExpression targetNode = Expression.Parameter( typeof( InheritanceNode ), "targetNode" );
				ParameterExpression realClassNode = Expression.Parameter( typeof( InheritanceNode ), "realClassNode" );
				ParameterExpression formatter = Expression.Parameter( typeof( IFormatter ), "formatter" );
				ParameterExpression result = Expression.Parameter( typeof( object ), "result" );

				Expression block = Expression.Block(
					new ParameterExpression[] { classID, targetNode, realClassNode, formatter, result },

					Expression.Assign(
						Expression.Property( DeserializeParams.Stream, _streamPosition ),
						DeserializeParams.Offset ),

					Expression.Assign(
						classID,
						Expression.Call( Expression.New( _newBinaryReader, DeserializeParams.Stream ), _binaryReaderReadInt32 ) ),

					Expression.Call( DeserializeParams.Logger, _logClassId, classID ),

					Expression.Assign(
						targetNode,
						Expression.Call(
							Expression.Property( null, _inheritanceCacheInstance ),
							_inheritanceCacheGet,
							Expression.Field( null, _targetTypeField ) ) ),

					Expression.Assign(
						realClassNode,
						Expression.Call(
							targetNode,
							_nodeFindSubclass,
							classID ) ),

					Expression.Assign(
						formatter,
						Expression.Property(
							Expression.Call(
								null,
								_getFormatter,
								Expression.Property( realClassNode, _nodeType ),
								Expression.Constant( false ) ),
							_taskResult ) ),

					Expression.Assign(
						result,
						Expression.Call(
							formatter,
							_deserialize,
							DeserializeParams.Stream,
							Expression.Property( DeserializeParams.Stream, _streamPosition ),
							Expression.Subtract( DeserializeParams.Length, Expression.Constant( (long) sizeof( int ) ) ),
							DeserializeParams.Logger ) ),

					Expression.TypeAs( result, TargetType )
					);

				return block;
			}

			private void CreateTargetTypeField()
			{
				TypeBuilder helperBuilder = GeneratedAssembly.CreateType( TargetType + "FormatterHelper" );
				FieldBuilder field = helperBuilder.DefineField( "_targetType", typeof( Type ), FieldAttributes.Static | FieldAttributes.Public );

				Type helperClass = helperBuilder.CreateType();
				_targetTypeField = helperClass.GetField( field.Name );
				_targetTypeField.SetValue( null, TargetType );
			}

			protected override Expression CreateShouldSkipExpression() => Expression.Equal( base.ShouldSkipValueParam, Expression.Constant( null ) );
		}
	}
}
