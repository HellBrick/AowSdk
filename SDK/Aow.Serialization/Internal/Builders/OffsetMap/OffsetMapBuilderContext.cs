using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Aow2.Serialization.Internal.Builders.Base;
using Aow2.Serialization.Logging;
using Utils.Runtime;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal class OffsetMapBuilderContext : BuilderContext, IOffsetMapBuilderContext
	{
		private List<IFieldProvider> _providers;

		private static MethodInfo _omFormatterWriteMethod = Reflection.Method( ( Stream s1, Stream s2, List<FieldLengthInfo> l ) => OffsetMapFormatter.WriteNodesToStream( s1, s2, l ) );
		private static PropertyInfo _streamPositionProperty = Reflection.Property( ( Stream s ) => s.Position );
		private static MethodInfo _readOffsetsMethod = Reflection.Method( ( Stream s, long o, long l ) => OffsetMapFormatter.ReadNodesFromStream( s, o, l ) );
		private static PropertyInfo _listCountProperty = Reflection.Property( ( List<IReadOffsetRecord> l ) => l.Count );
		private static MethodInfo _listItemGetter = Reflection.Method( ( List<IReadOffsetRecord> l, int i ) => l[ i ] );
		private static MethodInfo _listAddMethod = Reflection.Method( ( List<FieldLengthInfo> list, FieldLengthInfo obj ) => list.Add( obj ) );
		private static ConstructorInfo _fieldLengthInfoCtor = Reflection.Constructor( ( int id, long length ) => new FieldLengthInfo( id, length ) );

		private static MethodInfo _getIntEnumerator = Reflection.Method( ( IEnumerable<int> e ) => e.GetEnumerator() );
		private static MethodInfo _intMoveNext = Reflection.Method( ( IEnumerator<int> e ) => e.MoveNext() );
		private static PropertyInfo _intCurrent = Reflection.Property( ( IEnumerator<int> e ) => e.Current );
		private static MethodInfo _dispose = Reflection.Method( ( IDisposable d ) => d.Dispose() );

		private static PropertyInfo _readRecordOffset = Reflection.Property( ( IReadOffsetRecord r ) => r.AbsoluteOffset );
		private static PropertyInfo _readRecordID = Reflection.Property( ( IReadOffsetRecord r ) => r.ID );

		private static MethodInfo _logFieldStart = Reflection.Method( ( ISerializationLogger l, int id ) => l.LogFieldStart( id ) );
		private static MethodInfo _logFieldEnd = Reflection.Method( ( ISerializationLogger l ) => l.LogFieldEnd() );

		public OffsetMapBuilderContext( Type targetType, IEnumerable<IFieldProvider> providers )
			: this( targetType, providers.ToArray() )
		{
		}

		public OffsetMapBuilderContext( Type targetType, params IFieldProvider[] providers )
			: base( targetType )
		{
			FillFieldListParams = new FillFieldListParameters();
			ParseFieldParams = new ParseFieldParameters( targetType );

			_providers = providers.OrderBy( p => p.StartingIndex ).ToList();
		}

		public FillFieldListParameters FillFieldListParams { get; private set; }
		public ParseFieldParameters ParseFieldParams { get; private set; }

		protected override Expression CreateSerializeExpression()
		{
			BlockExpression body = Expression.Block(
				new ParameterExpression[] { FillFieldListParams.FieldLengths, FillFieldListParams.DataStream },

				Expression.Assign( FillFieldListParams.FieldLengths, Expression.New( typeof( List<FieldLengthInfo> ) ) ),

				Expression.Assign(
					FillFieldListParams.DataStream,
					Expression.New( typeof( MemoryStream ) ) ),

				Expression.TryFinally(
					Expression.Block(
						FillSerializationFieldListExpression(),
						Expression.Call( null, _omFormatterWriteMethod, SerializeParams.Stream, FillFieldListParams.DataStream, FillFieldListParams.FieldLengths ) ),

					Expression.Block(
						Expression.IfThen(
							Expression.NotEqual( FillFieldListParams.DataStream, Expression.Constant( null, typeof( MemoryStream ) ) ),
							Expression.Call( FillFieldListParams.DataStream, _dispose ) ) )
				 ) );

			return body;
		}

		private Expression FillSerializationFieldListExpression()
		{
			IEnumerable<Expression> expressions = _providers.Select<IFieldProvider, Expression>( p => FillFieldListPart( p ) );
			return Expression.Block( expressions );
		}

		private Expression FillFieldListPart( IFieldProvider provider )
		{
			ParameterExpression keyEnumerator = Expression.Parameter( typeof( IEnumerator<int> ), "keyEnumerator" );
			ParameterExpression key = Expression.Parameter( typeof( int ), "key" );
			ParameterExpression lastFieldPosition = Expression.Parameter( typeof( long ), "lastFieldPosition" );

			LabelTarget labelTargetContinue = Expression.Label( "_continue" );
			LabelTarget labelTargetBreak = Expression.Label( "_break" );

			return Expression.Block(
				new ParameterExpression[] { key, keyEnumerator },

				Expression.Assign(
					keyEnumerator,
					Expression.Call(
						provider.KeyEnumerableExpression( this ),
						_getIntEnumerator ) ),

				Expression.TryFinally(
					Expression.Loop(
						Expression.IfThenElse(
							Expression.Call( keyEnumerator, _intMoveNext ),

							Expression.Block(
								new ParameterExpression[] { lastFieldPosition },

								Expression.Assign( key, Expression.Property( keyEnumerator, _intCurrent ) ),

								Expression.IfThen(
									Expression.Not( provider.ShouldSkipFieldExpression( new FieldContext( this, key ) ) ),

									Expression.Block(
										Expression.Assign(
											lastFieldPosition,
											Expression.Property( FillFieldListParams.DataStream, _streamPositionProperty ) ),

										provider.SerializeFieldExpression( new FieldContext( this, key ) ),

										Expression.Call(
											FillFieldListParams.FieldLengths,
											_listAddMethod,
											Expression.New(
												_fieldLengthInfoCtor,
												Expression.Add( Expression.Constant( provider.StartingIndex ), key ),
												Expression.Subtract(
													Expression.Property( FillFieldListParams.DataStream, _streamPositionProperty ),
													lastFieldPosition ) ) )
										)
									)
								),

							Expression.Break( labelTargetBreak ) ),

						labelTargetBreak,
						labelTargetContinue ),

					Expression.IfThen(
						Expression.NotEqual( keyEnumerator, Expression.Constant( null ) ),
						Expression.Call( keyEnumerator, _dispose ) ) )
				);
		}

		protected override Expression CreateDeserializeExpression()
		{
			ParameterExpression recordList = Expression.Parameter( typeof( List<IReadOffsetRecord> ), "recordList" );
			ParameterExpression recordDictionary = Expression.Parameter( typeof( Dictionary<int, IReadOffsetRecord> ), "recordDictionary" );
			ParameterExpression parsedObject = Expression.Parameter( typeof( object ), "parsedObject" );
			ParameterExpression hasIndex = Expression.Parameter( typeof( bool ), "hasIndex" );
			ParameterExpression streamPosition = Expression.Parameter( typeof( long ), "streamPosition" );
			ParameterExpression index = Expression.Parameter( typeof( int ), "i" );

			LabelTarget labelTargetContinue = Expression.Label( "_continue" );
			LabelTarget labelTargetBreak = Expression.Label( "_break" );

			BlockExpression body = Expression.Block(
				TargetType,
				new ParameterExpression[] { ParseFieldParams.Result, recordList, recordDictionary, parsedObject, hasIndex, ParseFieldParams.OffsetRecord, streamPosition, index },

				Expression.Assign(
					ParseFieldParams.Result,
					Expression.New( TargetType.GetConstructor( Type.EmptyTypes ) ) ),

				Expression.Assign(
					Expression.Property( DeserializeParams.Stream, _streamPositionProperty ),
					DeserializeParams.Offset ),

				Expression.Assign(
					streamPosition,
					Expression.Property( DeserializeParams.Stream, _streamPositionProperty ) ),

				Expression.Assign(
					recordList,
					Expression.Call( null, _readOffsetsMethod, DeserializeParams.Stream, streamPosition, DeserializeParams.Length ) ),

				Expression.Assign(
					index,
					Expression.Constant( 0 ) ),

				Expression.Loop(
					Expression.IfThenElse(
						Expression.LessThan(
							index,
							Expression.Property( recordList, _listCountProperty ) ),

						Expression.Block(
							Expression.Assign(
								ParseFieldParams.OffsetRecord,
								Expression.Call( recordList, _listItemGetter, new Expression[] { index } ) ),

							ParseFieldExpression(),

							Expression.Assign(
								index,
								Expression.Increment( index ) ),

							Expression.Continue( labelTargetContinue )
							),

						Expression.Break( labelTargetBreak ) ),

					labelTargetBreak,
					labelTargetContinue ),

				Expression.Label( Expression.Label( typeof( object ) ), ParseFieldParams.Result ),
				ParseFieldParams.Result
			);

			return body;
		}

		private Expression ParseFieldExpression()
		{
			ParameterExpression key = Expression.Parameter( typeof( int ), "index" );
			IFieldContext fieldContext = new FieldContext( this, key );

			LabelTarget labelBreak = Expression.Label( "_break" );

			ParseFieldProviderExpressions providerExpressions = null;

			if ( _providers.Count == 1 )
			{
				IFieldProvider provider = _providers[ 0 ];
				providerExpressions = new ParseFieldProviderExpressions( provider, fieldContext );

				return ParseFieldPart( fieldContext, providerExpressions );
			}
			else
			{
				List<ParseFieldProviderExpressions> providerExpressionsList = _providers
					.Select( p => new ParseFieldProviderExpressions( p, fieldContext ) )
					.ToList();

				IEnumerable<Expression> providerParseParts = ParseFieldProviderParts( fieldContext, providerExpressionsList );
				return Expression.Block( providerParseParts.ToArray() );
			}
		}

		private IEnumerable<Expression> ParseFieldProviderParts( IFieldContext fieldContext, List<ParseFieldProviderExpressions> providerExpressionsList )
		{
			for ( int i = 0; i < providerExpressionsList.Count; i++ )
			{
				bool isLast = i == providerExpressionsList.Count - 1;
				ParseFieldProviderExpressions providerExpressions = providerExpressionsList[ i ];

				Expression test;

				Expression fitsLowerBound = Expression.LessThanOrEqual(
					providerExpressions.StartingIndex,
					Expression.Property( fieldContext.ParseFieldParams.OffsetRecord, _readRecordID ) );

				if ( isLast )
				{
					test = fitsLowerBound;
				}
				else
				{
					Expression fitsUpperBound = Expression.LessThan(
						Expression.Property( fieldContext.ParseFieldParams.OffsetRecord, _readRecordID ),
						providerExpressionsList[ i + 1 ].StartingIndex );

					test = Expression.And( fitsLowerBound, fitsUpperBound );
				}

				yield return
					Expression.IfThen(
						test,
						ParseFieldPart( fieldContext, providerExpressions ) );
			}
		}

		private Expression ParseFieldPart( IFieldContext fieldContext, ParseFieldProviderExpressions providerExpressions ) => Expression.Block(
				new ParameterExpression[] { fieldContext.Key },

				Expression.Call(
					fieldContext.DeserializeParams.Logger,
					_logFieldStart,
					Expression.Property( ParseFieldParams.OffsetRecord, _readRecordID ) ),

				Expression.Assign(
					fieldContext.Key,
					Expression.Subtract(
						Expression.Property( ParseFieldParams.OffsetRecord, _readRecordID ),
						providerExpressions.StartingIndex ) ),

				Expression.Assign(
					Expression.Property( DeserializeParams.Stream, _streamPositionProperty ),
					Expression.Property( ParseFieldParams.OffsetRecord, _readRecordOffset ) ),

				providerExpressions.DeserializeAndSaveFieldExpression,

				Expression.Call( fieldContext.DeserializeParams.Logger, _logFieldEnd ) );

		private class ParseFieldProviderExpressions
		{
			public ParseFieldProviderExpressions( IFieldProvider provider, IFieldContext fieldContext )
			{
				StartingIndex = Expression.Constant( provider.StartingIndex );
				DeserializeAndSaveFieldExpression = provider.DeserializeAndSaveFieldExpression( fieldContext );
			}

			public Expression StartingIndex { get; set; }
			public Expression DeserializeAndSaveFieldExpression { get; set; }
		}

		protected override Expression CreateShouldSkipExpression() => Expression.Equal( ShouldSkipValueParam, Expression.Constant( null ) );
	}
}
