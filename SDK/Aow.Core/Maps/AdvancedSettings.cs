namespace Aow2.Maps
{
	[AowClass]
	public class AdvancedSettings
	{
		[Field( 0x15 )] public bool NoOutpostBuilding { get; set; }
		[Field( 0x16 )] public int? MaxHeroes { get; set; }
		[Field( 0x17 )] public int? HeroLevelLimit { get; set; }
		[Field( 0x18 )] public bool NoCityGrowth { get; set; }
		[Field( 0x19 )] public bool NoItemTeleportation { get; set; }
		[Field( 0x1a )] public UnknownData TBitList_DisabledBuildings { get; set; }
		[Field( 0x1c )] public bool NoSurrender { get; set; }
		[Field( 0xA0 )] public bool NoDigging { get; set; }

		#region Generated
		[Field( 0x1b )] public byte? u_enum_1b;
		#endregion
	}
}
