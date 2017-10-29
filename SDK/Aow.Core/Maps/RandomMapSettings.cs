namespace Aow2.Maps
{
	[AowClass]
	public abstract class WorldMapBuilderSettings
	{
		[Field( 0x14 )] public int Width { get; set; }
		[Field( 0x15 )] public int Heigth { get; set; }
		[Field( 0x18 )] public int Seed { get; set; }

		[Field( 0xa0 )] public bool DisableUndergroundStart { get; set; }
		[Field( 0xa1 )] public bool DisableShadowWorldStart { get; set; }

		[Field( 0x17 )] public int u_17;
		[Field( 0x19 )] public byte u_enum_19;
		[Field( 0x1a )] public bool u_1a;
		[Field( 0x1b )] public bool u_1b;
		[Field( 0x1c )] public byte u_enum_1c;
		[Field( 0x1d )] public byte u_enum_1d;
		[Field( 0x1e )] public byte u_enum_1e;
		[Field( 0x1f )] public byte u_enum_1f;
		[Field( 0x20 )] public byte u_enum_20;
		[Field( 0x21 )] public byte u_enum_21;
		[Field( 0x22 )] public byte u_enum_22;
		[Field( 0x23 )] public UnknownData u_23;
		[Field( 0x24 )] public UnknownData u_24;
		[Field( 0x25 )] public UnknownData u_25;
		[Field( 0x26 )] public bool u_26;
	}

	[AowClass( ID = 0x011001bc )]
	public class RandomMapSettings : WorldMapBuilderSettings
	{

	}
}
