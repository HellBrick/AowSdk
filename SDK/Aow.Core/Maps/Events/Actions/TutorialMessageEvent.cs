using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b13 )]
	public class TutorialMessageEvent : PlayerEvent
	{
		public TutorialMessageEvent()
		{
			CanClose = true;
			ResetCenterPanel = true;
		}

		public string Header
		{
			get => header;
			set => header = value;
		}

		public string Message
		{
			get => message;
			set => message = value;
		}

		[Field( 0x32 )] private ShortPascalString header;
		[Field( 0x33 )] private LongPascalString message;

		[Field( 0x34 )] public int ImageIndex { get; set; }
		[Field( 0x36 )] public int TutorialID { get; set; }

		[Field( 0x35 )] public bool CanClose { get; set; }
		[Field( 0x37 )] public bool HideWindow { get; set; }
		[Field( 0x38 )] public bool ResetCenterPanel { get; set; }
	}
}
