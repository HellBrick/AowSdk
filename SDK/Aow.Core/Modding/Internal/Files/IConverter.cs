namespace Aow2.Modding.Internal.Files
{
	internal interface IConverter<TLegacy, TNew>
	{
		TNew Convert( TLegacy legacyData );
	}
}
