using System;

namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b09 )]
	public class PlayMovieEvent : Event
	{
		[Field( 0x3c )] private FileName _file;
		public string MovieFile
		{
			get => _file;
			set => _file = value;
		}

		public override string ToString() => base.ToString() + String.Format( " [Movie] {0}", MovieFile );
	}
}
