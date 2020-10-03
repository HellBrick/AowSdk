namespace System
{
	public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e) where TEventArgs: EventArgs;
}
