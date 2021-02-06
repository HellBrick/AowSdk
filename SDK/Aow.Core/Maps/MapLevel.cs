namespace Aow2.Maps
{
	[AowClass( ID = 0x01100003 )]
	public class MapLevel
	{
		[Field( 0x14 )] public int Width { get; set; }
		[Field( 0x15 )] public int Height { get; set; }
		[Field( 0x16 )] public MapLevelType Type { get; set; }

		[Field( 0x0a, Order = 1 )] public UnknownData Data { get; set; }
	}

	public enum MapLevelType : int
	{
		Surface = 0,
		Underground = 1,
		ShadowWorld = 3
	}
}
