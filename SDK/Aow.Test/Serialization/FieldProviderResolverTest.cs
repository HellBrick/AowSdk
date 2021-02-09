using System;
using System.Collections.Generic;
using System.Linq;
using Aow2.Serialization.Internal.Builders.OffsetMap;
using Aow2.Serialization.Internal.Builders.OffsetMap.FieldProviders;
using Aow2.Test.Serialization.Mocks;
using NUnit.Framework;

namespace Aow2.Test.Serialization
{
	[TestFixture]
	public class FieldProviderResolverTest
	{
		[Test]
		public void Class()
		{
			List<Type> expectedProviderTypes = new List<Type>() { typeof( ClassFieldProvider ) };

			IList<IFieldProvider> providers = FieldProviderResolver.Instance.GetProviders( typeof( PlainClassMock ) );
			List<Type> providerTypes = providers
				.Select( p => p.GetType() )
				.ToList();				

			CollectionAssert.AreEqual( expectedProviderTypes, providerTypes );
		}

		[Test]
		public void List()
		{
			List<Type> expectedProviderTypes = new List<Type>() { typeof( ListFieldProvider ) };

			IList<IFieldProvider> providers = FieldProviderResolver.Instance.GetProviders( typeof( ListMock ) );
			List<Type> providerTypes = providers
				.Select( p => p.GetType() )
				.ToList();

			CollectionAssert.AreEqual( expectedProviderTypes, providerTypes );
		}

		[Test]
		public void Dictionary()
		{
			List<Type> expectedProviderTypes = new List<Type>() { typeof( DictionaryFieldProvider ) };

			IList<IFieldProvider> providers = FieldProviderResolver.Instance.GetProviders( typeof( DictionaryMock ) );
			List<Type> providerTypes = providers
				.Select( p => p.GetType() )
				.ToList();

			CollectionAssert.AreEqual( expectedProviderTypes, providerTypes );
		}

		[Test]
		public void ClassList()
		{
			List<Type> expectedProviderTypes = new List<Type>() { typeof( ClassFieldProvider ), typeof( ListFieldProvider ) };

			IList<IFieldProvider> providers = FieldProviderResolver.Instance.GetProviders( typeof( ListClassMock ) );
			List<Type> providerTypes = providers
				.Select( p => p.GetType() )
				.ToList();

			CollectionAssert.AreEqual( expectedProviderTypes, providerTypes );
		}

		[Test]
		public void ClassDictionary()
		{
			List<Type> expectedProviderTypes = new List<Type>() { typeof( ClassFieldProvider ), typeof( DictionaryFieldProvider ) };

			IList<IFieldProvider> providers = FieldProviderResolver.Instance.GetProviders( typeof( DictionaryClassMock ) );
			List<Type> providerTypes = providers
				.Select( p => p.GetType() )
				.ToList();

			CollectionAssert.AreEqual( expectedProviderTypes, providerTypes );
		}
	}
}
