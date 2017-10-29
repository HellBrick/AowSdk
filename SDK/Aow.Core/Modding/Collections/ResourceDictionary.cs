using System;
using System.Linq;
using Aow2.Collections;

namespace Aow2.Modding.Collections
{
	[AowClass]
	public class ResourceDictionary<T> : AowDictionary<T>
		where T : IResourceItem
	{
		[Field( 0x0a )]
		protected int _nextID;

		public void Add( T item )
		{
			item.ID = NextID();
			base.Add( item.ID, item );
		}

		protected override void OnItemAdded( int key, T item )
		{
			if ( key + 1 > _nextID )
				_nextID = key + 1;
		}

		private int NextID() => Math.Max( _nextID, Keys.Max() + 1 );
	}
}
