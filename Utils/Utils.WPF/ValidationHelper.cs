using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Utils.WPF
{
	public class ValidationHelper : IDataErrorInfo
	{
		private Dictionary<string, string> _faultedRules = new Dictionary<string, string>();
		private Dictionary<string, Func<string>> _rules = new Dictionary<string, Func<string>>();

		public void AddRule( string propertyName, Func<string> validationRule ) => _rules.Add( propertyName, validationRule );

		#region IDataErrorInfo members

		private string _firstError;
		public string Error
		{
			get => _firstError;
			set
			{
				string oldValue = _firstError;
				_firstError = value;

				if ( oldValue != value )
					RaiseErrorChanged();
			}
		}

		public event EventHandler ErrorChanged;
		private void RaiseErrorChanged()
		{
			EventHandler handler = ErrorChanged;
			if ( handler != null )
				handler( this, new EventArgs() );
		}

		public string this[string columnName]
		{
			get
			{
				string currentError = GetError( columnName );
				bool errorChanged = false;

				if ( currentError == null && _faultedRules.ContainsKey( columnName ) )
				{
					_faultedRules.Remove( columnName );
					errorChanged = true;
				}

				if ( currentError != null && !_faultedRules.ContainsKey( columnName ) )
				{
					_faultedRules.Add( columnName, currentError );
					errorChanged = true;
				}

				if ( errorChanged )
					Error = _faultedRules.Values.FirstOrDefault();

				return currentError;
			}
		}

		private string GetError( string columnName )
		{
			Func<string> rule;
			if ( _rules.TryGetValue( columnName, out rule ) )
				return rule();
			else
				return null;
		}

		#endregion
	}
}
