using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b02 )]
	public class ManaRewardEvent : MultiplePlayersEvent
	{
		[Field( 0x46 )] public int MinAmount { get; set; }
		[Field( 0x47 )] public int MaxAmount { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Mana] + {0} ~ {1}", MinAmount, MaxAmount );
	}
}
