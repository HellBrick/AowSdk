using Aow2.Maps.Internal;

namespace Aow2.Maps
{
	[AowClass]
	public abstract class MapHeaderBase
	{
	}

	[AowClass( ID = 0x011001ed )]
	public class MapHeader : MapHeaderBase
	{
		public MapHeader()
		{
		}

		public static MapHeader FromFile( string filename )
		{
			using ( MapFormatHelper helper = MapFormatHelper.FromFile( filename ) )
			{
				return helper.DeserializeHeader();
			}
		}

		[Field( 0x1e )] public byte PlayersCount { get; set; }
		[Field( 0x21 )] public int Day { get; set; }
		[Field( 0x22 )] public GameType GameType { get; set; }

		public MapLayerID LastLayer
		{
			get => (MapLayerID) ( _lastLayerStorage - 1 );
			protected set => _lastLayerStorage = (int) value + 1;
		}

		[Field( 0x16 )] private int _lastLayerStorage;

		[Field( 0x14 )] public int Width { get; set; }
		[Field( 0x15 )] public int Height { get; set; }

		public MapSize Size => new MapSize() { Width = Width, Height = Height };

		[Field( 0x1f )] private LongPascalString _description = "";
		public string Description
		{
			get => _description;
			set => _description = value;
		}

		[Field( 0x20 )] private ShortPascalString _title = "";
		public string Title
		{
			get => _title;
			set => _title = value;
		}

		[Field( 0x23 )] public bool u_23;
	}
}
