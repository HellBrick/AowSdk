using System;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	public class Circular: IEquatable<Circular>
	{
		[Field( 0x0A )] public Circular Next { get; set; }
		[Field( 0x0B )] public int Value { get; set; }

		#region IEquatable<Circular> Members

		public bool Equals( Circular other ) => other != null &&
				Value == other.Value &&
				Next == other.Next;

		public override bool Equals( object obj ) => Equals( obj as Circular );

		public static bool operator ==( Circular x, Circular y ) => x == null && y == null ||
				x.Equals( y );

		public static bool operator !=( Circular x, Circular y ) => !( x == y );

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				int prime = 23;

				hash = hash * prime + Value.GetHashCode();
				hash = hash * prime + Next.GetHashCode();

				return hash;
			}
		}

		#endregion
	}
}
