using System;

namespace Aow2.Serialization.Internal
{
	internal interface IFormatterBuilder
	{
		bool CanCreate( FormatterRequest request );
		IFormatter Create( Type type );
	}
}
