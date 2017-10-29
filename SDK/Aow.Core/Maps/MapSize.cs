using System;
using System.Text;

namespace Aow2.Maps
{
	public struct MapSize : IEquatable<MapSize>
	{
		public int Width { get; set; }
		public int Height { get; set; }

		public string SizeName
		{
			get
			{
				if ( Equals( MapSize.S ) )
					return "S";

				if ( Equals( MapSize.M ) )
					return "M";

				if ( Equals( MapSize.L ) )
					return "L";

				if ( Equals( MapSize.XL ) )
					return "XL";

				return "?";
			}
		}

		public static MapSize S => new MapSize() { Width = _s_width, Height = _s_height };
		private const int _s_width = 64;
		private const int _s_height = 48;

		public static MapSize M => new MapSize() { Width = _m_width, Height = _m_height };
		private const int _m_width = 96;
		private const int _m_height = 72;

		public static MapSize L => new MapSize() { Width = _l_width, Height = _l_height };
		private const int _l_width = 128;
		private const int _l_height = 96;

		public static MapSize XL => new MapSize() { Width = _xl_width, Height = _xl_height };
		private const int _xl_width = 192;
		private const int _xl_height = 144;

		#region IEquatable<MapSize> Members

		public bool Equals( MapSize other ) => ( Width == other.Width ) && ( Height == other.Height );

		public override bool Equals( object obj )
		{
			if ( obj == null )
				return false;

			if ( !( obj is MapSize ) )
				return false;

			return Equals( (MapSize) obj );
		}

		public static bool operator ==( MapSize size1, MapSize size2 ) => size1.Equals( size2 );

		public static bool operator !=( MapSize size1, MapSize size2 ) => !( size1 == size2 );

		public override int GetHashCode() => Width * 0x100 + Height;

		#endregion

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.Append( SizeName );
			result.AppendFormat( " ({0}x{1})", Width, Height );
			return result.ToString();
		}
	}
}
