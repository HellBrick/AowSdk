using System;
using System.Collections.Generic;
using System.Linq;
using Aow2.Serialization;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	[ListStartingIndex( 0x32 )]
	public class ListClassMock: List<int>, IEquatable<ListClassMock>
	{
		[Field( 0x0A )]
		public int Field0A { get; set; }

		#region IEquatable<ListClassMock> Members

		public bool Equals( ListClassMock other ) => other != null &&
				Field0A == other.Field0A &&
				Enumerable.SequenceEqual( this, other );

		public override bool Equals( object obj ) => Equals( obj as ListClassMock );

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				int prime = 23;

				hash = hash * prime + Field0A.GetHashCode();
				foreach ( int item in this )
					hash = hash * prime + item.GetHashCode();

				return hash;
			}
		}

		#endregion
	}
}
