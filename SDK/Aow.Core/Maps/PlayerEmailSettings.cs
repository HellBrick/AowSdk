namespace Aow2.Maps
{
	[AowClass]
	public class PlayerEmailSettings
	{
		[Field( 0x15 )] private EmailGame _settings = new EmailGame();

		public string GameTitle
		{
			get => _settings.GameTitle;
			set => _settings.GameTitle = value;
		}

		public string Email
		{
			get => _settings.Email;
			set => _settings.Email = value;
		}

		[Field( 0x14 )] public byte U_Enum_14 { get; set; }

		public int? U_15
		{
			get => _settings.u_15;
			set => _settings.u_15 = value;
		}

		public int? U_16
		{
			get => _settings.u_16;
			set => _settings.u_16 = value;
		}

		public int? U_18
		{
			get => _settings.u_18;
			set => _settings.u_18 = value;
		}

		[AowClass]
		private class EmailGame
		{
			public EmailGame()
			{
				GameTitle = "";
				Email = "";
			}

			[Field( 0x14 )] public ShortPascalString GameTitle { get; set; }
			[Field( 0x17 )] public ShortPascalString Email { get; set; }

			[Field( 0x15 )] public int? u_15;
			[Field( 0x16 )] public int? u_16;
			[Field( 0x18 )] public int? u_18;
		}
	}
}
