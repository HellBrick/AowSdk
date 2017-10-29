using System;
using System.Collections.Generic;
using System.Text;

namespace Aow2.Serialization
{
	/// <summary>
	/// The exception that is thrown when class contains a field that can not be processed by the serializer.
	/// </summary>
	[Serializable]
	public class FieldAnalysisException : Exception
	{
		private FieldAnalysisException() => FieldIDs = new List<int>();

		public FieldAnalysisException( Type type, int fieldID ) : this()
		{
			Type = type;
			FieldIDs.Add( fieldID );
		}

		public FieldAnalysisException( Type type, IEnumerable<int> fieldIDs )
			: this()
		{
			Type = type;
			FieldIDs = new List<int>( fieldIDs );
		}

		public Type Type { get; set; }
		public List<int> FieldIDs { get; private set; }

		public override string Message => String.Format( "Serializer cannot analyze field{2} {0} of class {1}.", FieldList(), Type.Name, FieldIDs.Count > 1 ? "s" : "" );

		private string FieldList()
		{
			StringBuilder result = new StringBuilder();
			if ( FieldIDs.Count > 0 )
			{
				result.Append( FieldIDs[ 0 ].ToString( "x2" ) );
				for ( int i = 1; i < FieldIDs.Count; i++ )
					result.AppendFormat( ", {0}", FieldIDs[ i ].ToString( "x2" ) );
			}
			return result.ToString();
		}

		protected FieldAnalysisException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context )
			: base( info, context ) { }
	}

	/// <summary>
	/// The exception that is thrown when serializer can't create binary routines for a type.
	/// </summary>
	[Serializable]
	public class SerializationTypeBinaryAnalysisException : Exception
	{
		public SerializationTypeBinaryAnalysisException( Type type ) => Type = type;

		public Type Type { get; set; }

		public override string Message => String.Format( "Serializer doesn't know how to create binary representation of type {0}.", Type.Name );

		protected SerializationTypeBinaryAnalysisException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context )
			: base( info, context ) { }
	}

	/// <summary>
	/// The exception that is thrown when serializer can't resolve class ID.
	/// </summary>
	[Serializable]
	public class SerializationClassIDException : Exception
	{
		public SerializationClassIDException( Type type ) => Type = type;

		public Type Type { get; set; }

		public override string Message => String.Format( "Serializer can't find class ID for type {0}.", Type.Name );

		protected SerializationClassIDException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context )
			: base( info, context ) { }
	}

	/// <summary>
	/// The exception that is thrown when serializer can't find class for specified class ID.
	/// </summary>
	[Serializable]
	public class UnknownClassIDException : Exception
	{
		public UnknownClassIDException( int classID ) => ClassID = classID;

		public int ClassID { get; set; }

		public override string Message => String.Format( "Serializer have encountered class with unrecognized class ID = 0x{0:x8}.", ClassID.ToString( "x2" ) );

		protected UnknownClassIDException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context )
			: base( info, context ) { }
	}

	/// <summary>
	/// The exception that is thrown when type discovered by class ID is not derived from required base class.
	/// </summary>
	[Serializable]
	public class InheritanceViolationException : Exception
	{
		public InheritanceViolationException( Type requiredType, Type discoveredType, int classID )
		{
			ClassID = classID;
			DiscoveredType = discoveredType;
			RequiredType = requiredType;
		}

		public int ClassID { get; set; }
		public Type RequiredType { get; set; }
		public Type DiscoveredType { get; set; }

		public override string Message => String.Format( "Serializer has found inheritance violation: {0} with ID = 0x {1:x8} must be derived from {2}.", DiscoveredType.Name, ClassID, RequiredType.Name );

		protected InheritanceViolationException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context )
			: base( info, context ) { }
	}

	/// <summary>
	/// Is thrown when a root wrapper is expected but something else is read from the stream.
	/// </summary>
	[Serializable]
	public class InvalidRootWrapperException : Exception
	{
		public InvalidRootWrapperException()
		{
		}

		public override string Message => "Root wrapper was expected, but something else has been read from the stream.";

		protected InvalidRootWrapperException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context )
			: base( info, context ) { }
	}
}
