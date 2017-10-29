using System.Linq;
using Aow2.Collections;

namespace Aow2.Modding.Units
{
	#region Enums

	public enum Direction
	{
		Up = 0,
		UpRight,
		DownRight,
		Down,
		DownLeft,
		UpLeft
	}

	public enum UnitAnimationType
	{
		StandStill = 0,
		Move,
		Attack,
		Hit,
		Dead,
		Shadow,
		StandStillEx1,
		StandStillEx2,
		Special
	}

	#endregion

	public class UnitAnimations
	{
		public UnitAnimations()
			: this( new AowList<ImageSequence>( Enumerable.Repeat( new ImageSequence(), _previewSequenceOffset + 1 ) ) )
		{
		}

		public UnitAnimations( AowList<ImageSequence> storage ) => _storage = storage;

		private const int _directionCount = 6;
		private const int _previewSequenceOffset = 54;

		private AowList<ImageSequence> _storage;

		public ImageSequence Preview
		{
			get => _storage[ _previewSequenceOffset ];
			set => _storage[ _previewSequenceOffset ] = value;
		}

		public ImageSequence this[ UnitAnimationType type, Direction direction ]
		{
			get
			{
				int offset = SequenceOffset( type, direction );
				return _storage[ offset ];
			}
			set
			{
				int offset = SequenceOffset( type, direction );
				_storage[ offset ] = value;
			}
		}

		private int SequenceOffset( UnitAnimationType type, Direction direction ) => _directionCount * ( (int) type ) + (int) direction;
	}
}
