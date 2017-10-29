using System.Collections;
using System.Collections.Generic;

namespace Aow2.Collections
{
	[AowClass]
	[SuppressListSerialization]
	public class AowDictionary<T> : IDictionary<int, T>
	{
		public AowDictionary() { }

		[Field( 0x01 )]
		protected Dictionary<int, T> InnerDictionary = new Dictionary<int, T>();

		#region Virtual

		protected virtual void OnItemAdded( int key, T item )
		{
		}

		#endregion

		#region IDictionary<int,T> Members

		public void Add( int key, T value )
		{
			InnerDictionary.Add( key, value );
			OnItemAdded( key, value );
		}

		public bool ContainsKey( int key ) => InnerDictionary.ContainsKey( key );

		public ICollection<int> Keys => InnerDictionary.Keys;

		public bool Remove( int key ) => InnerDictionary.Remove( key );

		public bool TryGetValue( int key, out T value ) => InnerDictionary.TryGetValue( key, out value );

		public ICollection<T> Values => InnerDictionary.Values;

		public T this[ int key ]
		{
			get => InnerDictionary[ key ];
			set => InnerDictionary[ key ] = value;
		}

		#endregion

		#region ICollection<KeyValuePair<int,T>> Members

		public void Add( KeyValuePair<int, T> item )
		{
			( InnerDictionary as ICollection<KeyValuePair<int, T>> ).Add( item );
			OnItemAdded( item.Key, item.Value );
		}

		public void Clear() => ( InnerDictionary as ICollection<KeyValuePair<int, T>> ).Clear();

		public bool Contains( KeyValuePair<int, T> item ) => ( InnerDictionary as ICollection<KeyValuePair<int, T>> ).Contains( item );

		public void CopyTo( KeyValuePair<int, T>[] array, int arrayIndex ) => ( InnerDictionary as ICollection<KeyValuePair<int, T>> ).CopyTo( array, arrayIndex );

		public int Count => ( InnerDictionary as ICollection<KeyValuePair<int, T>> ).Count;

		public bool IsReadOnly => ( InnerDictionary as ICollection<KeyValuePair<int, T>> ).IsReadOnly;

		public bool Remove( KeyValuePair<int, T> item ) => ( InnerDictionary as ICollection<KeyValuePair<int, T>> ).Remove( item );

		#endregion

		#region IEnumerable<KeyValuePair<int,T>> Members

		public IEnumerator<KeyValuePair<int, T>> GetEnumerator() => ( InnerDictionary as IEnumerable<KeyValuePair<int, T>> ).GetEnumerator();

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator() => ( InnerDictionary as IEnumerable ).GetEnumerator();

		#endregion
	}
}
