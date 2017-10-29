using System;
using System.Collections.Generic;

namespace Aow2.Test.Serialization.Mocks
{
	[SuppressListSerialization]
	class SuppressedListMock: IList<int>
	{
		#region IList<int> Members

		public int IndexOf( int item ) => throw new NotImplementedException();

		public void Insert( int index, int item ) => throw new NotImplementedException();

		public void RemoveAt( int index ) => throw new NotImplementedException();

		public int this[int index]
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#region ICollection<int> Members

		public void Add( int item ) => throw new NotImplementedException();

		public void Clear() => throw new NotImplementedException();

		public bool Contains( int item ) => throw new NotImplementedException();

		public void CopyTo( int[] array, int arrayIndex ) => throw new NotImplementedException();

		public int Count => throw new NotImplementedException();

		public bool IsReadOnly => throw new NotImplementedException();

		public bool Remove( int item ) => throw new NotImplementedException();

		#endregion

		#region IEnumerable<int> Members

		public IEnumerator<int> GetEnumerator() => throw new NotImplementedException();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new NotImplementedException();

		#endregion
	}
}
