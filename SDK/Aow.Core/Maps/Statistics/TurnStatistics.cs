namespace Aow2.Maps.Statistics
{
	[AowClass( ID = 0x1100018 )]
	public class TurnStatistics
	{
		[Field( 0x1d )] public byte? Heroes { get; set; }

		[Field( 0x19 )] public int? _int19;
		[Field( 0x1a )] public int? _int1a;
		[Field( 0x1b )] public int? _int1b;
		[Field( 0x1c )] public int? _int1c;

		[Field( 0x1e )] public int? _int1e;
	}
}
