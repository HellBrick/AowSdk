using System;

namespace Aow2
{
	[AttributeUsage( AttributeTargets.Class, Inherited = true, AllowMultiple = false )]
	public sealed class SuppressListSerializationAttribute : Attribute
	{
	}
}
