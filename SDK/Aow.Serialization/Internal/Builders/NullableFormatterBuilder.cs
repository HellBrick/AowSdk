using System;
using System.IO;

namespace Aow2.Serialization.Internal.Builders
{
	class NullableFormatterBuilder : IFormatterBuilder
	{
		public bool CanCreate( FormatterRequest request )
			=> request.Type.IsValueType
			&& request.Type.IsGenericType
			&& request.Type.GetGenericTypeDefinition() == typeof( Nullable<> );

		public IFormatter Create( Type type )
			=> (IFormatter)Activator.CreateInstance( typeof( NullableFormatter<> ).MakeGenericType( type.GenericTypeArguments[ 0 ] ) );

		private class NullableFormatter<T> : Formatter<T?>
			where T : struct
		{
			private readonly IFormatter<T> _underlyingFormatter = FormatterManager.GetFormatter<T>( isPolymorph: false );

			public override bool ShouldSkipField( T? value ) => value is null;

			public override T? Deserialize( Stream inStream, long offset, long length )
				=> _underlyingFormatter.Deserialize( inStream, offset, length );

			public override void Serialize( Stream outStream, T? value )
				=> _underlyingFormatter.Serialize( outStream, value.Value );
		}
	}
}
