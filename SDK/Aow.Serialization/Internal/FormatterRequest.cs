using System;

namespace Aow2.Serialization.Internal
{
	internal struct FormatterRequest : IEquatable<FormatterRequest>
	{
		public FormatterRequest( Type type, bool isPolymorph )
		{
			_type = type;
			_isPolymorph = isPolymorph;
		}

		private Type _type;
		public Type Type => _type;

		private bool _isPolymorph;
		public bool IsPolymorph => _isPolymorph;

		#region IEquatable<FormatterKey> Members

		public bool Equals( FormatterRequest other ) => Type == other.Type &&
				IsPolymorph == other.IsPolymorph;

		public override bool Equals( object obj ) => obj is FormatterRequest &&
				Equals( (FormatterRequest) obj );

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + Type.GetHashCode();
				hash = hash * 23 + IsPolymorph.GetHashCode();
				return hash;
			}
		}

		#endregion
	}
}
