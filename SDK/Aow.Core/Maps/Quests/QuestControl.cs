using Aow2.Collections;
namespace Aow2.Maps.Quests
{
	[AowClass]
	public class QuestControl
	{
		public QuestControl() => List = new AowList<Quest>();

		[Field( 0x1e )] public AowList<Quest> List { get; private set; }

		#region Generated		
		[Field( 0x1f )] public int? u_1f;
		#endregion
	}
}
