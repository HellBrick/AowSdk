using System;

namespace Aow2
{
	[AttributeUsage( AttributeTargets.Class, Inherited = false, AllowMultiple = false )]
	public sealed class AowClassAttribute : Attribute
	{
		public AowClassAttribute() => ID = AbstractID;

		public int ID { get; set; }
		public bool InvertHierarchyFieldOrder { get; set; }

		internal const int AbstractID = Int32.MinValue;
	}
}
