using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b1b )]
	public class ControlInterfaceEvent : Event
	{
		[Field( 0x3c )] public ControlInterfaceAction Action { get; set; }

		[Field( 0x3d )] private ShortPascalString control = "";
		public string InterfaceElement
		{
			get => control;
			set => control = value;
		}

		[Field( 0x3e )] public EventXYL MapPosition { get; set; }
		[Field( 0x3f )] public bool LockGameOptions { get; set; }
	}

	public enum ControlInterfaceAction : byte
	{
		Lock,
		Unlock,
		Unselect
	}
}
