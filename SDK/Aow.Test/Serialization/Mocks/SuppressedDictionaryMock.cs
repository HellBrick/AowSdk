using System;
using System.Collections.Generic;

namespace Aow2.Test.Serialization.Mocks
{
	[SuppressListSerialization]
	class SuppressedDictionaryMock: IDictionary<int, ListItemMock>
	{
		#region IDictionary<int,ListItemMock> Members

		public void Add( int key, ListItemMock value ) => throw new NotImplementedException();

		public bool ContainsKey( int key ) => throw new NotImplementedException();

		public ICollection<int> Keys => throw new NotImplementedException();

		public bool Remove( int key ) => throw new NotImplementedException();

		public bool TryGetValue( int key, out ListItemMock value ) => throw new NotImplementedException();

		public ICollection<ListItemMock> Values => throw new NotImplementedException();

		public ListItemMock this[int key]
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#region ICollection<KeyValuePair<int,ListItemMock>> Members

		public void Add( KeyValuePair<int, ListItemMock> item ) => throw new NotImplementedException();

		public void Clear() => throw new NotImplementedException();

		public bool Contains( KeyValuePair<int, ListItemMock> item ) => throw new NotImplementedException();

		public void CopyTo( KeyValuePair<int, ListItemMock>[] array, int arrayIndex ) => throw new NotImplementedException();

		public int Count => throw new NotImplementedException();

		public bool IsReadOnly => throw new NotImplementedException();

		public bool Remove( KeyValuePair<int, ListItemMock> item ) => throw new NotImplementedException();

		#endregion

		#region IEnumerable<KeyValuePair<int,ListItemMock>> Members

		public IEnumerator<KeyValuePair<int, ListItemMock>> GetEnumerator() => throw new NotImplementedException();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new NotImplementedException();

		#endregion
	}
}
