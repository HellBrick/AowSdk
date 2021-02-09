using System;
using Aow2;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace ModEditor.ViewModels.Mod.Abilities
{
	public class AbilityVM: NotificationObject
	{
		private const int _minLevel = 1;
		private const int _maxLevel = 99;

		#region Properties

		public Ability Model { get; set; }

		public string Text => Model.ToString();

		public int Level
		{
			get => Model.Level ?? 0;
			set { Model.Level = ClamLevel( value ); RaisePropertyChanged( () => Level ); RaisePropertyChanged( () => Text ); }
		}

		private sbyte ClamLevel( int value )
		{
			if ( value < _minLevel )
				return (sbyte) _minLevel;

			if ( value > _maxLevel )
				return (sbyte) _maxLevel;

			return (sbyte) value;
		}

		private bool _isEditing;
		public bool IsEditing
		{
			get => _isEditing;
			set { _isEditing = value; RaisePropertyChanged( () => IsEditing ); RaiseIsEditingChanged(); }
		}

		#endregion

		#region Commands

		#region Remove

		private DelegateCommand _cmdRemove;
		public DelegateCommand RemoveCommand
		{
			get
			{
				if ( _cmdRemove == null )
					_cmdRemove = new DelegateCommand( () => RaiseRemoved() );

				return _cmdRemove;
			}
		}

		#endregion

		#endregion

		#region Events

		public event EventHandler<AbilityVM, EventArgs> Removed;
		private void RaiseRemoved()
		{
			EventHandler<AbilityVM, EventArgs> handler = Removed;
			if ( handler != null )
				handler( this, new EventArgs() );
		}

		public event EventHandler<AbilityVM, EventArgs> IsEditingChanged;
		private void RaiseIsEditingChanged()
		{
			EventHandler<AbilityVM, EventArgs> handler = IsEditingChanged;
			if ( handler != null )
				handler( this, new EventArgs() );
		}

		#endregion

		public override string ToString() => Text;
	}
}
