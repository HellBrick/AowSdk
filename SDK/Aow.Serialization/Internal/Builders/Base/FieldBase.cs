using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Aow2.Serialization.Internal.Builders.Base
{
	abstract class FieldBase
	{
		protected FieldBase( FieldAttribute attribute, Type type, Type declaringType )
		{
			ID = attribute.ID;
			Type = type;
			DeclaringType = declaringType;
			IsImportOnly = attribute.ImportOnly;
			Order = attribute.Order;
		}

		public int ID { get; private set; }
		public Type Type { get; private set; }
		public Type DeclaringType { get; }
		public bool IsImportOnly { get; private set; }
		public int Order { get; }

		public abstract Expression CreateExpression( Expression instance );
	}

	class Field : FieldBase
	{
		private FieldInfo _fieldInfo;

		public Field( FieldInfo fieldInfo, FieldAttribute attribute )
			: base( attribute, fieldInfo.FieldType, fieldInfo.DeclaringType ) => _fieldInfo = fieldInfo;

		public override Expression CreateExpression( Expression instance ) => Expression.Field( instance, _fieldInfo );
	}

	class Property : FieldBase
	{
		private PropertyInfo _propertyInfo;

		public Property( PropertyInfo propertyInfo, FieldAttribute attribute )
			: base( attribute, propertyInfo.PropertyType, propertyInfo.DeclaringType ) => _propertyInfo = propertyInfo;

		public override Expression CreateExpression( Expression instance ) => Expression.Property( instance, _propertyInfo );
	}
}
