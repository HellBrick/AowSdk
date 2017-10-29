using System;

namespace Aow2.Modding
{
	[AowClass( ID = 0x00031003 )]
	public class SfxSettings
	{
		public SfxSettings() => Volume = 100;

		public string FileName
		{
			get => file_name;
			set => file_name = value;
		}
		[Field( 0x0a )] private LongPascalString file_name = "";

		[Field( 0x0b )] public int Delay { get; set; }
		[Field( 0x0c )] public int Volume { get; set; }
		[Field( 0x0d )] public int Frequency { get; set; }
	}
}
