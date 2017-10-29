using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Utils.Runtime
{
	public static partial class Reflection
	{
		#region Constructor

		public static ConstructorInfo Constructor( Expression<Action> expr ) => ConstructorImpl( expr as LambdaExpression );

		public static ConstructorInfo Constructor<TArg>( Expression<Action<TArg>> expr ) => ConstructorImpl( expr as LambdaExpression );

		public static ConstructorInfo Constructor<TArg1, TArg2>( Expression<Action<TArg1, TArg2>> expr ) => ConstructorImpl( expr as LambdaExpression );

		private static ConstructorInfo ConstructorImpl( LambdaExpression expr ) => ( expr.Body as NewExpression ).Constructor;

		#endregion

		#region Method

		public static MethodInfo Method<TArg>( Expression<Action<TArg>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg1, TArg2>( Expression<Action<TArg1, TArg2>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg1, TArg2, TArg3>( Expression<Action<TArg1, TArg2, TArg3>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg1, TArg2, TArg3, TArg4>( Expression<Action<TArg1, TArg2, TArg3, TArg4>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg>( Expression<Func<TArg>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg1, TArg2>( Expression<Func<TArg1, TArg2>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg1, TArg2, TArg3>( Expression<Func<TArg1, TArg2, TArg3>> expr ) => MethodImpl( expr as LambdaExpression );

		public static MethodInfo Method<TArg1, TArg2, TArg3, TArg4>( Expression<Func<TArg1, TArg2, TArg3, TArg4>> expr ) => MethodImpl( expr as LambdaExpression );

		private static MethodInfo MethodImpl( LambdaExpression expr ) => ( expr.Body as MethodCallExpression ).Method;

		#endregion

		#region Property

		public static PropertyInfo Property<TProperty>( Expression<Func<TProperty>> expr ) => PropertyImpl( expr as LambdaExpression );

		public static PropertyInfo Property<TObject, TProperty>( Expression<Func<TObject, TProperty>> expr ) => PropertyImpl( expr as LambdaExpression );

		private static PropertyInfo PropertyImpl( LambdaExpression lambdaExpression ) => ( lambdaExpression.Body as MemberExpression ).Member as PropertyInfo;

		#endregion
	}
}
