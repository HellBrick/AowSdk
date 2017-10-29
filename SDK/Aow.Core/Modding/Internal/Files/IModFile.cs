using System;

namespace Aow2.Modding.Internal.Files
{
	internal interface IModFile : IDisposable
	{
		bool IsParsed { get; }
		string Folder { get; set; }
		string FileName { get; }

		void Save();
	}

	internal interface IModFile<T> : IModFile
	{
		T Data { get; set; }
	}
}
