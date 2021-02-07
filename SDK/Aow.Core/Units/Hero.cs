namespace Aow2.Units
{
	[AowClass( ID = 0x01100128 )]
	public class Hero: AbstractHero
	{
		[Field( 0x49 )] private LongPascalString _description;
		public string Description
		{
			get => _description;
			set => _description = value;
		}

		[Field( 0x46 )] public byte? _byte46;
		[Field( 0x48 )] public int? _int48;
	}
}
