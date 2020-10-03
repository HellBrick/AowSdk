using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Threading;

namespace Utils.Collections
{
	public class ObservableDictionary<TKey, TValue> :
		INotifyCollectionChanged, IDictionary<TKey, TValue>,
		ICollection<TValue>,
		IList<TValue>,
		IList,
		IEnumerable<TValue>
	{
		public ObservableDictionary( Func<TValue, TKey> keyExtractor ) => _keyExtractor = keyExtractor;

		private Func<TValue, TKey> _keyExtractor;
		private ObservableCollection<TValue> _collection = new ObservableCollection<TValue>();
		private Dictionary<TKey, TValue> _map = new Dictionary<TKey, TValue>();

		private SpinLock _lockAdding = new SpinLock();

		/// <summary>
		/// Tries to add item in a thread-safe way.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool TryAdd( TValue item )
		{
			bool isLockTaken = false;
			try
			{
				_lockAdding.Enter( ref isLockTaken );

				if ( Contains( item ) )
				{
					return false;
				}
				else
				{
					Add( item );
					return true;
				}
			}
			finally
			{
				if ( isLockTaken )
					_lockAdding.Exit();
			}
		}

		#region Члены IDictionary<TKey,TValue>

		public void Add( TKey key, TValue value )
		{
			_map.Add( key, value );
			_collection.Add( value );
		}

		public bool ContainsKey( TKey key ) => _map.ContainsKey( key );

		public ICollection<TKey> Keys => _map.Keys;

		public bool Remove( TKey key ) => TryRemove( key );/*TValue value = _map[key];
			_collection.Remove( value );
			return _map.Remove( key );*/

		public bool TryRemove( TKey key )
		{
			bool isLockTaken = false;
			try
			{
				_lockAdding.Enter( ref isLockTaken );

				if ( !ContainsKey( key ) )
				{
					return false;
				}
				else
				{
					TValue value = _map[key];
					_collection.Remove( value );
					return _map.Remove( key );
				}
			}
			finally
			{
				if ( isLockTaken )
					_lockAdding.Exit();
			}
		}

		public bool TryGetValue( TKey key, out TValue value ) => _map.TryGetValue( key, out value );

		public ICollection<TValue> Values => _map.Values;

		public TValue this[TKey key]
		{
			get => _map[ key ];
			set
			{
				TValue old_value = _map[key];
				int index = _collection.IndexOf( old_value );
				_collection.RemoveAt( index );
				_map[key] = value;
				_collection.Insert( index, value );
			}
		}

		#endregion

		#region Члены ICollection<KeyValuePair<TKey,TValue>>

		public void Add( KeyValuePair<TKey, TValue> item ) => Add( item.Key, item.Value );

		public void Clear()
		{
			_map.Clear();
			_collection.Clear();
		}

		public bool Contains( KeyValuePair<TKey, TValue> item ) => _map.ContainsKey( item.Key );

		public void CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex ) => ( _map as ICollection<KeyValuePair<TKey, TValue>> ).CopyTo( array, arrayIndex );

		public int Count => _collection.Count;

		public bool IsReadOnly => false;

		public bool Remove( KeyValuePair<TKey, TValue> item ) => TryRemove( item.Key );

		#endregion

		#region Члены IEnumerable<KeyValuePair<TKey,TValue>>

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => _map.GetEnumerator();

		#endregion

		#region Члены IEnumerable

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _map.GetEnumerator();

		#endregion

		#region Члены INotifyCollectionChanged

		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add { _collection.CollectionChanged += value; }
			remove { _collection.CollectionChanged -= value; }
		}

		#endregion

		#region Члены ICollection<TValue>

		public void Add( TValue item ) => Add( _keyExtractor( item ), item );

		public bool Contains( TValue item ) => ContainsKey( _keyExtractor( item ) );

		public void CopyTo( TValue[] array, int arrayIndex ) => _collection.CopyTo( array, arrayIndex );

		public bool Remove( TValue item ) => TryRemove( _keyExtractor( item ) );

		#endregion

		#region Члены IEnumerable<TValue>

		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => _collection.GetEnumerator();

		#endregion

		#region Члены IList<TValue>

		int IList<TValue>.IndexOf( TValue item ) => _collection.IndexOf( item );

		void IList<TValue>.Insert( int index, TValue item )
		{
			_collection.Insert( index, item );
			_map.Add( _keyExtractor( item ), item );
		}

		void IList<TValue>.RemoveAt( int index )
		{
			TValue value = _collection[index];
			_collection.RemoveAt( index );
			_map.Remove( _keyExtractor( value ) );
		}

		TValue IList<TValue>.this[int index]
		{
			get => _collection[ index ];
			set
			{
				TValue old_value = _collection[index];
				_collection[index] = value;
				_map.Remove( _keyExtractor( old_value ) );
				_map.Add( _keyExtractor( value ), value );
			}
		}

		#endregion

		#region Члены IList

		int IList.Add( object value )
		{
			Add( (TValue)value );
			return _collection.IndexOf( (TValue)value );
		}

		void IList.Clear() => Clear();

		bool IList.Contains( object value ) => Contains( (TValue) value );

		int IList.IndexOf( object value ) => ( this as IList<TValue> ).IndexOf( (TValue) value );

		void IList.Insert( int index, object value ) => ( this as IList<TValue> ).Insert( index, (TValue) value );

		bool IList.IsFixedSize => false;

		bool IList.IsReadOnly => IsReadOnly;

		void IList.Remove( object value ) => Remove( (TValue) value );

		void IList.RemoveAt( int index ) => ( this as IList<TValue> ).RemoveAt( index );

		object IList.this[int index]
		{
			get => ( this as IList<TValue> )[ index ];
			set => ( this as IList<TValue> )[ index ] = (TValue) value;
		}

		#endregion

		#region Члены ICollection

		void ICollection.CopyTo( Array array, int index ) => ( _collection as ICollection ).CopyTo( array, index );

		int ICollection.Count => ( _collection as ICollection ).Count;

		bool ICollection.IsSynchronized => ( _collection as ICollection ).IsSynchronized;

		object ICollection.SyncRoot => ( _collection as ICollection ).SyncRoot;

		#endregion

		#region Члены IEnumerable<TValue>

		public IEnumerator<TValue> GetEnumerator() => _collection.GetEnumerator();

		#endregion
	}
}
