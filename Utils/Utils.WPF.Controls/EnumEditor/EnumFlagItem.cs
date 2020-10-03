using System;
using System.ComponentModel;

namespace Utils.WPF.Controls
{
	public class EnumFlagItem: INotifyPropertyChanged
	{
		public EnumFlagItem( Enum value )
		{
			_value = value;
			_isChecked = false;
		}

		#region IEnumFlagItem members

		public const string IsCheckedProperty = "IsChecked";
		private bool _isChecked;
		public bool IsChecked
		{
			get => _isChecked;
			set { _isChecked = value; RaisePropertyChanged( IsCheckedProperty ); }
		}

		private Enum _value;
		public Enum Value
		{
			get => _value;
			set { _value = value; RaisePropertyChanged( "Value" ); }
		}

		#endregion

		#region INotifyPropertyChanged members

		public event PropertyChangedEventHandler PropertyChanged;
		private void RaisePropertyChanged( string propertyName )
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if ( handler != null )
				handler( this, new PropertyChangedEventArgs( propertyName ) );
		}

		#endregion
	}
}
