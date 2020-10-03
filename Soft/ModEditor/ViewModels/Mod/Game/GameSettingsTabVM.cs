using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aow2.Modding.MPE;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Utils.WPF;
using Utils.Text;

namespace ModEditor.ViewModels.Mod.Game
{
	class GameSettingsTabVM: NotificationObject, IModTab, IDataErrorInfo
	{
		private ValidationHelper _validation = new ValidationHelper();

		public GameSettingsTabVM() => _validation.AddRule( _modIdTextProperty, () =>
													{
														int temp;
														if ( Int32.TryParse( ModIDText, System.Globalization.NumberStyles.HexNumber, null, out temp ) )
															return null;
														else
															return "Mod ID has invalid format";
													} );

		private ModVM _mod;
		public ModVM Mod
		{
			get => _mod;
			set { _mod = value; NewName = CurrentName; }
		}

		public MpeSettings Model { get; set; }

		#region Properties

		public int ModID
		{
			get => Mod.Model.Data.ID;
			set { Mod.Model.Data.ID = value; RaisePropertyChanged( () => ModID ); RaisePropertyChanged( () => ModIDText ); }
		}

		private const string _modIdTextProperty = "ModIDText";
		public string ModIDText
		{
			get => ModID.ToString( "x8" );
			set => ModID = Int32.Parse( value, System.Globalization.NumberStyles.HexNumber );
		}

		public string CurrentName
		{
			get => Mod.Model.Info.Name;
			set => Mod.Model.Copy( value );
		}

		private string _newName;
		public string NewName
		{
			get => _newName;
			set
			{
				StringBuilder builder = new StringBuilder( value.Length );
				builder.AppendCollection( value.Where( c => !Path.GetInvalidFileNameChars().Contains( c ) ), "" );
				_newName = builder.ToString();

				RaisePropertyChanged( () => NewName );
				CopyCommand.RaiseCanExecuteChanged();
			}
		}

		#endregion

		#region Commands

		#region GenerateID

		private DelegateCommand _cmdGenerateID;
		public DelegateCommand GenerateIDCommand
		{
			get
			{
				if ( _cmdGenerateID == null )
					_cmdGenerateID = new DelegateCommand( () => GenerateID() );

				return _cmdGenerateID;
			}
		}

		private static Random _rng = new Random();

		private void GenerateID() => ModID = _rng.Next();

		#endregion

		#region Copy

		private DelegateCommand _cmdCopy;
		public DelegateCommand CopyCommand
		{
			get
			{
				if ( _cmdCopy == null )
					_cmdCopy = new DelegateCommand(
						() => Copy(),
						() => CanCopy() );

				return _cmdCopy;
			}
		}

		private void Copy()
		{
			Mod.Copy( NewName );
			RaisePropertyChanged( () => CurrentName );
			CopyCommand.RaiseCanExecuteChanged();
		}

		private bool CanCopy() => !String.IsNullOrEmpty( NewName ) &&
				!Directory.Exists( Path.Combine( Aow2.Environment.Instance.ModsFolderPath, NewName ) ) &&
				NewName != CurrentName;

		#endregion

		#region ResetName

		private DelegateCommand _cmdResetName;
		public DelegateCommand ResetNameCommand
		{
			get
			{
				if ( _cmdResetName == null )
					_cmdResetName = new DelegateCommand( () => ResetName() );

				return _cmdResetName;
			}
		}

		private void ResetName() => NewName = CurrentName;

		#endregion

		#endregion

		#region IModTab Members

		public string Header => "Settings";

		public string IconResourceKey => "SettingsTabIcon";

		public bool IsLoaded { get; set; }

		public async Task LoadAsync() => await Task.Factory.StartNew( () => Load() );

		private void Load()
		{
			Model = Mod.Model.Data.Mpe;
			IsLoaded = true;
		}

		#endregion

		#region IDataErrorInfo Members

		public string Error => _validation.Error;

		public string this[ string columnName ] => _validation[ columnName ];

		#endregion
	}
}
