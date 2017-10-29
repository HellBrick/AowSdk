using Aow2.Collections;

namespace Aow2.Modding.Units
{
	[AowClass( ID = 0x01100133 )]
	public class UnitModel : EResource
	{
		public UnitModel()
		{
			ScaleTC = 1.6f;
			ScaleWM = 1.4f;
			BlockChance = 1;
			BloodType = 1;
			MoveStep2Pcnt = 50;
		}

		[Field( 0x14 )] private AowList<ImageSequence> _animationsStorage = new AowList<ImageSequence>();
		public UnitAnimations Animations => new UnitAnimations( _animationsStorage );

		[Field( 0x1f )] public IntegerList SpecialAnimationAbilityIDs { get; set; }

		[Field( 0x15 )] public UnknownEnum Hotspot { get; set; } //	12 bytes
		[Field( 0x16 )] public float ScaleTC { get; set; }
		[Field( 0x17 )] public float ScaleWM { get; set; }
		[Field( 0x18 )] public byte BlockChance { get; set; }
		[Field( 0x19 )] public byte BloodType { get; set; }

		[Field( 0x1b )] public MoveDebris MoveDebris { get; set; }

		[Field( 0x1c )] public int HeightOffset { get; set; }
		[Field( 0x1d )] public int MoveStep2Pcnt { get; set; }
		[Field( 0x1e )] public UnitMoveSpeed MoveSpeed { get; set; }
		[Field( 0x20 )] public int Height { get; set; }
		[Field( 0x21 )] public int Radius { get; set; }

		[Field( 0x23 )] public int GlowColorMin { get; set; }
		[Field( 0x24 )] public int GlowColorMax { get; set; }
		[Field( 0x25 )] public int GlowColorPeriod { get; set; }
		[Field( 0x26 )] public int GlowColorRange { get; set; }
		[Field( 0x27 )] public UnitEffectLocation EffectLocation { get; set; }

		[Field( 0x1a )] public UnknownData _UnknownData1a;
		[Field( 0x22 )] public UnknownData _UnknownData22;
	}

	public enum UnitMoveSpeed : byte
	{
		Slow = 0,
		Normal,
		Fast,
		VeryFast
	}

	public enum UnitEffectLocation : byte
	{
		Base = 0,
		Hotspot,
		HotspotHeight
	}

	public enum MoveDebris : byte
	{
		None = 0,
		Small,
		Medium,
		Large
	}
}
