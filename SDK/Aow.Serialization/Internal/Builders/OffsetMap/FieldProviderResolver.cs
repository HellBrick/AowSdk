using System;
using System.Collections.Generic;
using System.Linq;
using Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders;

namespace Aow2.Serialization.Internal.Builders.OffsetMap
{
	internal interface IFieldProviderResolver
	{
		bool IsSupported( Type type );
		IList<IFieldProvider> GetProviders( Type type );
	}

	internal class FieldProviderResolver: IFieldProviderResolver
	{
		private static Lazy<IFieldProviderResolver> _instance;
		public static IFieldProviderResolver Instance
		{
			get => _instance.Value;
			set => _instance = new Lazy<IFieldProviderResolver>( () => value );
		}

		private FieldProviderResolver()
		{
		}

		static FieldProviderResolver() => _instance = new Lazy<IFieldProviderResolver>( () => new FieldProviderResolver() );

		private List<IFieldProviderFactory> _factories = new List<IFieldProviderFactory>()
		{
			ClassFieldProvider.Factory,
			ListFieldProvider.Factory,
			DictionaryFieldProvider.Factory
		};

		public bool IsSupported( Type type ) => _factories.Any( f => f.IsSupported( type ) );

		public IList<IFieldProvider> GetProviders( Type type ) => _factories
				.Where( f => f.IsSupported( type ) )
				.Select( f => f.Create( type ) )
				.ToList();
	}
}
