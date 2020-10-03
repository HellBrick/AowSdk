namespace Utils.Math
{
	static class MathHelper
	{
		public static int AlignUp( int value, int multiplier ) => ( ( value + multiplier - 1 ) / multiplier ) * multiplier;
	}
}
