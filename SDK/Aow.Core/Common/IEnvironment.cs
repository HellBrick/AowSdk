namespace Aow2
{
	public interface IEnvironment
	{
		string ImagesFolder { get; }

		string ModsFolderPath { get; }
		string ActiveModName { get; set; }
		string ActiveModFullPath { get; }
	}
}
