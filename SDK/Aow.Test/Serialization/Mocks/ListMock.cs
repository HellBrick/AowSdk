using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow2.Test.Serialization.Mocks
{
	class ListMock: IList<ListItemMock>
	{
		public ListMock() => List = new List<ListItemMock>();

		public ListMock( params ListItemMock[] items ) => List = items.ToList();

		public List<ListItemMock> List { get; private set; }

		#region IList<ListItemMock> Members

		public int IndexOf( ListItemMock item ) => List.IndexOf( item );

		public void Insert( int index, ListItemMock item ) => List.Insert( index, item );

		public void RemoveAt( int index ) => List.RemoveAt( index );

		public ListItemMock this[int index]
		{
			get => List[ index ];
			set => List[ index ] = value;
		}

		#endregion

		#region ICollection<ListItemMock> Members

		public void Add( ListItemMock item ) => List.Add( item );

		public void Clear() => List.Clear();

		public bool Contains( ListItemMock item ) => List.Contains( item );

		public void CopyTo( ListItemMock[] array, int arrayIndex ) => List.CopyTo( array, arrayIndex );

		public int Count => List.Count;

		public bool IsReadOnly => ( List as ICollection<ListItemMock> ).IsReadOnly;

		public bool Remove( ListItemMock item ) => List.Remove( item );

		#endregion

		#region IEnumerable<ListItemMock> Members

		public IEnumerator<ListItemMock> GetEnumerator() => List.GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => List.GetEnumerator();

		#endregion
	}

	[AowClass( ID = 0x12345678 )]
	class ListItemMock: IEquatable<ListItemMock>
	{
		[Field( 0xAA )]
		public int AA { get; set; }

		#region IEquatable<ListItemMock> Members

		public bool Equals( ListItemMock other ) => other != null &&
				GetType() == other.GetType() &&
				AA == other.AA;

		public override bool Equals( object obj ) => Equals( obj as ListItemMock );

		public override int GetHashCode() => AA.GetHashCode();

		#endregion
	}

	[AowClass( ID = 0x77777777 )]
	class DerivedListItemMock: ListItemMock, IEquatable<DerivedListItemMock>
	{
		[Field( 0xBB )]
		public int BB { get; set; }

		#region IEquatable<DerivedListItemMock> Members

		public bool Equals( DerivedListItemMock other ) => other != null &&
				GetType() == other.GetType() &&
				AA == other.AA &&
				BB == other.BB;

		public override bool Equals( object obj ) => Equals( obj as DerivedListItemMock );

		public override int GetHashCode() => base.GetHashCode() * 23 + BB.GetHashCode();

		#endregion
	}
}
