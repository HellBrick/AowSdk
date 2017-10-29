using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b1c )]
	public class ControlRaceRelationEvent : PlayerEvent
	{
		[Field( 0x3c )] public sbyte Modifier { get; set; }
		[Field( 0x3d )] public EventPlayerList Players { get; set; }
		[Field( 0x3e )] public Race Race { get; set; }

		[Field( 0x3f )] private ShortPascalString reason;
		public string Reason
		{
			get => reason;
			set => reason = value;
		}

		public override string ToString() => base.ToString() + String.Format( " [Race relation] {0}: {1} ({2})", Race, Modifier, Players );
	}
}
