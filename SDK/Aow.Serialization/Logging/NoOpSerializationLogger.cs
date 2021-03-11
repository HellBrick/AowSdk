using System.IO;

namespace Aow2.Serialization.Logging
{
	public class NoOpSerializationLogger : ISerializationLogger
	{
		public static ISerializationLogger Instance { get; } = new NoOpSerializationLogger();

		public void LogBlob( Stream stream, long offset, long length ) { }
		public void LogClassId( int classId ) { }
		public void LogFieldEnd() { }
		public void LogFieldStart( int fieldId ) { }
	}
}
