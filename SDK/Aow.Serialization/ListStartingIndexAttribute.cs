using System;

namespace Aow2.Serialization
{
	[AttributeUsage( AttributeTargets.Class, Inherited = false, AllowMultiple = false )]
	public sealed class ListStartingIndexAttribute: Attribute
	{
		public ListStartingIndexAttribute( int startingIndex ) => StartingID = startingIndex;

		public int StartingID { get; set; }
	}
}