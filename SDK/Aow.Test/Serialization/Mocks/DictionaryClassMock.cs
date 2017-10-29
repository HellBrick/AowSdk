using System;
using System.Collections.Generic;
using System.Linq;
using Aow2.Serialization;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	[ListStartingIndex( 0x32 )]
	public class DictionaryClassMock: Dictionary<int, int>, IEquatable<DictionaryClassMock>
	{
		[Field( 0x0A )]
		public int Field0A { get; set; }

		#region IEquatable<ListClassMock> Members

		public bool Equals( DictionaryClassMock other ) => other != null &&
				Field0A == other.Field0A &&
				Enumerable.SequenceEqual( this, other );

		public override bool Equals( object obj ) => Equals( obj as DictionaryClassMock );

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				int prime = 23;

				hash = hash * prime + Field0A.GetHashCode();
				foreach ( KeyValuePair<int, int> item in this )
					hash = hash * prime + item.GetHashCode();

				return hash;
			}
		}

		#endregion
	}
}
