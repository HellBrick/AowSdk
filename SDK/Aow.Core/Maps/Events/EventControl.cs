using Aow2.Collections;

namespace Aow2.Maps.Events
{
	[AowClass]
	public class EventControl
	{
		public EventControl() => Actions = new AowList<Event>();

		[Field( 0x1e )]
		public AowList<Event> Actions { get; private set; }

		#region Generated

		[Field( 0x1f )]
		public int u_1f;

		#endregion
	}
}
