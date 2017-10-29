using System;
using System.Collections.Generic;
using System.IO;

namespace Aow2.Serialization.Internal.Builders
{
	class PrimitiveTypeFormatterBuilder : IFormatterBuilder
	{
		private Dictionary<Type, IFormatter> _formatters = new Dictionary<Type, IFormatter>()
		{
			{ typeof( long ), new Int64Formatter() },
			{ typeof( int ), new Int32Formatter() },
			{ typeof( short ), new Int16Formatter() },
			{ typeof( ulong ), new UInt64Formatter() },
			{ typeof( uint ), new UInt32Formatter() },
			{ typeof( ushort ), new UInt16Formatter() },
			{ typeof( byte ), new ByteFormatter() },
			{ typeof( sbyte ), new SByteFormatter() },
			{ typeof( float ), new SingleFormatter() },
			{ typeof( double ), new DoubleFormatter() },
		};

		private class Int64Formatter : Formatter<long>
		{
			public override void Serialize( Stream outStream, long value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( long ) );
			}

			public override long Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( long ) ];
				inStream.Read( buffer, 0, sizeof( long ) );
				return BitConverter.ToInt64( buffer, 0 );
			}
		}

		private class Int32Formatter : Formatter<int>
		{
			public override void Serialize( Stream outStream, int value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( int ) );
			}

			public override int Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( int ) ];
				inStream.Read( buffer, 0, sizeof( int ) );
				return BitConverter.ToInt32( buffer, 0 );
			}
		}

		private class Int16Formatter : Formatter<short>
		{
			public override void Serialize( Stream outStream, short value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( short ) );
			}

			public override short Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( short ) ];
				inStream.Read( buffer, 0, sizeof( short ) );
				return BitConverter.ToInt16( buffer, 0 );
			}
		}

		private class UInt64Formatter : Formatter<ulong>
		{
			public override void Serialize( Stream outStream, ulong value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( ulong ) );
			}

			public override ulong Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( ulong ) ];
				inStream.Read( buffer, 0, sizeof( ulong ) );
				return BitConverter.ToUInt64( buffer, 0 );
			}
		}

		private class UInt32Formatter : Formatter<uint>
		{
			public override void Serialize( Stream outStream, uint value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( uint ) );
			}

			public override uint Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( uint ) ];
				inStream.Read( buffer, 0, sizeof( uint ) );
				return BitConverter.ToUInt32( buffer, 0 );
			}
		}

		private class UInt16Formatter : Formatter<ushort>
		{
			public override void Serialize( Stream outStream, ushort value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( ushort ) );
			}

			public override ushort Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( ushort ) ];
				inStream.Read( buffer, 0, sizeof( ushort ) );
				return BitConverter.ToUInt16( buffer, 0 );
			}
		}

		private class ByteFormatter : Formatter<byte>
		{
			public override void Serialize( Stream outStream, byte value ) => outStream.WriteByte( value );

			public override byte Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				return (byte) inStream.ReadByte();
			}
		}

		private class SByteFormatter : Formatter<sbyte>
		{
			public override void Serialize( Stream outStream, sbyte value )
			{
				unchecked
				{
					outStream.WriteByte( (byte) value );
				}
			}

			public override sbyte Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				return (sbyte) inStream.ReadByte();
			}
		}

		private class SingleFormatter : Formatter<float>
		{
			public override void Serialize( Stream outStream, float value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( float ) );
			}

			public override float Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( float ) ];
				inStream.Read( buffer, 0, sizeof( float ) );
				return BitConverter.ToSingle( buffer, 0 );
			}
		}

		private class DoubleFormatter : Formatter<double>
		{
			public override void Serialize( Stream outStream, double value )
			{
				byte[] buffer = BitConverter.GetBytes( value );
				outStream.Write( buffer, 0, sizeof( double ) );
			}

			public override double Deserialize( Stream inStream, long offset, long length )
			{
				inStream.Position = offset;
				byte[] buffer = new byte[ sizeof( double ) ];
				inStream.Read( buffer, 0, sizeof( double ) );
				return BitConverter.ToDouble( buffer, 0 );
			}
		}

		public bool CanCreate( FormatterRequest request ) => _formatters.ContainsKey( request.Type );

		public IFormatter Create( Type type ) => _formatters[ type ];
	}
}
