﻿using System.IO;

namespace Aow2.Serialization.Logging
{
	public interface ISerializationLogger
	{
		void LogFieldStart( int fieldId );
		void LogFieldEnd();
		void LogClassId( int classId );
		void LogBlob( Stream stream, long offset, long length );
	}
}
