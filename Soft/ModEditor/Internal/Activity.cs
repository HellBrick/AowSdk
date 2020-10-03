using Microsoft.Practices.Prism.ViewModel;

namespace Utils.WPF.Models
{
	class Activity: NotificationObject
	{
		public Activity() => _content = this;

		public Activity( object content ) => _content = content;

		private object _content;
		public object Content
		{
			get => _content;
			set { _content = value; RaisePropertyChanged( () => Content ); }
		}

		private string _name;
		public string ActivityName
		{
			get => _name;
			set { _name = value; RaisePropertyChanged( () => ActivityName ); }
		}

		private int _max;
		public int MaxProgress
		{
			get => _max;
			set { _max = value; RaisePropertyChanged( () => MaxProgress ); }
		}

		private int _value;
		public int CurrentProgress
		{
			get => _value;
			set { _value = value; RaisePropertyChanged( () => CurrentProgress ); }
		}

		private bool _isUncertain = true;
		public bool IsUnknownProgress
		{
			get => _isUncertain;
			set { _isUncertain = value; RaisePropertyChanged( () => IsUnknownProgress ); }
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get => _isBusy;
			set { _isBusy = value; RaisePropertyChanged( () => IsBusy ); }
		}

		public void StartActivity( string activityName, int maxProgress )
		{
			ActivityName = activityName;
			IsUnknownProgress = false;
			MaxProgress = maxProgress;
			CurrentProgress = 0;
			IsBusy = true;
		}
	}
}
