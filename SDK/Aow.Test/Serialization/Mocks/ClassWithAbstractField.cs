using System;

namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	class ClassWithAbstractField: IEquatable<ClassWithAbstractField>
	{
		[Field(0x0A)] public AbstractMock Field { get; set; }

		#region IEquatable<AbstractMock> Members

		public bool Equals( ClassWithAbstractField other ) => other != null &&
				Field.Equals( other.Field );

		public override bool Equals( object obj ) => Equals( obj as ClassWithAbstractField );

		public override int GetHashCode() => Field.GetHashCode();

		#endregion
	}
}
