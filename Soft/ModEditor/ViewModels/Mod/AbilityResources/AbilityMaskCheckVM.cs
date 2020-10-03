using Microsoft.Practices.Prism.ViewModel;

namespace ModEditor.ViewModels.Mod.AbilityResources
{
	public class AbilityMaskCheckVM : NotificationObject
	{
		public AbilityMaskCheckVM( AbilityResourceVM masked, AbilityResourceVM maskedBy )
		{
			Masked = masked;
			MaskedBy = maskedBy;
			IsChecked = Masked.Model.MaskingAbilities.Contains( (int) maskedBy.ID );
		}

		public AbilityResourceVM Masked { get; set; }
		public AbilityResourceVM MaskedBy { get; set; }

		private bool _isChecked;
		public bool IsChecked
		{
			get => _isChecked;
			set { _isChecked = value; OnDependencyChanged(); RaisePropertyChanged( () => IsChecked ); }
		}

		public string Name => MaskedBy.Name;

		private void OnDependencyChanged()
		{
			if ( IsChecked )
				Masked.Model.MaskingAbilities.Add( (int) MaskedBy.ID );
			else
				Masked.Model.MaskingAbilities.Remove( (int) MaskedBy.ID );

			if ( Masked.MaskedCheckList != null )
				Masked.MaskedCheckList.Refresh();
		}

		public override string ToString()
		{
			if ( IsChecked )
				return "+ " + MaskedBy.Name;
			else
				return MaskedBy.Name;
		}
	}
}
