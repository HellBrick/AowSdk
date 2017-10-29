using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Utils.Runtime;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	class ListFieldProvider: EnumerationFieldProvider
	{
		private static MethodInfo _enumerableRange = Reflection.Method( ( int s, int c ) => Enumerable.Range( s, c ) );

		private PropertyInfo _listCount;
		private PropertyInfo _listItem;
		private MethodInfo _listAdd;

		public ListFieldProvider( Type type )
			: base( type ) => FindListMethods();

		private static IFieldProviderFactory _factory = new ListFieldProviderFactory();
		public static IFieldProviderFactory Factory => _factory;

		private void FindListMethods()
		{
			BindingFlags propertySearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
			_listCount = Type.GetProperty( "Count", propertySearchFlags );
			_listItem = Type.GetProperty( "Item", propertySearchFlags );
			_listAdd = Type.GetMethod( "Add", propertySearchFlags, null, new Type[] { ItemType }, null );
		}

		public static bool ImplementsIList( Type type ) => type.IsGenericType &&
				type.GetGenericTypeDefinition() == typeof( IList<> );

		public static bool IsListSuppressed( Type type ) => type.GetCustomAttribute<SuppressListSerializationAttribute>() != null;

		public override Expression KeyEnumerableExpression( IOffsetMapBuilderContext context ) => Expression.Call(
				null,
				_enumerableRange,
				Expression.Constant( 0 ),
				Expression.Property(
					context.SerializeParams.Value,
					_listCount )
				);

		protected override Type GetItemType()
		{
			Type listInterface = Type.GetInterfaces()
				.Where( i => i.IsGenericType )
				.Where( i => ImplementsIList( i ) )
				.SingleOrDefault();

			return listInterface.GetGenericArguments()[0];
		}

		protected override Expression GetItemExpression( IFieldContext context ) => Expression.Property( context.SerializeParams.Value, _listItem, context.Key );

		protected override Expression AddItemExpression( IItemFieldContext context )
		{
			LabelTarget labelBreak = Expression.Label( "_break" );

			return Expression.Block(
				Expression.Loop(
					Expression.IfThenElse(
						Expression.LessThan(
							Expression.Add(
								Expression.Property( context.ParseFieldParams.Result, _listCount ),
								Expression.Constant( 1 ) ),

							context.Key ),

						Expression.Call( context.ParseFieldParams.Result, _listAdd, Expression.Default( ItemType ) ),
						Expression.Break( labelBreak ) ),

					labelBreak ),

				Expression.Call( context.ParseFieldParams.Result, _listAdd, context.Item ) );
		}
	}
}
