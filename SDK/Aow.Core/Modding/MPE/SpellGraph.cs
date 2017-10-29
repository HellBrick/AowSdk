using System.Collections.Generic;
using System.IO;
using Aow2.Serialization;

namespace Aow2.Modding.MPE
{
	public class SpellGraph : ICustomFormatted
	{
		private Dictionary<int, HashSet<int>> _graph = new Dictionary<int, HashSet<int>>();

		public HashSet<int> this[ int spellID ]
		{
			get
			{
				if ( !_graph.ContainsKey( spellID ) )
					_graph.Add( spellID, new HashSet<int>() );

				return _graph[ spellID ];
			}
		}

		public void AddDependency( int spellID, int requiredID )
		{
			if ( !_graph.ContainsKey( spellID ) )
				_graph.Add( spellID, new HashSet<int>() );

			if ( !_graph[ spellID ].Contains( requiredID ) )
				_graph[ spellID ].Add( requiredID );
		}

		public void RemoveDependency( int spellID, int requiredID )
		{
			if ( _graph.ContainsKey( spellID ) && _graph[ spellID ].Contains( requiredID ) )
			{
				_graph[ spellID ].Remove( requiredID );
			}
		}

		void ICustomFormatted.Serialize( Stream outStream )
		{
			BinaryWriter writer = new BinaryWriter( outStream );
			foreach ( KeyValuePair<int, HashSet<int>> pair in _graph )
			{
				if ( pair.Value.Count > 0 )
				{
					writer.Write( pair.Key );
					writer.Write( pair.Value.Count );

					foreach ( int spell in pair.Value )
						writer.Write( spell );
				}
			}
		}

		void ICustomFormatted.Deserialize( Stream inStream, long length )
		{
			BinaryReader reader = new BinaryReader( inStream );
			long stopPosition = inStream.Position + length;

			while ( inStream.Position < stopPosition )
			{
				int spell = reader.ReadInt32();
				int dependencyCount = reader.ReadInt32();
				HashSet<int> dependencies = new HashSet<int>();

				for ( int i = 0; i < dependencyCount; i++ )
					dependencies.Add( reader.ReadInt32() );

				_graph.Add( spell, dependencies );
			}
		}

		bool ICustomFormatted.ShouldBeOmitted() => false;
	}
}
