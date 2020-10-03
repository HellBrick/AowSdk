using System;
using System.Threading;

namespace Utils.Threading
{
	class SpinLockHelper
	{
		private SpinLock _lock = new SpinLock();

		public void InvokeLocked( Action action ) => InvokeLockedInternal( action );

		public T InvokeLocked<T>( Func<T> action )
		{
			T result = default( T );
			InvokeLockedInternal( () => result = action() );
			return result;
		}

		private void InvokeLockedInternal( Action action )
		{
			bool lockTaken = false;

			try
			{
				_lock.Enter( ref lockTaken );
				action();
			}
			finally
			{
				if ( lockTaken )
					_lock.Exit();
			}
		}
	}
}
