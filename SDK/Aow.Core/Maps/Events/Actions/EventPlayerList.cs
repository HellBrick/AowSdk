using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass]
	public class EventPlayerList
	{
		[Field( 0x1e )] public PlayerBitSet? Players { get; set; }
		[Field( 0x1f )] public PersistentBool? UseTriggerPlayer { get; set; }
		[Field( 0x20 )] public bool Independents { get; set; }

		public override string ToString()
		{
			if ( UseTriggerPlayer == true )
				return "<trigger player>";

			if ( !Independents )
				return ( Players ?? PlayerBitSet.None ).ToString();

			return ( Players ?? PlayerBitSet.None ).ToString() + " | Independents";
		}
	}

	[Flags]
	public enum PlayerBitSet : byte
	{
		Player1 = 1 << 0,
		Player2 = 1 << 1,
		Player3 = 1 << 2,
		Player4 = 1 << 3,
		Player5 = 1 << 4,
		Player6 = 1 << 5,
		Player7 = 1 << 6,
		Player8 = 1 << 7,

		None = 0,
		All = 255
	}
}
