using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders
{
	class DictionaryFieldProvider : EnumerationFieldProvider
	{
		private PropertyInfo _dictCount;
		private PropertyInfo _dictItem;
		private MethodInfo _dictAdd;
		private PropertyInfo _dictKeys;

		public DictionaryFieldProvider( Type type )
			: base( type ) => FindDictionaryMethods();

		private static IFieldProviderFactory _factory = new DictionaryFieldProviderFactory();
		public static IFieldProviderFactory Factory => _factory;

		private void FindDictionaryMethods()
		{
			BindingFlags propertySearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
			_dictCount = Type.GetProperty( "Count", propertySearchFlags );
			_dictItem = Type.GetProperty( "Item", propertySearchFlags );
			_dictKeys = Type.GetProperty( "Keys", propertySearchFlags );
			_dictAdd = Type.GetMethod( "Add", propertySearchFlags, null, new Type[] { typeof( int ), ItemType }, null );
		}

		public static bool ImplementsIDictionary( Type type )
			=> type.IsGenericType &&
			type.GetGenericTypeDefinition() == typeof( IDictionary<,> ) &&
			type.GetGenericArguments()[ 0 ] == typeof( int );

		public override Expression KeyEnumerableExpression( IOffsetMapBuilderContext context ) => Expression.Property( context.SerializeParams.Value, _dictKeys );

		protected override Type GetItemType()
		{
			Type dictionaryInterface = Type.GetInterfaces()
				.Where( i => i.IsGenericType )
				.Where( i => ImplementsIDictionary( i ) )
				.SingleOrDefault();

			return dictionaryInterface.GetGenericArguments()[ 1 ];
		}

		protected override Expression GetItemExpression( IFieldContext context ) => Expression.Property( context.SerializeParams.Value, _dictItem, context.Key );

		protected override Expression AddItemExpression( IItemFieldContext context ) => Expression.Call( context.ParseFieldParams.Result, _dictAdd, context.Key, context.Item );
	}
}
