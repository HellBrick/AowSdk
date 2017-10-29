using Aow2.Collections;
using Aow2.Modding.Generated;

namespace Aow2.Modding.Spells
{
	[AowClass]
	public class SpellResource
	{
		public SpellResource() => IsValidStartingSpell = true;

		public int ID { get; set; }
		public string Name { get; set; }

		[Field( 0x1a )] public SphereMask Spheres { get; set; }
		[Field( 0x1b )] public byte Level { get; set; }

		[Field( 0x17 )] public int ManaCost { get; set; }
		[Field( 0x18 )] public int Upkeep { get; set; }
		[Field( 0x19 )] public int ResearchCost { get; set; }

		[Field( 0x14 )] protected LongPascalString _description = "<No description>";
		public string Description
		{
			get => _description;
			set => _description = value;
		}

		[Field( 0x1e )] public Spirits RewardingSpirits { get; set; }

		[Field( 0x1f )] public PersistentBool IsValidStartingSpell { get; set; }

		[Field( 0x16 )]
		public AowList<ImageSequence> _icon;

		[Field( 0x0a )] public int u_0a; //	1 for Tornado
		[Field( 0x0b )] public int u_0b;
		[Field( 0x0c )] public int u_0c;
		[Field( 0x15 )] public TSFXLibrary u_15;

		[Field( 0x1c )] public TEFilename u_1c;
		[Field( 0x1d )] public int u_1d;

		public override string ToString() => Name;
	}
}
