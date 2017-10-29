using System;

namespace Aow2.Maps
{
	[AowClass]
	public class UserCampaignSettings
	{
		[Field( 0x14 )] public bool IsUserCampaign { get; set; }
		[Field( 0x1a )] public bool IsFirstMap { get; set; }
		[Field( 0x1b )] public bool IsAlliedVictoryEnabled;

		[Field( 0x17 )]
		private LongPascalString _introductionText = String.Empty;
		public string IntroductionText
		{
			get => _introductionText;
			set => _introductionText = value;
		}

		[Field( 0x18 )]
		private LongPascalString _victoryText = String.Empty;
		public string VictoryText
		{
			get => _victoryText;
			set => _victoryText = value;
		}

		[Field( 0x19 )]
		private ShortPascalString _nextMap = String.Empty;
		public string NextMap
		{
			get => _nextMap;
			set => _nextMap = value;
		}

		[Field( 0x15 )] public UnknownData TImageLibrary_u_15;
	}
}
