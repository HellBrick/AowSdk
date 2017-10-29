using System.Linq;
using System.Text;

namespace Utils.Text
{
	public static class IntExtensions
	{
		public static string ToRomanString( this int number )
		{
			string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
			int[] normal = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
			StringBuilder result = new StringBuilder();
			for ( int i = 0; i < roman.Count(); i++ )
			{
				while ( number >= normal[i] )
				{
					number -= normal[i];
					result.Append( roman[i] );
				}
			}
			return result.ToString();
		}

		public static string ToRomanString( this byte number ) => ( (int) number ).ToRomanString();
	}
}
