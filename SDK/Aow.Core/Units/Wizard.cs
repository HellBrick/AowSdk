using System;
using Aow2.Collections;

namespace Aow2.Units
{
	[AowClass( ID = 0x0110015e )]
	public partial class Wizard: AbstractHero
	{
		public Wizard ()
		{
			AvailableSkillsID = new IntegerList();
			AvailableSpellsID = new IntegerList();
			SpellProgress = new SpellResearchProgress( this );
			SkillProgress = new SkillResearchProgress( this );
		}

		[Field( 0x3c )] private LongPascalString _biography;
		public string Biography
		{
			get => _biography;
			set => _biography = value;
		}

		[Field( 0x3d )] public Sphere Sphere { get; set; }
		[Field( 0x4e )] public SphereList SpherePicks { get; set; }

		[Field( 0x46 )] public IntegerList AvailableSpellsID { get; set; }
		[Field( 0x4c )] public IntegerList AvailableSkillsID { get; set; }

		public IResearchProgress SpellProgress { get; private set; }
		public IResearchProgress SkillProgress { get; private set; }

		[Field( 0x43 )] public int? ResearchingSpellID { get; set; }
		[Field( 0x48 )] public int? ResearchingSkillID { get; set; }

		[Field( 0x3e )] public UnknownData _UnknownData3e;
		[Field( 0x3f )] public byte? _byte3f;
		[Field( 0x40 )] public byte? _byte40;
		[Field( 0x41 )] public sbyte _byte41 = -1;
		
		[Field( 0x45 )] public int? _int45;
		[Field( 0x47 )] public PersistentBool? _PersistentBool47;
		[Field( 0x49 )] public byte? _byte49;
		[Field( 0x4a )] public byte? _byte4a;
		[Field( 0x4b )] public bool _bool4b;
		[Field( 0x4d )] public UnknownData _UnknownData4d;
		[Field( 0x4f )] public UnknownPersistent _UnknownPersistent4f;

		[Field( 0x44 )] [Obsolete("Deprecated since MPE 1.5")] public int? ResearchRemainingCost { get; set; }

		#region Progress fields

		[Field( 0x100 )] private int? _spellProgress0;
		[Field( 0x101 )] private int? _spellProgress1;
		[Field( 0x102 )] private int? _spellProgress2;
		[Field( 0x103 )] private int? _spellProgress3;
		[Field( 0x104 )] private int? _spellProgress4;
		[Field( 0x105 )] private int? _spellProgress5;
		[Field( 0x106 )] private int? _spellProgress6;
		[Field( 0x107 )] private int? _spellProgress7;

		[Field( 0x108 )] private int? _skillProgress0;
		[Field( 0x109 )] private int? _skillProgress1;
		[Field( 0x10a )] private int? _skillProgress2;
		[Field( 0x10b )] private int? _skillProgress3;
		[Field( 0x10c )] private int? _skillProgress4;
		[Field( 0x10d )] private int? _skillProgress5;
		[Field( 0x10e )] private int? _skillProgress6;
		[Field( 0x10f )] private int? _skillProgress7;

		#endregion
	}
}
