using System;
using System.Collections.Generic;
using System.Text;

namespace Aow2.Serialization.Internal.Builders.Polymorph
{
	class InheritanceNode
	{
		public InheritanceNode()
		{
			DerivedClasses = new List<InheritanceNode>();
			DerivedClassLookup = new Dictionary<int, InheritanceNode>();
		}

		public InheritanceNode( Type type, AowClassAttribute aowClass )
			: this()
		{
			Type = type;
			if ( aowClass.ID != AowClassAttribute.AbstractID )
				ClassID = aowClass.ID;
		}

		public Type Type { get; set; }
		public int? ClassID { get; set; }

		public InheritanceNode BaseClass { get; set; }
		public List<InheritanceNode> DerivedClasses { get; private set; }
		public Dictionary<int, InheritanceNode> DerivedClassLookup { get; private set; }

		public InheritanceNode FindSubclass( int subclassID )
		{
			InheritanceNode node = TryFindSubclass( subclassID );
			if ( node == null )
				throw new InvalidOperationException( String.Format( "Type {0} doesn't have a derived class with ID = 0x{1:x8}", Type.Name, subclassID ) );

			return node;
		}

		private InheritanceNode TryFindSubclass( int subclassID )
		{
			if ( ClassID == subclassID )
				return this;

			if ( DerivedClassLookup.TryGetValue( subclassID, out InheritanceNode node ) )
				return node;

			foreach ( InheritanceNode subnode in DerivedClasses )
			{
				node = subnode.TryFindSubclass( subclassID );
				if ( node != null )
					return node;
			}

			return null;
		}

		public void ConnectSubclass( InheritanceNode node )
		{
			node.BaseClass = this;
			DerivedClasses.Add( node );

			if ( node.ClassID.HasValue )
				DerivedClassLookup.Add( node.ClassID.Value, node );
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append( Type.Name );

			if ( ClassID.HasValue )
				builder.Append( " [" ).Append( ClassID.Value.ToString( "x8" ) ).Append( ']' );

			if ( DerivedClasses.Count > 0 )
				builder.Append( ", " ).Append( DerivedClasses.Count ).Append( " children" );

			return builder.ToString();
		}
	}
}
