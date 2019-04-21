using System.Collections.Generic;
using System.IO;
using Aow2.Collections;
using Aow2.Cryptography;
using Aow2.Maps.Enchantments;
using Aow2.Maps.Events;
using Aow2.Maps.Generated;
using Aow2.Maps.Internal;
using Aow2.Maps.Quests;
using Aow2.Units;

namespace Aow2.Maps
{
	[AowClass]
	public class AowMap
	{
		public AowMap()
		{
			Players = new AowList<Player>();
			Events = new EventControl();
			UserCampaignSettings = new UserCampaignSettings();

			Description = "";
			Title = "";
			CryptedPassword = "";
			Authors = "";
			u_19 = "";
			u_3f = "";
			u_55 = "";

			_unitControl = new UnitControl();
		}

		public static AowMap FromFile( string filename ) => MapFormatHelper.ReadMapFromFile( filename );

		public void Save( string filename )
		{
			using ( FileStream outStream = new FileStream( filename, FileMode.Create ) )
			{
				MapFormatHelper.WriteToStream( this, outStream );
			}
		}

		public int ModID { get; set; }
		public int ClassID { get; set; }

		[Field( 0x13 )] public MapHeader PreviewHeader { get; set; }
		[Field( 0x15 )] public LongPascalString Description { get; set; }
		[Field( 0x1e )] public ShortPascalString Title { get; set; }
		[Field( 0x16 )] public IndependentAIBehavior IndependentAI { get; set; }
		[Field( 0x1c )] public bool Exploration { get; set; }    // is ignored by map open dialog
		[Field( 0x33 )] public bool AlliedVictory { get; set; }  // is ignored by map open dialog
		[Field( 0x3b )] public ShortPascalString CryptedPassword { get; set; }
		[Field( 0x4a )] public ShortPascalString Authors { get; set; }
		[Field( 0x51 )] public bool LockEditing { get; set; }
		[Field( 0x17 )] public byte CurrentPlayerNumber { get; set; }
		[Field( 0x37 )] public GameType SaveType { get; set; }

		public string Password
		{
			get => PasswordCryptor.Decode( CryptedPassword );
			set => CryptedPassword = PasswordCryptor.Encode( value );
		}

		[Field( 0x50 )]
		[Field( 0x58 )]
		public AdvancedSettings AdvancedSettings { get; set; }

		[Field( 0x59 )] public UserCampaignSettings UserCampaignSettings { get; set; }

		[Field( 0x1a )] public AowList<Player> Players { get; private set; }
		[Field( 0x1b )] public RaceList Races { get; private set; }
		[Field( 0x23 )] public EnchantmentControl Enchantments { get; set; }
		[Field( 0x2e )] public QuestControl Quests { get; set; }

		[Field( 0x44 )] private HeroControl _heroes = new HeroControl();
		public IList<Hero> Heroes => _heroes.List;

		[Field( 0x43 )] public EventControl Events { get; private set; }

		[Field( 0x4b )] public WorldMapBuilderSettings RandomSettings { get; set; }

		[Field( 0x0a )] public AowDictionary<MapLevel> levels;

		[Field( 0x28 )] private UnitControl _unitControl;  //	inner list is always empty

		[Field( 0x12 )] public IntegerList u_12;
		[Field( 0x14 )] public int u_14;

		[Field( 0x18 )] public int u_18;
		[Field( 0x19 )] public ShortPascalString u_19;

		[Field( 0x1d )] public byte u_enum_1d;

		[Field( 0x1f )] public bool u_1f;
		[Field( 0x20 )] public int u_20;
		[Field( 0x21 )] public int u_21;
		[Field( 0x22 )] public bool u_22;

		[Field( 0x24 )] public byte u_enum_24;
		[Field( 0x25 )] public byte u_enum_25;
		[Field( 0x26 )] public int u_26;
		[Field( 0x27 )] public byte u_enum_27;


		[Field( 0x29 )] public byte u_enum_29;
		[Field( 0x2a )] public TSpiritControl u_2a;
		[Field( 0x2b )] public int u_2b;
		[Field( 0x2c )] public UnknownData u_2c_TextControl;

		[Field( 0x32 )] public bool u_32;

		[Field( 0x34 )] public int u_34;
		[Field( 0x35 )] public int u_35;
		[Field( 0x36 )] public TStructureControl u_36;

		[Field( 0x38 )] public bool u_38;
		[Field( 0x3a )] public bool u_3a;

		[Field( 0x3d )] public bool u_3d;
		[Field( 0x3e )] public TItemControl u_3e;
		[Field( 0x3f )] public LongPascalString u_3f;
		[Field( 0x41 )] public UnknownData TByteList_u_41;
		[Field( 0x42 )] public TAIGroupManager u_42;

		[Field( 0x45 )] public TDiplomaticActionControl u_45;
		[Field( 0x46 )] public int u_46;
		[Field( 0x47 )] public int u_47;
		[Field( 0x48 )] public int u_48;
		[Field( 0x49 )] public bool u_49;

		[Field( 0x4c )] public IntegerList u_4c;
		[Field( 0x4d )] public bool u_4d;
		[Field( 0x4e )] public byte u_enum_4e;
		[Field( 0x4f )] public byte u_enum_4f;

		[Field( 0x52 )] public UnknownData TDoubleItemList_u_52;
		[Field( 0x53 )] public UnknownData TDoubleItemList_u_53;
		[Field( 0x54 )] public UnknownData u_54_TutorialArrowList;
		[Field( 0x55 )] public ShortPascalString u_55;
		[Field( 0x56 )] public UnknownData u_56;
		[Field( 0x57 )] public UnknownData u_57;

		[Field( 0x5a )] public TCustomFaceControl u_5a;
		[Field( 0x5b )] public int u_5b;
	}
}
