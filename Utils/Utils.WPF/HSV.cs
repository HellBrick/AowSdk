using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace Utils.WPF
{
	public class HSV: MarkupExtension
	{
		public HSV() => Alpha = 255;

		public int Hue { get; set; }
		public byte Saturation { get; set; }
		public byte Value { get; set; }
		public byte Alpha { get; set; }

		public override object ProvideValue( IServiceProvider serviceProvider )
		{
			NormalizedColor result = HsvToRgb( Hue / 60.0, Saturation / 100.0, Value / 100.0 );

			return Color.FromArgb( Alpha, (byte)( result.Red * 255.0 ), (byte)( result.Green * 255.0 ), (byte)( result.Blue * 255.0 ) );
		}

		private NormalizedColor HsvToRgb( double hue, double saturation, double value )
		{
			double max_color = value;
			double min_color = value - saturation * value;

			int hf = (int)Math.Floor( hue );
			double chroma_diff = 1 - Math.Abs( hue % 2.0 - 1.0 );
			double mid_color = min_color + chroma_diff * ( max_color - min_color );

			switch ( hf )
			{
				case 0:	//	R -> G -> B
					return new NormalizedColor()
					{
						Red = max_color,
						Green = mid_color,
						Blue = min_color
					};

				case 1:	//	G -> R -> B
					return new NormalizedColor()
					{
						Green = max_color,
						Red = mid_color,
						Blue = min_color
					};

				case 2:	//	G -> B -> R
					return new NormalizedColor()
					{
						Green = max_color,
						Blue = mid_color,
						Red = min_color
					};

				case 3:	//	B -> G -> R
					return new NormalizedColor()
					{
						Blue = max_color,
						Green = mid_color,
						Red = min_color
					};

				case 4:	//	B -> R -> G
					return new NormalizedColor()
					{
						Blue = max_color,
						Red = mid_color,
						Green = min_color
					};

				case 5:	//	R -> B -> G
					return new NormalizedColor()
					{
						Red = max_color,
						Blue = mid_color,
						Green = min_color
					};

				default:
					return new NormalizedColor()
					{
						Red = max_color,
						Blue = max_color,
						Green = max_color
					};
			}
		}

		private struct NormalizedColor
		{
			public double Red { get; set; }
			public double Green { get; set; }
			public double Blue { get; set; }
		}
	}
}
