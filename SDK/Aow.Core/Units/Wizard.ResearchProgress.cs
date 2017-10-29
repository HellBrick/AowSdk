using System;
using System.Collections.Generic;

namespace Aow2.Units
{
	public interface IResearchProgress
	{
		int this[int slot] { get; set; }
	}

	public partial class Wizard
	{
		private abstract class ResearchProgress: IResearchProgress
		{
			private Wizard _wizard;
			private List<Accessor> _accessors;

			protected ResearchProgress( Wizard wizard )
			{
				_wizard = wizard;
				_accessors = new List<Accessor>( SlotAccessors() );
			}

			protected abstract IEnumerable<Accessor> SlotAccessors();

			#region IResearchProgress Members

			public int this[int slot]
			{
				get
				{
					ValidateSlot( slot );
					return _accessors[slot].Get( _wizard );
				}
				set
				{
					ValidateSlot( slot );
					_accessors[slot].Set( _wizard, value );
				}
			}

			private void ValidateSlot( int slot )
			{
				if ( ( slot < 0 ) || ( slot > 7 ) )
					throw new ArgumentException( "Slot number must fall in 0..7 interval.", "slot" );
			}

			#endregion

			protected struct Accessor
			{
				public Func<Wizard, int> Get { get; set; }
				public Action<Wizard, int> Set { get; set; }
			}
		}

		private class SpellResearchProgress: ResearchProgress
		{
			public SpellResearchProgress( Wizard wizard )
				: base( wizard )
			{
			}

			protected override IEnumerable<ResearchProgress.Accessor> SlotAccessors()
			{
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress0, Set = ( w, v ) => w._spellProgress0 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress1, Set = ( w, v ) => w._spellProgress1 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress2, Set = ( w, v ) => w._spellProgress2 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress3, Set = ( w, v ) => w._spellProgress3 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress4, Set = ( w, v ) => w._spellProgress4 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress5, Set = ( w, v ) => w._spellProgress5 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress6, Set = ( w, v ) => w._spellProgress6 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._spellProgress7, Set = ( w, v ) => w._spellProgress7 = v };
			}
		}

		private class SkillResearchProgress: ResearchProgress
		{
			public SkillResearchProgress( Wizard wizard )
				: base( wizard )
			{
			}

			protected override IEnumerable<ResearchProgress.Accessor> SlotAccessors()
			{
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress0, Set = ( w, v ) => w._skillProgress0 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress1, Set = ( w, v ) => w._skillProgress1 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress2, Set = ( w, v ) => w._skillProgress2 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress3, Set = ( w, v ) => w._skillProgress3 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress4, Set = ( w, v ) => w._skillProgress4 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress5, Set = ( w, v ) => w._skillProgress5 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress6, Set = ( w, v ) => w._skillProgress6 = v };
				yield return new ResearchProgress.Accessor() { Get = ( w ) => w._skillProgress7, Set = ( w, v ) => w._skillProgress7 = v };
			}
		}
	}
}
