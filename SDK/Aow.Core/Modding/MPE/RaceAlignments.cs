using System.IO;
using Aow2.Serialization;

namespace Aow2.Modding.MPE
{
	public class RaceAlignments : ICustomFormatted
	{
		private Alignment[] _alignments =
		{
			Alignment.Neutral,
			Alignment.Neutral,
			Alignment.Neutral,
			Alignment.Neutral,
			Alignment.Good,
			Alignment.Good,
			Alignment.Good,
			Alignment.PureGood,
			Alignment.Evil,
			Alignment.Evil,
			Alignment.Evil,
			Alignment.PureEvil,
			Alignment.Neutral,
			Alignment.Good,
			Alignment.Evil
		};

		public Alignment this[ Race race ]
		{
			get
			{
				if ( race == Race.None )
					return Alignment.None;

				return _alignments[ (int) race ];
			}
			set
			{
				if ( race != Race.None )
					_alignments[ (int) race ] = value;
			}
		}

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			foreach ( Alignment value in _alignments )
				writer.Write( (byte) value );
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			for ( int i = 0; i < length; i++ )
				_alignments[ i ] = (Alignment) reader.ReadByte();
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;
	}
}
