using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b08 )]
	public class QuestEvent : PlayerEvent
	{
		public QuestEvent()
		{
			Message = "";
			QuestName = "";
			MissionDescription = "";
			PopupHeader = "";
			Popup = true;
		}

		[Field( 0x3c )] public EventSpirit Spirit { get; set; }
		[Field( 0x3e )] public int QuestID { get; set; }

		[Field( 0x40 )] public bool AutoReward { get; set; }
		[Field( 0x41 )] public EventXYL Location { get; set; }

		[Field( 0x43 )] public QuestDifficulty Difficulty { get; set; }
		[Field( 0x44 )] public int Days { get; set; }
		[Field( 0x45 )] public bool Popup { get; set; }

		[Field( 0x3d )] private LongPascalString _message;
		[Field( 0x3f )] private ShortPascalString _questName;
		[Field( 0x42 )] private LongPascalString _missionDescription;
		[Field( 0x46 )] private ShortPascalString _popupHeader;

		public string Message
		{
			get => _message;
			set => _message = value;
		}

		public string QuestName
		{
			get => _questName;
			set => _questName = value;
		}

		public string MissionDescription
		{
			get => _missionDescription;
			set => _missionDescription = value;
		}

		public string PopupHeader
		{
			get => _popupHeader;
			set => _popupHeader = value;
		}

		public override string ToString() => base.ToString() + String.Format( " [New quest] {0}", QuestName );
	}

	public enum EventSpirit : byte
	{
		None = 0,
		Order,
		War,
		Nature,
		Magic
	}

	public enum QuestDifficulty : byte
	{
		None = 0,
		Easy,
		Average,
		Hard
	}
}
