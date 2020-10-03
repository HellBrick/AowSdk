using Microsoft.Practices.Prism.ViewModel;

namespace ModEditor.ViewModels.Mod.Spells
{
	public class SpellCheckVM: NotificationObject
	{
		public SpellCheckVM( SpellVM owner, SpellVM spell )
		{
			Owner = owner;
			Spell = spell;
			IsChecked = Owner.RequiredIDs.Contains( Spell.ID );
		}

		public SpellVM Owner { get; set; }
		public SpellVM Spell { get; set; }

		private bool _isChecked;
		public bool IsChecked
		{
			get => _isChecked;
			set { _isChecked = value; OnDependencyChanged(); RaisePropertyChanged( () => IsChecked ); }
		}

		public string Name => Spell.Name;

		private void OnDependencyChanged()
		{
			if ( IsChecked )
				Owner.RequiredIDs.Add( Spell.ID );
			else
				Owner.RequiredIDs.Remove( Spell.ID );
		}

		public override string ToString()
		{
			if ( IsChecked )
				return "+ " + Spell.Name;
			else
				return Spell.Name;
		}
	}
}
