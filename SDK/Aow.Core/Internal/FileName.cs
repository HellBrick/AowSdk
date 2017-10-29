using System;

namespace Aow2
{
	[AowClass]
	public class FileName
	{
		public FileName() => Filename = "";

		[Field( 0x14 )]
		public ShortPascalString Filename { get; set; }

		public static implicit operator string( FileName file ) => file.Filename;

		public static implicit operator FileName( string file ) => new FileName { Filename = file };

		public override string ToString() => Filename.Value;
	}
}
