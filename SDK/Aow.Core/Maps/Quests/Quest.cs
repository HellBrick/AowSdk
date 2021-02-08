namespace Aow2.Maps.Quests
{
	[AowClass]
	public abstract class Quest
	{
		[Field( 0x15 )] public int SpiritIndex { get; set; }
		[Field( 0x16 )] public int? ID { get; set; }
		[Field( 0x17 )] public byte PlayerIndex { get; set; }
		[Field( 0x18 )] public QuestDifficulty Diffictulty { get; set; }
		[Field( 0x19 )] public byte TimeLimit { get; set; }
		[Field( 0x1a )] public int StartDay { get; set; }
		[Field( 0x1b )] public int? CompletionDay { get; set; }
		[Field( 0x1c )] public bool IsSuccess { get; set; }
		[Field( 0x1d )] public RewardType RewardType { get; set; }

		[Field( 0x14 )] public int _int14;
	}
}
