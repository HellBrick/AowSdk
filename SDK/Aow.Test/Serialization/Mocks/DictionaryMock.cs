using System.Collections.Generic;

namespace Aow2.Test.Serialization.Mocks
{
	class DictionaryMock: IDictionary<int, ListItemMock>
	{
		public DictionaryMock() => Dictionary = new Dictionary<int, ListItemMock>();

		public Dictionary<int, ListItemMock> Dictionary { get; set; }

		#region IDictionary interfaces

		#region IDictionary<int,ListItemMock> Members

		public void Add( int key, ListItemMock value ) => Dictionary.Add( key, value );

		public bool ContainsKey( int key ) => Dictionary.ContainsKey( key );

		public ICollection<int> Keys => Dictionary.Keys;

		public bool Remove( int key ) => Dictionary.Remove( key );

		public bool TryGetValue( int key, out ListItemMock value ) => Dictionary.TryGetValue( key, out value );

		public ICollection<ListItemMock> Values => Dictionary.Values;

		public ListItemMock this[int key]
		{
			get => Dictionary[ key ];
			set => Dictionary[ key ] = value;
		}

		#endregion

		#region ICollection<KeyValuePair<int,ListItemMock>> Members

		public void Add( KeyValuePair<int, ListItemMock> item ) => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).Add( item );

		public void Clear() => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).Clear();

		public bool Contains( KeyValuePair<int, ListItemMock> item ) => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).Contains( item );

		public void CopyTo( KeyValuePair<int, ListItemMock>[] array, int arrayIndex ) => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).CopyTo( array, arrayIndex );

		public int Count => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).Count;

		public bool IsReadOnly => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).IsReadOnly;

		public bool Remove( KeyValuePair<int, ListItemMock> item ) => ( Dictionary as ICollection<KeyValuePair<int, ListItemMock>> ).Remove( item );

		#endregion

		#region IEnumerable<KeyValuePair<int,ListItemMock>> Members

		public IEnumerator<KeyValuePair<int, ListItemMock>> GetEnumerator() => ( Dictionary as IEnumerable<KeyValuePair<int, ListItemMock>> ).GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ( Dictionary as System.Collections.IEnumerable ).GetEnumerator();

		#endregion

		#endregion
	}
}
