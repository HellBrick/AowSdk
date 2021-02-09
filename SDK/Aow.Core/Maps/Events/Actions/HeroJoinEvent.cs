using System;
using System.Linq;
using Aow2.Collections;
using Aow2.Units;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b06 )]
	public class HeroJoinEvent : PlayerEvent
	{
		[Field( 0x32 )] public int? HeroID { get; set; }
		[Field( 0x34 )] public int? Price { get; set; }
		[Field( 0x35 )] public EventXYL Location { get; set; }

		[Field( 0x33 )] public RandomUnitIDList Companions { get; set; }

		[Field( 0x36 )] private LongPascalString message;
		public string Message
		{
			get => message;
			set => message = value;
		}

		[Field( 0x37 )] public AowList<Hero> Heroes { get; private set; }

		public override string ToString() => base.ToString() + String.Format( " [Hero join] {0}, {1} gold, {2}", ( ( Heroes.Count > 0 ) ? Heroes.First().ToString() : "<No hero>" ), Price, Location );
	}
}
