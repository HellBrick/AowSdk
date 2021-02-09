using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b0d )]
	public class DiplomaticActionEvent : Event
	{
		[Field( 0x32 )] public sbyte? FromPlayerNumber { get; set; }
		[Field( 0x33 )] public sbyte? ToPlayerNumber { get; set; }

		[Field( 0x34 )] private LongPascalString message { get; set; }
		public string Message
		{
			get => message;
			set => message = value;
		}

		[Field( 0x35 )] public DiplomaticAction? Action { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Diplomacy] {0} -> {1}: {2}", PlayerNumberToName( FromPlayerNumber ?? 0 ), PlayerNumberToName( ToPlayerNumber ?? 0 ), Action );
	}

	public enum DiplomaticAction : byte
	{
		SendMessage = 0,
		OfferPeace = 1,
		DeclareWar = 2,
		ProposeAliance = 3
	}
}
