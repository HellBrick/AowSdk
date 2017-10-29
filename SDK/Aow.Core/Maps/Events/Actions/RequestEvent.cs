namespace Aow2.Maps.Events.Actions
{
	[AowClass( ID = 0x01100b19 )]
	public class RequestEvent : ShowMessageEvent
	{
		[Field( 0x3c )] public int RequestID { get; set; }
	}
}
