using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aow2.Serialization.Internal
{
	internal static class FormatterManager
	{
		private static Dictionary<FormatterRequest, Task<IFormatter>> _formatters = new Dictionary<FormatterRequest, Task<IFormatter>>();
		private static SpinLock _lock;

		public static Formatter<T> GetFormatter<T>( bool isPolymorph ) => GetFormatterAsync( typeof( T ), isPolymorph ).Result as Formatter<T>;

		public static Task<IFormatter> GetFormatterAsync( Type type, bool isPolymorph )
		{
			FormatterRequest key = new FormatterRequest( type, isPolymorph );
			if ( _formatters.TryGetValue( key, out Task<IFormatter> task ) )
				return task;

			TaskCompletionSource<IFormatter> completionSource = null;

			bool lockTaken = false;

			try
			{
				_lock.Enter( ref lockTaken );

				if ( _formatters.TryGetValue( key, out task ) )
					return task;

				completionSource = new TaskCompletionSource<IFormatter>();
				_formatters.Add( key, completionSource.Task );
			}
			finally
			{
				if ( lockTaken )
					_lock.Exit();
			}

			IFormatter formatter = CreateFormatter( key );
			completionSource.SetResult( formatter );

			return completionSource.Task;
		}

		private static IFormatter CreateFormatter( FormatterRequest request )
		{
			IFormatterBuilder builder = BuilderResolver.GetBuilder( request );
			return builder.Create( request.Type );
		}
	}
}
