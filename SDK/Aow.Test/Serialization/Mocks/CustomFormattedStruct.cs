using Aow2.Serialization;

namespace Aow2.Test.Serialization.Mocks
{
	struct CustomFormattedStruct : ICustomFormatted
	{
		#region ICustomFormatted Members

		public void Serialize( System.IO.Stream outStream )
		{
		}

		public void Deserialize( System.IO.Stream inStream, long length )
		{
		}

		public bool ShouldBeOmitted() => false;

		#endregion
	}
}
