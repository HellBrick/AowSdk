using System.Collections;
using System.Collections.Generic;
using Aow2.Collections;

namespace Aow2
{
	[AowClass]
	[SuppressListSerialization]
	public class RandomUnitIDList : IList<UnitIDList>
	{
		[Field( 0x1e )]
		private AowList<UnitIDList> _collections = new AowList<UnitIDList>();

		#region IList<UnitIDList> Members

		public int IndexOf( UnitIDList item ) => _collections.IndexOf( item );

		public void Insert( int index, UnitIDList item ) => _collections.Insert( index, item );

		public void RemoveAt( int index ) => _collections.RemoveAt( index );

		public UnitIDList this[ int index ]
		{
			get => _collections[ index ];
			set => _collections[ index ] = value;
		}

		#endregion

		#region ICollection<UnitIDList> Members

		public void Add( UnitIDList item ) => _collections.Add( item );

		public void Clear() => _collections.Clear();

		public bool Contains( UnitIDList item ) => _collections.Contains( item );

		public void CopyTo( UnitIDList[] array, int arrayIndex ) => _collections.CopyTo( array, arrayIndex );

		public int Count => _collections.Count;

		public bool IsReadOnly => _collections.IsReadOnly;

		public bool Remove( UnitIDList item ) => _collections.Remove( item );

		#endregion

		#region IEnumerable<UnitIDList> Members

		public IEnumerator<UnitIDList> GetEnumerator() => _collections.GetEnumerator();

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator() => _collections.GetEnumerator();

		#endregion
	}
}
