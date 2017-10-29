using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b18 )]
	public class SetTimerEvent : Event
	{
		[Field( 0x32 )] public int TimerID { get; set; }
		[Field( 0x33 )] public int Days { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Timer] Timer_{0} = {1} days", TimerID, Days );
	}
}
