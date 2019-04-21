using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Aow2.Serialization;

namespace Aow2.Collections
{
	[SuppressListSerialization]
	internal class EnumByteList<T> : IList<T>, ICustomFormatted where T : struct
	{
		private ByteList _list = new ByteList();

		static EnumByteList()
		{
			Type type = typeof( T );
			if ( !type.IsEnum || type.GetEnumUnderlyingType() != typeof( byte ) )
				throw new InvalidCastException( String.Format( "Type {0} can't be converted to Byte", typeof( T ) ) );
		}

		private T ToEnum( byte item ) => Unsafe.As<byte, T>( ref item );

		private byte ToByte( T item ) => Convert.ToByte( item );

		#region IList<T> Members

		public int IndexOf( T item ) => _list.IndexOf( ToByte( item ) );

		public void Insert( int index, T item ) => _list.Insert( index, ToByte( item ) );

		public void RemoveAt( int index ) => _list.RemoveAt( index );

		public T this[ int index ]
		{
			get => ToEnum( _list[ index ] );
			set => _list[ index ] = ToByte( value );
		}

		#endregion

		#region ICollection<T> Members

		public void Add( T item ) => _list.Add( ToByte( item ) );

		public void Clear() => _list.Clear();

		public bool Contains( T item ) => _list.Contains( ToByte( item ) );

		public void CopyTo( T[] array, int arrayIndex ) => _list.Select( b => ToEnum( b ) ).ToList().CopyTo( array, arrayIndex );

		public int Count => _list.Count;

		bool ICollection<T>.IsReadOnly => ( _list as ICollection<byte> ).IsReadOnly;

		public bool Remove( T item ) => _list.Remove( ToByte( item ) );

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator() => _list.Cast<T>().GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region ICustomFormatted Members

		void ICustomFormatted.Serialize( System.IO.Stream outStream ) => ( _list as ICustomFormatted ).Serialize( outStream );

		void ICustomFormatted.Deserialize( System.IO.Stream inStream, long length ) => ( _list as ICustomFormatted ).Deserialize( inStream, length );

		bool ICustomFormatted.ShouldBeOmitted() => ( _list as ICustomFormatted ).ShouldBeOmitted();

		#endregion
	}
}
