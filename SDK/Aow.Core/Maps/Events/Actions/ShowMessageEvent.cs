using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b00 )]
	public class ShowMessageEvent : MultiplePlayersEvent
	{
		public ShowMessageEvent()
		{
			Message = "";
			Header = "";
		}

		[Field( 0x32 )] protected ShortPascalString header;
		[Field( 0x33 )] protected LongPascalString message;

		public string Message
		{
			get => message;
			set => message = value;
		}

		public string Header
		{
			get => header;
			set => header = value;
		}

		[Field( 0x34 )] public int? ImageIndex { get; set; }
		[Field( 0x35 )] public EventSpirit? Spirit { get; set; }
	}
}
