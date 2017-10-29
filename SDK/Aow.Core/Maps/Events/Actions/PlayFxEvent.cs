using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b0e )]
	public class PlayFxEvent : Event
	{
		[Field( 0x32 )] public EventXYL Location { get; set; }
		[Field( 0x33 )] public Firework FireworkType { get; set; }
		[Field( 0x34 )] public bool CenterView { get; set; }

		[Field( 0x35 )] public int Number { get; set; }
		[Field( 0x36 )] public int Delay { get; set; }
		[Field( 0x37 )] public int NumberDelay { get; set; }
		[Field( 0x39 )] public int WaitTime { get; set; }

		[Field( 0x38 )] private FileName _file;
		public string FxFile
		{
			get => _file;
			set => _file = value;
		}

		public override string ToString() => base.ToString() + String.Format( " [FX] {0}", FxFile );
	}

	public enum Firework : byte
	{
		Cosmos = 0,
		Water,
		Fire,
		Air,
		Earth,
		Life,
		Death
	}
}
