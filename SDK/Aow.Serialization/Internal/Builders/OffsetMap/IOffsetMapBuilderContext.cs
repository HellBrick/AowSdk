using Aow2.Serialization.Internal.Builders.Base;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	interface IOffsetMapBuilderContext: IBuilderContext
	{
		FillFieldListParameters FillFieldListParams { get; }
		ParseFieldParameters ParseFieldParams { get; }
	}
}
