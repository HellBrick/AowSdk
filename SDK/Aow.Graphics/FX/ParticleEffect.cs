using Aow2.Collections;

namespace Aow2.Graphics.FX
{
	[AowClass( ID = 0x00031000 )]
	public class ParticleEffect
	{
		[Field( 0x0a )]
		public ParticleSettings ParticleSettings { get; set; }

		[Field( 0x1e )]
		private ShortPascalString _name;
		public string Name
		{
			get => _name;
			set => _name = value;
		}

		[Field( 0x1f )]
		private ShortPascalString _ilbPath;
		public string ImageLibraryPath
		{
			get => _ilbPath;
			set => _ilbPath = value;
		}

		#region Generated
		[Field( 0x0b )] public int u_enum_0b { get; set; }
		[Field( 0x0c )] public int u_0c { get; set; }
		[Field( 0x0d )] public int u_0d { get; set; }
		[Field( 0x0e )] public int u_0e { get; set; }
		[Field( 0x0f )] public int u_0f { get; set; }
		[Field( 0x10 )] public int u_10 { get; set; }
		[Field( 0x11 )] public int u_11 { get; set; }
		[Field( 0x12 )] public int u_12 { get; set; }
		[Field( 0x13 )] public int u_13 { get; set; }
		[Field( 0x14 )] public int u_14 { get; set; }
		[Field( 0x19 )] public byte u_enum_19 { get; set; }
		[Field( 0x1a )] public byte u_enum_1a { get; set; }
		[Field( 0x1d )] public byte u_enum_1d { get; set; }

		[Field( 0x20 )] public int u_20 { get; set; }
		[Field( 0x21 )] public int u_21 { get; set; }
		[Field( 0x22 )] public int u_22 { get; set; }
		[Field( 0x23 )] public AowList<TriggerSettings> u_23 { get; set; }
		[Field( 0x24 )] public int u_enum_24 { get; set; }
		[Field( 0x25 )] public int u_25 { get; set; }
		[Field( 0x26 )] public int u_26 { get; set; }
		[Field( 0x2c )] public byte u_enum_2c { get; set; }
		[Field( 0x2d )] public byte u_enum_2d { get; set; }
		[Field( 0x30 )] public int u_enum_30 { get; set; }
		[Field( 0x31 )] public int u_31 { get; set; }
		[Field( 0x32 )] public int u_32 { get; set; }
		[Field( 0x33 )] public int u_33 { get; set; }
		[Field( 0x34 )] public byte u_enum_34 { get; set; }
		#endregion
	}
}
