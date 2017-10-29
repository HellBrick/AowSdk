using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b21 )]
	public class HeroUpgradeEvent : Event
	{
		[Field( 0x28 )] public int HeroID { get; set; }

		public override string ToString() => base.ToString() + String.Format( " [Upgrade hero] ID = {0}", HeroID );
	}
}
