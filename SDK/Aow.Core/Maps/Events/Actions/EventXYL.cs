using System;
using System.IO;
using Aow2.Serialization;

namespace Aow2.Maps.Events.Actions
{
	[AowClass]
	public class EventXYL
	{
		[Field( 0x1e )] CoordinateWrapper xyl = new CoordinateWrapper();

		public int X
		{
			get => xyl.X;
			set => xyl.X = value;
		}

		public int Y
		{
			get => xyl.Y;
			set => xyl.Y = value;
		}

		public int L
		{
			get => xyl.L;
			set => xyl.L = value;
		}

		private class CoordinateWrapper : ICustomFormatted
		{
			public int X { get; set; }
			public int Y { get; set; }
			public int L { get; set; }

			#region ICustomFormatted Members

			void ICustomFormatted.Serialize( Stream outStream )
			{
				BinaryWriter output = new BinaryWriter( outStream );

				output.Write( X );
				output.Write( Y );
				output.Write( L );
				output.Write( (int) 0 );
			}

			void ICustomFormatted.Deserialize( Stream inStream, long length )
			{
				BinaryReader input = new BinaryReader( inStream );
				X = input.ReadInt32();
				Y = input.ReadInt32();
				L = input.ReadInt32();
				input.ReadInt32();   //	4th zero dword
			}

			bool ICustomFormatted.ShouldBeOmitted() => false;

			#endregion
		}

		public override string ToString() => String.Format( "X={0} Y={1} L={2}", X, Y, L );
	}
}
