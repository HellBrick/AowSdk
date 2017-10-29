using System.Collections.Generic;
using Aow2.Collections;

namespace Aow2.Maps.Statistics
{
	[AowClass]
	public class PlayerStatistics
	{
		[Field( 0x14 )]
		private AowList<TurnStatistics> _turns = new AowList<TurnStatistics>();
		public IList<TurnStatistics> Turns => _turns;

		#region Generated

		[Field( 0x15 )] public int _int15;
		[Field( 0x16 )] public int _int16;

		#endregion
	}
}
