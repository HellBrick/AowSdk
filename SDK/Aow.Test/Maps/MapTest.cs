﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Aow.Test.Maps.Resources;
using Aow2.Maps;
using Aow2.Maps.Internal;
using Aow2.Serialization;
using Aow2.Serialization.Logging;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NUnit.Framework;

namespace Aow2.Test.Maps
{
	[TestFixture]
	public class MapTest
	{
		[Test]
		[TestCaseSource( nameof( EnumerateMapCases ), new object[] { "Structure" } )]
		public void SaveRoundTrips( byte[] saveBytes )
		{
			AowMap original = AowMap.FromBytes( saveBytes );
			AowMap roundTripped = AowMap.FromBytes( original.ToBytes() );

			roundTripped.Should().BeEquivalentTo( original );
		}

		[Test]
		[TestCaseSource( nameof( EnumerateMapCases ), new object[] { "Binary" } )]
		public void SaveDataSectionRoundTrips( byte[] saveBytes )
		{
			MemoryStream compressedSaveStream = new MemoryStream( saveBytes );
			byte[] originalDataBytes = MapFormatHelper.ReadPreHeaderAndDecompressDataStream( compressedSaveStream ).dataStream.ToArray();

			AowMap deserializedMap = AowMap.FromBytes( saveBytes );
			MemoryStream roundTrippedDataStream = new MemoryStream();
			AowSerializer<AowMap> mapSerializer = new AowSerializer<AowMap>( hasRootWrapper: true );
			mapSerializer.Serialize( roundTrippedDataStream, deserializedMap );
			byte[] roundTrippedDataBytes = roundTrippedDataStream.ToArray();

			try
			{
				roundTrippedDataBytes.Should().BeEquivalentTo( originalDataBytes );
			}
			catch
			{
				MapStructureLogger originalLogger = new MapStructureLogger();
				_ = mapSerializer.Deserialize( new MemoryStream( originalDataBytes ), originalLogger );

				MapStructureLogger roundTrippedLogger = new MapStructureLogger();
				_ = mapSerializer.Deserialize( new MemoryStream( roundTrippedDataBytes ), roundTrippedLogger );

				roundTrippedLogger.RootNode.Should().BeEquivalentTo
				(
					originalLogger.RootNode,
					cfg
						=> cfg
						.WithStrictOrdering()
						.Excluding((IMemberInfo m) => m.SelectedMemberInfo.Name == nameof(MapStructureLogger.Node.Parent))
						.AllowingInfiniteRecursion()
				);
				throw;
			}
		}

		private static IEnumerable<TestCaseData> EnumerateMapCases(string kind)
			=> MapFiles
			.ResourceManager
			.GetResourceSet( CultureInfo.CurrentUICulture, createIfNotExists: true, tryParents: true )
			.Cast<System.Collections.DictionaryEntry>()
			.Select( e => new TestCaseData( (byte[]) e.Value ).SetName( $"{e.Key}.{kind}" ) );

		private class MapStructureLogger : ISerializationLogger
		{
			private Node _currentNode;
			public Node RootNode { get; }

			public MapStructureLogger()
			{
				RootNode = new Node( parent: null, "<Root>" );
				_currentNode = RootNode;
			}

			public void LogBlob( Stream stream, long offset, long length )
			{
				byte[] buffer = new byte[ (int) length ];
				stream.Position = offset;
				stream.Read( buffer, 0, (int) length );
				stream.Position = offset;

				_currentNode.Data = buffer;
			}

			public void LogClassId( int classId ) => _currentNode.Header += $"/[{classId:x8}]";

			public void LogFieldStart( int fieldId )
			{
				Node fieldNode = new Node( _currentNode, $"{_currentNode.Header}/{fieldId:x2}" );
				_currentNode.Children.Add( fieldNode );
				_currentNode = fieldNode;
			}

			public void LogFieldEnd() => _currentNode = _currentNode.Parent;

			public class Node
			{
				public Node( Node parent, string header )
				{
					Parent = parent;
					Header = header;
				}

				public Node Parent { get; }
				public string Header { get; set; }

				public List<Node> Children { get; } = new List<Node>();
				public byte[] Data { get; set; }

				public override string ToString() => Header;
			}
		}
	}
}
