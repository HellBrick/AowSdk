using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Text
{
	public static partial class StringBuilderExtensions
	{
		public static StringBuilder AppendCollection<T>( this StringBuilder builder, IEnumerable<T> collection, string delimiter = ", " )
		{
			string delim = String.Empty;

			foreach ( T elem in collection )
			{
				builder.Append( delim ).Append( elem );
				delim = delimiter;
			}

			return builder;
		}
	}
}
