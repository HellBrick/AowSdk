using System;

namespace Aow2.Maps.Events
{
	[AowClass]
	public abstract class Event
	{
		protected Event() => Name = "";

		[Field( 0x16 )] public UnknownData Trigger { get; set; }
		[Field( 0x18 )] public ActivationMode ActivationMode { get; set; }

		[Field( 0x14 )] public UnknownData u_014;

		[Field( 0x17 )] public UnknownData u_017;
		[Field( 0x19 )] public UnknownData u_019;

		[Field( 0x1a )] public UnknownData u_enum_01a;

		public string Name
		{
			get => name;
			set => name = value;
		}

		[Field( 0x15 )] protected ShortPascalString name;

		public override string ToString() => String.Format( "{0} {1}", Name,
				ActivationMode == ActivationMode.Once ? "(x1)" : ( ActivationMode == Events.ActivationMode.OncePerPlayer ? "(x1/pp)" : "" ) );

		public string PlayerNumberToName( sbyte number )
		{
			if ( number == -1 )
				return "<trigger player>";

			return String.Format( "Player_{0}", number + 1 );
		}

		public string PlayerOrIndNumberToName( sbyte number )
		{
			if ( number == -1 )
				return "<trigger player>";

			if ( number == 0 )
				return "Independents";

			return String.Format( "Player_{0}", number );
		}
	}

	public enum ActivationMode : byte
	{
		Always = 0,
		Once = 1,
		OncePerPlayer = 2
	}
}
