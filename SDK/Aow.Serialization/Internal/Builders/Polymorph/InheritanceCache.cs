using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Aow2.Serialization.Internal.Builders.Polymorph
{
	class InheritanceCache
	{
		private HashSet<string> _scannedAssemblies = new HashSet<string>();
		private ConcurrentDictionary<Type, InheritanceNode> _cache = new ConcurrentDictionary<Type, InheritanceNode>();
		private ConcurrentDictionary<Type, InheritanceNode> _disconnectedNodes = new ConcurrentDictionary<Type, InheritanceNode>();

		private string _aowClassAssemblyName = typeof( AowClassAttribute ).Assembly.FullName;
		private object _lock = new object();

		public static InheritanceCache Instance { get; private set; }

		static InheritanceCache() => Instance = new InheritanceCache();

		private InheritanceCache() { }

		public InheritanceNode GetOrCreateNode( Type type )
		{
			if ( !_cache.TryGetValue( type, out InheritanceNode node ) )
				Rescan();

			if ( !_cache.TryGetValue( type, out node ) )
				throw new InvalidOperationException( String.Format( "Can't find aow class {0} in loaded assemblies. The most likely cause is it misses [AowClass] attribute with a proper ClassID.", type ) );

			return node;
		}

		public string Print()
		{
			StringBuilder builder = new StringBuilder();
			foreach ( InheritanceNode node in _cache.Values.Where( n => n.BaseClass == null ) )
				Print( node, builder, 0 );

			return builder.ToString();
		}

		private void Print( InheritanceNode node, StringBuilder builder, int depth )
		{
			for ( int i = 0; i < depth; i++ )
				builder.Append( "    " );

			builder.AppendLine( node.ToString() );
			foreach ( InheritanceNode subnode in node.DerivedClasses )
				Print( subnode, builder, depth + 1 );
		}

		private void Rescan()
		{
			FillDisconnectedNodes();
			ConnectNodes();
		}

		private void FillDisconnectedNodes()
		{
			List<Assembly> newAssemblies = AppDomain.CurrentDomain.GetAssemblies()
				.Where( a => !a.IsDynamic )
				.Where( a => !_scannedAssemblies.Contains( a.FullName ) )
				.ToList();

			IEnumerable<Assembly> referencingCore = newAssemblies
						 .Where( a => a.GetReferencedAssemblies().Any( an => an.FullName == _aowClassAssemblyName ) );

			foreach ( Assembly assembly in referencingCore )
			{
				foreach ( TypeInfo type in assembly.DefinedTypes )
				{
					AowClassAttribute aowClass = type.GetCustomAttribute<AowClassAttribute>();
					if ( aowClass != null )
					{
						InheritanceNode disconnectedNode = new InheritanceNode( type, aowClass );
						_disconnectedNodes.TryAdd( disconnectedNode.Type, disconnectedNode );
					}
				}
			}

			foreach ( Assembly assembly in newAssemblies )
				_scannedAssemblies.Add( assembly.FullName );
		}

		private void ConnectNodes()
		{
			lock ( _lock )
			{
				while ( _disconnectedNodes.Count > 0 )
				{
					InheritanceNode node = TakeDisconnectedNode();
					if ( node == null )
						break;

					ConnectNode( node );
				}
			}
		}

		private InheritanceNode TakeDisconnectedNode()
		{
			Type key = _disconnectedNodes.Keys.FirstOrDefault();
			if ( key == null )
				return null;

			_disconnectedNodes.TryRemove( key, out InheritanceNode node );
			return node;
		}

		private void ConnectNode( InheritanceNode node )
		{
			foreach ( InheritanceNode anotherNode in _cache.Values.Concat( _disconnectedNodes.Values ) )
			{
				if ( IsParent( anotherNode, node ) )
					anotherNode.ConnectSubclass( node );

				if ( IsParent( node, anotherNode ) )
					node.ConnectSubclass( anotherNode );
			}

			_cache.TryAdd( node.Type, node );
		}

		private bool IsParent( InheritanceNode parent, InheritanceNode child ) => child.BaseClass == null &&
				(
					child.Type.BaseType == parent.Type ||
					IsGenericParent( parent, child )
				);

		private bool IsGenericParent( InheritanceNode parent, InheritanceNode child ) => parent.Type.IsGenericTypeDefinition &&
				child.Type.BaseType.IsGenericType &&
				child.Type.BaseType.GetGenericTypeDefinition() == parent.Type;
	}
}
