using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b04 )]
	public class LoseGameEvent : MultiplePlayersEvent
	{
		public override string ToString() => base.ToString() + String.Format( " [Lose game] {0}", Players );
	}
}
