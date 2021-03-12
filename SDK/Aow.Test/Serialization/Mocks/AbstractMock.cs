using System;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	abstract class AbstractMock: IEquatable<AbstractMock>
	{
		[Field( 0x30 )] public int I30 { get; set; }

		#region IEquatable<AbstractMock> Members

		public bool Equals( AbstractMock other )
		{
			if ( other == null || GetType() != other.GetType() )
				return false;

			Type type = GetType();
			if ( type == typeof( DerivedOne ) )
				return ( this as DerivedOne ).Equals( other as DerivedOne );

			if ( type == typeof( DerivedTwo ) )
				return ( this as DerivedTwo ).Equals( other as DerivedTwo );

			throw new NotSupportedException();
		}

		public override bool Equals( object obj ) => Equals( obj as AbstractMock );

		public override int GetHashCode() => I30.GetHashCode();

		#endregion
	}

	[AowClass( ID = 0x11111111 )]
	class DerivedOne: AbstractMock, IEquatable<DerivedOne>
	{
		[Field( 0x40 )] public int I40 { get; set; }

		#region IEquatable<DerivedOne> Members

		public bool Equals( DerivedOne other ) => other != null &&
				I30 == other.I30 &&
				I40 == other.I40;

		public override bool Equals( object obj ) => Equals( obj as DerivedOne );

		public override int GetHashCode() => base.GetHashCode() * 23 + I40.GetHashCode();

		#endregion
	}

	[AowClass( ID = 0x22222222 )]
	class DerivedTwo: DerivedOne, IEquatable<DerivedTwo>
	{
		[Field( 0x50 )] public int I50 { get; set; }

		#region IEquatable<DerivedTwo> Members

		public bool Equals( DerivedTwo other ) => other != null &&
				I30 == other.I30 &&
				I40 == other.I40 &&
				I50 == other.I50;

		public override bool Equals( object obj ) => Equals( obj as DerivedTwo );

		public override int GetHashCode() => base.GetHashCode() * 23 + I50.GetHashCode();

		#endregion
	}
}
