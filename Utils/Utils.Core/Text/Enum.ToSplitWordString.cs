using System;
using System.Text;

namespace Utils.Text
{
	public static class EnumFormatExtensions
	{
		public static string ToSplitWordString( this Enum value )
		{
			string original = value.ToString();
			StringBuilder result = new StringBuilder();

			result.Append( original[0] );
			for ( int i = 1; i < original.Length; ++i )
				if ( Char.IsUpper( original[i] ) && original[i - 1] != ' ' )
					result.Append( ' ' ).Append( Char.ToLower( original[i] ) );
				else
					result.Append( original[i] );

			return result.ToString();
		}
	}
}
