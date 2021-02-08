using System;
using Utils.Text;

namespace Aow2
{
	[AowClass( ID = 0x01100136 )]
	public class Ability
	{
		[Field( 0x14 )]
		public int? AbilityResourceIndex { get; set; }
		[Field( 0x15 )]
		public sbyte? Level { get; set; }

		public string AbilityName => ( (AbilityID) ( AbilityResourceIndex ?? 0 ) ).ToSplitWordString();

		public override string ToString()
		{
			string level_str = "";

			if ( Level > 1 )
				level_str = String.Format( " {0}", ( (byte) Level ).ToRomanString() );

			if ( Level < 1 )
				level_str = Level.ToString();

			return String.Format( "{0}{1}", AbilityName, level_str );
		}
	}
}
