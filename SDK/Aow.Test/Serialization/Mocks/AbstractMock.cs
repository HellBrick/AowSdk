using System;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	abstract class AbstractMock: IEquatable<AbstractMock>
	{
		[Field( 0xA0 )] public int A0 { get; set; }

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

		public override int GetHashCode() => A0.GetHashCode();

		#endregion
	}

	[AowClass( ID = 0x11111111 )]
	class DerivedOne: AbstractMock, IEquatable<DerivedOne>
	{
		[Field( 0xB0 )] public int B0 { get; set; }

		#region IEquatable<DerivedOne> Members

		public bool Equals( DerivedOne other ) => other != null &&
				A0 == other.A0 &&
				B0 == other.B0;

		public override bool Equals( object obj ) => Equals( obj as DerivedOne );

		public override int GetHashCode() => base.GetHashCode() * 23 + B0.GetHashCode();

		#endregion
	}

	[AowClass( ID = 0x22222222 )]
	class DerivedTwo: DerivedOne, IEquatable<DerivedTwo>
	{
		[Field( 0xC0 )] public int C0 { get; set; }

		#region IEquatable<DerivedTwo> Members

		public bool Equals( DerivedTwo other ) => other != null &&
				A0 == other.A0 &&
				B0 == other.B0 &&
				C0 == other.C0;

		public override bool Equals( object obj ) => Equals( obj as DerivedTwo );

		public override int GetHashCode() => base.GetHashCode() * 23 + C0.GetHashCode();

		#endregion
	}
}
