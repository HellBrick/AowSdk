using Aow2.Collections;

namespace Aow2.Units
{
	[AowClass]
	public abstract class AbstractUnit
	{
		public AbstractUnit() => Abilities = new AowList<Ability>();

		[Field( 0x0a )] public byte? PlayerNumber { get; set; }
		[Field( 0x0b )] public byte? CurrentMP { get; set; }
		[Field( 0x0c )] public byte? CurrentHP { get; set; }
		[Field( 0x0d )] public AowList<Ability> Abilities { get; protected set; }

		[Field( 0x0e )] public int? _int0e;
		[Field( 0x0f )] public int? _int0f;
		[Field( 0x10 )] public byte? _byte10;
		[Field( 0x11 )] public UnknownEnum _UnknownEnum11;	//	1 bytes
	}
}

