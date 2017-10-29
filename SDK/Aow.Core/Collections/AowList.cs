using System.Collections.Generic;

namespace Aow2.Collections
{
	[AowClass]
	[SuppressListSerialization]
	public class AowList<T> : IList<T>
	{
		public AowList() => InnerList = new List<T>();

		public AowList( IEnumerable<T> collection ) => InnerList = new List<T>( collection );

		[Field( 0x01 )]
		protected List<T> InnerList { get; set; }

		#region IList<T> Members

		public int IndexOf( T item ) => InnerList.IndexOf( item );

		public void Insert( int index, T item ) => InnerList.Insert( index, item );

		public void RemoveAt( int index ) => InnerList.RemoveAt( index );

		public T this[ int index ]
		{
			get => InnerList[ index ];
			set => InnerList[ index ] = value;
		}

		#endregion

		#region ICollection<T> Members

		public void Add( T item ) => InnerList.Add( item );

		public void Clear() => InnerList.Clear();

		public bool Contains( T item ) => InnerList.Contains( item );

		public void CopyTo( T[] array, int arrayIndex ) => InnerList.CopyTo( array, arrayIndex );

		public int Count => InnerList.Count;

		public bool IsReadOnly => ( InnerList as ICollection<T> ).IsReadOnly;

		public bool Remove( T item ) => InnerList.Remove( item );

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator() => InnerList.GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}
}
