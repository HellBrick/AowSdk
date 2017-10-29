using System;
using System.IO;

namespace Aow2.Serialization.Internal
{
	public interface IFormatter
	{
		void Serialize( Stream outStream, object value );
		object Deserialize( Stream inStream, long offset, long length );
	}

	public interface IFormatter<T>
	{
		void Serialize( Stream outStream, T value );
		T Deserialize( Stream inStream, long offset, long length );
	}

	public abstract class Formatter<T> : IFormatter, IFormatter<T>
	{
		public abstract void Serialize( Stream outStream, T value );
		public abstract T Deserialize( Stream inStream, long offset, long length );
		public virtual bool ShouldSkipField( T value ) => false;

		void IFormatter.Serialize( Stream outStream, object value ) => Serialize( outStream, (T) value );
		object IFormatter.Deserialize( Stream inStream, long offset, long length ) => Deserialize( inStream, offset, length );
	}

	public interface IEditableFormatter
	{
		Delegate SerializationDelegate { set; }
		Delegate DeserializationDelegate { set; }
		Delegate ShouldSkipFieldDelegate { set; }
	}

	public class EditableFormatter<T> : Formatter<T>, IEditableFormatter
	{
		public Action<Stream, T> SerializationMethod { get; set; }
		public Delegate SerializationDelegate
		{
			get => SerializationMethod;
			set => SerializationMethod = value as Action<Stream, T>;
		}

		public Func<Stream, long, long, T> DeserializationMethod { get; set; }
		public Delegate DeserializationDelegate
		{
			get => DeserializationMethod;
			set => DeserializationMethod = value as Func<Stream, long, long, T>;
		}

		public Func<T, bool> ShouldSkipFieldMethod { get; set; }
		public Delegate ShouldSkipFieldDelegate
		{
			get => ShouldSkipFieldMethod;
			set => ShouldSkipFieldMethod = value as Func<T, bool>;
		}

		public override void Serialize( Stream outStream, T value ) => SerializationMethod( outStream, value );

		public override T Deserialize( Stream inStream, long offset, long length ) => DeserializationMethod( inStream, offset, length );

		public override bool ShouldSkipField( T value ) => ShouldSkipFieldMethod( value );
	}
}
