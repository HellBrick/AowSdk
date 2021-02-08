using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Aow2.Serialization.Internal.Builders.Base
{
	internal abstract class BuilderContext : Aow2.Serialization.Internal.Builders.Base.IBuilderContext
	{
		private const MethodAttributes _staticMethodAttributes =
			MethodAttributes.Private |
			MethodAttributes.Static;

		public BuilderContext( Type targetType )
		{
			SerializeParams = new SerializeMethodParameters();
			DeserializeParams = new DeserializeMethodParameters();

			TargetType = targetType;
			SerializeParams.Value = Expression.Parameter( TargetType, "value" );
		}

		public Type TargetType { get; private set; }

		public SerializeMethodParameters SerializeParams { get; set; }
		public DeserializeMethodParameters DeserializeParams { get; set; }

		public ParameterExpression ShouldSkipValueParam { get; set; }

		public LambdaExpression SerializeExpression { get; set; }
		public LambdaExpression DeserializeExpression { get; set; }
		public LambdaExpression ShouldSkipExpression { get; set; }

		public IFormatter BuildFormatter()
		{
			Initialize();
			CreateSerializeMethod();
			CreateDeserializeMethod();
			CreateShouldSkipMethod();

			IEditableFormatter editableFormatter = Activator.CreateInstance( typeof( EditableFormatter<> ).MakeGenericType( TargetType ) ) as IEditableFormatter;
			editableFormatter.DeserializationDelegate = DeserializeExpression.Compile();
			editableFormatter.SerializationDelegate = SerializeExpression.Compile();
			editableFormatter.ShouldSkipFieldDelegate = ShouldSkipExpression.Compile();

			return editableFormatter as IFormatter;
		}

		protected virtual void Initialize()
		{
		}

		private void CreateSerializeMethod()
		{
			Expression serializeBlock = CreateSerializeExpression();
			SerializeExpression = Expression.Lambda( serializeBlock, SerializeParams.Stream, SerializeParams.Value );
		}

		protected abstract Expression CreateSerializeExpression();

		private void CreateDeserializeMethod()
		{
			Expression deserializeBlock = CreateDeserializeExpression();
			DeserializeExpression = Expression.Lambda( deserializeBlock, DeserializeParams.Stream, DeserializeParams.Offset, DeserializeParams.Length, DeserializeParams.Logger );
		}

		protected abstract Expression CreateDeserializeExpression();

		private void CreateShouldSkipMethod()
		{
			ShouldSkipValueParam = Expression.Parameter( TargetType, "value" );

			Expression shouldSkipBlock = CreateShouldSkipExpression();
			ShouldSkipExpression = Expression.Lambda( shouldSkipBlock, ShouldSkipValueParam );
		}

		protected abstract Expression CreateShouldSkipExpression();
	}
}
