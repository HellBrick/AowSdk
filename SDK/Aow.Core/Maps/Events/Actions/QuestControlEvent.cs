using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b0a )]
	public class QuestControlEvent : Event
	{
		[Field( 0x3c )] public int QuestID { get; set; }
		[Field( 0x3d )] public QuestAction Action { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Quest control] {0} Quest_{1}", Action, QuestID );
	}

	public enum QuestAction : byte
	{
		Complete = 0,
		Fail = 1
	}
}
