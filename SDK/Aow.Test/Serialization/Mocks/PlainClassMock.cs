using System;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	public class PlainClassMock: IEquatable<PlainClassMock>
	{
		[Field( 0x0A )] public int FieldA { get; set; }
		[Field( 0x0B )] public bool FieldB { get; set; }
		[Field( 0x0C )] public int FieldC { get; set; }
				
		[Field( 0x0D )] public int FieldD { get; set; }

		#region IEquatable<PlainClassMock> Members

		public bool Equals( PlainClassMock other )
		{
			if ( other == null )
				return false;

			return
				FieldA == other.FieldA &&
				FieldB == other.FieldB &&
				FieldC == other.FieldC &&
				FieldD == other.FieldD;
		}

		public override bool Equals( object obj ) => Equals( obj as PlainClassMock );

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				int prime = 23;

				hash = hash * prime + FieldA.GetHashCode();
				hash = hash * prime + FieldB.GetHashCode();
				hash = hash * prime + FieldC.GetHashCode();
				hash = hash * prime + FieldD.GetHashCode();

				return hash;
			}
		}

		#endregion
	}
}
