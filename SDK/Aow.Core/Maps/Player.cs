using System;
using Aow2.Collections;
using Aow2.Cryptography;
using Aow2.Maps.Diplomacy;
using Aow2.Maps.Generated;
using Aow2.Maps.Statistics;
using Aow2.Units;

namespace Aow2.Maps
{
	[AowClass( ID = 0x01100114 )]
	public class Player
	{
		public Player()
		{
			Race = Race.Humans;
			Password = "";
			DiplomaticRelations = new PlayerDiplomaticRelations();
			Statistics = new PlayerStatistics();
		}

		[Field( 0x0d )] public Wizard Wizard { get; set; }

		[Field( 0x02 )] public Race Race { get; set; }
		[Field( 0x08 )] public PlayerColor Color { get; set; }

		[Field( 0x03 )] public PlayerType Type { get; set; }
		[Field( 0x04 )] public bool FixedType { get; set; }

		[Field( 0x0b )] private ShortPascalString _encryptedPassword;
		public string Password
		{
			get => PasswordCryptor.Decode( _encryptedPassword );
			set => _encryptedPassword = PasswordCryptor.Encode( value );
		}

		[Field( 0x36 )] public PlayerEmailSettings EmailSettings { get; set; }

		[Field( 0x40 )] public bool NoSpawnAnimation { get; set; }

		[Field( 0x05 )] public int Gold { get; set; }
		[Field( 0x06 )] public int ExternalGoldIncome { get; set; }
		[Field( 0x3c )] public int Mana { get; set; }
		[Field( 0x3d )] public int ExternalPowerIncome { get; set; }

		[Field( 0x13 )] public PlayerDiplomaticRelations DiplomaticRelations { get; private set; }
		[Field( 0x23 )] public PlayerStatistics Statistics { get; private set; }

		[Field( 0x100 )] public int OutpostCount { get; set; }

		public bool IsAlive => ( u_enum_0a == 1 ) && ( u_enum_19 == 0 );
		public bool IsKilled => !IsAlive;

		[Field( 0x09 )] public UnknownData u_09;
		[Field( 0x0a )] public byte u_enum_0a;

		[Field( 0x0c )] public ShortPascalString str_0c;

		[Field( 0x10 )] public int u_10;
		[Field( 0x12 )] public byte u_12;
		[Field( 0x14 )] public int u_14;
		[Field( 0x15 )] public int u_15;
		[Field( 0x16 )] public int u_16;
		[Field( 0x17 )] public TEventLogbook u_17;
		[Field( 0x18 )] public ShortPascalString str_18;
		[Field( 0x19 )] public byte u_enum_19;
		[Field( 0x1b )] public byte u_1b;
		[Field( 0x1c )] public byte u_1c;
		[Field( 0x20 )] public IntegerList u_20;
		[Field( 0x21 )] public IntegerList u_21;
		[Field( 0x22 )] public int u_22;
		[Field( 0x24 )] public int u_24;
		[Field( 0x25 )] public int u_25;
		[Field( 0x26 )] public int u_26;
		[Field( 0x27 )] public int u_27;
		[Field( 0x28 )] public LongPascalString str_28;
		[Field( 0x32 )] public int u_32;
		[Field( 0x38 )] public bool u_38;
		[Field( 0x39 )] public int u_39;
		[Field( 0x3a )] public float u_3a;
		[Field( 0x3b )] public int u_3b;

		[Field( 0x3e )] public TAIPC u_3e;
		[Field( 0x3f )] public int u_3f;

		[Field( 0x41 )] public TPlayerAISettings u_41;
		[Field( 0x42 )] public byte u_enum_42;
		[Field( 0x43 )] public int u_43;
		[Field( 0x44 )] public TFameModifierList u_44;
		[Field( 0x45 )] public byte u_enum_45;

		public override string ToString()
		{
			if ( Type == PlayerType.Independents )
				return "Independents";

			if ( Wizard != null )
				return Wizard.Name;

			return base.ToString();
		}
	}
}
