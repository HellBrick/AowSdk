namespace ModEditor.ViewModels
{
	interface ICloseable
	{
		bool OnClosing();
	}

	interface IShowable
	{
		void OnShow();
	}
}
