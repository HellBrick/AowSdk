using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Aow2.Serialization.Internal.Builders.Base
{
	public static class GeneratedAssembly
	{
		private static bool _isSaved = false;

		private const string _assemblyName = "Aow.Serialization.Generated";
		private const string _assemblyFileName = _assemblyName + ".dll";

		private static AssemblyBuilder _assembly;
		private static ModuleBuilder _module;

		private const MethodAttributes _staticMethodAttributes =
			MethodAttributes.Private |
			MethodAttributes.Static;

		private static Random _rng = new Random();

		static GeneratedAssembly()
		{
			_assembly = AssemblyBuilder.DefineDynamicAssembly( new AssemblyName( _assemblyName ), AssemblyBuilderAccess.RunAndSave );
			_module = _assembly.DefineDynamicModule( _assemblyName, _assemblyFileName );
		}

		public static TypeBuilder CreateType( string desiredName )
		{
			string fullName = ClassName(desiredName);
			return _module.DefineType( fullName, TypeAttributes.Class | TypeAttributes.NotPublic, typeof( object ) );
		}

		private static string ClassName( string desiredName ) => String.Format( "{0}.{1}_{2}", _assemblyName, desiredName, _rng.Next() );

		public static void Save()
		{
			if ( !_isSaved )
			{
				_isSaved = true;
				_assembly.Save( _assemblyFileName );
			}
		}
	}
}
