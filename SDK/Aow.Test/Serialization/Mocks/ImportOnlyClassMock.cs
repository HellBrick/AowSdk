namespace Aow2.Test.Serialization.Mocks
{
	[AowClass]
	public class ImportOnlyClassMock
	{
		[Field( 0x0a, ImportOnly = true )]
		public int ImportOnly { get; set; }
	}
}
