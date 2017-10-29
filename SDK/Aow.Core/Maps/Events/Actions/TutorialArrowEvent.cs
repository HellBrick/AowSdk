using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b1a )]
	public class TutorialArrowEvent : Event
	{
		[Field( 0x3c )] private ShortPascalString control = "";
		public string InterfaceElement
		{
			get => control;
			set => control = value;
		}

		[Field( 0x3d )] public EventXYL MapLocation { get; set; }
		[Field( 0x3e )] public TutorialArrowAction Action { get; set; }
	}

	public enum TutorialArrowAction : byte
	{
		ShowArrow,
		ShowSelectArrow,
		Hide,
		HideAll
	}
}
