using System;

namespace Aow2
{
	[AttributeUsage( AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true )]
	public sealed class FieldAttribute : Attribute
	{
		public FieldAttribute( int id ) => ID = id;

		public int ID { get; set; }
		public bool ImportOnly { get; set; }
		public int Order { get; set; }
	}
}
