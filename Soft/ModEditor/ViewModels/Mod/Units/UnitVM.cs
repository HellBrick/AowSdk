using System;
using System.Windows.Media.Imaging;
using Aow2;
using Aow2.Modding.Units;
using Aow2.Units;
using Microsoft.Practices.Prism.ViewModel;
using ModEditor.ViewModels.Mod.Abilities;
using ModEditor.ViewModels.Mod.AbilityResources.Internal;

namespace ModEditor.ViewModels.Mod.Units
{
	public class UnitVM: NotificationObject
	{
		private const int _minStatValue = 0;
		private const int _maxStatValue = 99;
		private const int _minHP = 1;
		private const int _maxHP = 99;
		private const int _minMP = 1;
		private const int _maxMP = 99;
		private const int _minLevel = 0;
		private const int _maxLevel = 8;

		public UnitVM()
		{
			_abilities = new Lazy<AbilityListVM>( () => new AbilityListVM( Model.Abilities ) { ListProvider = AbilityListProvider } );
			_silverUpgrades = new Lazy<AbilityListVM>( () => new AbilityListVM( Model.SilverUpgrades ) { ListProvider = AbilityListProvider } );
			_goldUpgrades = new Lazy<AbilityListVM>( () => new AbilityListVM( Model.GoldUpgrades ) { ListProvider = AbilityListProvider } );
		}

		public UnitResource Model { get; set; }

		internal IUnitPreviewProvider PreviewProvider { get; set; }
		internal IAbilityListProvider AbilityListProvider { get; set; }

		#region Properties

		public string Name
		{
			get => Model.Name;
			set { Model.Name = value; RaisePropertyChanged( () => Name ); }
		}

		public string Description
		{
			get => Model.Description;
			set { Model.Description = value; RaisePropertyChanged( () => Description ); }
		}

		public Race Race
		{
			get => Model.Race;
			set { Model.Race = value; RaisePropertyChanged( () => Race ); }
		}

		public byte Level
		{
			get => Model.Level;
			set { Model.Level = LevelClam( value ); RaisePropertyChanged( () => Level ); }
		}

		public int Price
		{
			get => Model.Price;
			set { Model.Price = value; RaisePropertyChanged( () => Price ); }
		}

		public Alignment Alignment
		{
			get => Model.Alignment;
			set { Model.Alignment = value; RaisePropertyChanged( () => Alignment ); }
		}

		public UnitType Type
		{
			get => Model.Type;
			set { Model.Type = value; RaisePropertyChanged( () => Type ); }
		}

		public UnitGender Gender
		{
			get => Model.Gender;
			set { Model.Gender = value; RaisePropertyChanged( () => Gender ); }
		}

		public UnitSize Size
		{
			get => Model.Size;
			set { Model.Size = value; RaisePropertyChanged( () => Size ); }
		}

		public int Attack
		{
			get => Model.Attack;
			set { Model.Attack = StatClam( value ); RaisePropertyChanged( () => Attack ); }
		}

		public int Damage
		{
			get => Model.Damage;
			set { Model.Damage = StatClam( value ); RaisePropertyChanged( () => Damage ); }
		}

		public int Defense
		{
			get => Model.Defense;
			set { Model.Defense = StatClam( value ); RaisePropertyChanged( () => Defense ); }
		}

		public int Resistance
		{
			get => Model.Resistance;
			set { Model.Resistance = StatClam( value ); RaisePropertyChanged( () => Resistance ); }
		}

		public int HP
		{
			get => Model.HP;
			set { Model.HP = HpClam( value ); RaisePropertyChanged( () => HP ); }
		}

		public int MP
		{
			get => Model.MP;
			set { Model.MP = HpClam( value ); RaisePropertyChanged( () => MP ); }
		}

		public bool IsGarrisonDisabled
		{
			get => Model.IsGarrisonDisabled;
			set { Model.IsGarrisonDisabled = value; RaisePropertyChanged( () => IsGarrisonDisabled ); }
		}

		private Lazy<AbilityListVM> _abilities;
		public AbilityListVM Abilities => _abilities.Value;

		private Lazy<AbilityListVM> _silverUpgrades;
		public AbilityListVM SilverUpgrades => _silverUpgrades.Value;

		private Lazy<AbilityListVM> _goldUpgrades;
		public AbilityListVM GoldUpgrades => _goldUpgrades.Value;

		//private BitmapSource _preview;
		//public BitmapSource Preview
		//{
		//	get { return _preview; }
		//	set { _preview = value; RaisePropertyChanged( () => Preview ); }
		//}

		public BitmapSource Preview => PreviewProvider[ Model.ModelIndex ];

		#endregion

		#region Private

		private static byte StatClam( int statValue )
		{
			if ( statValue < _minStatValue )
				return (byte) _minStatValue;

			if ( statValue > _maxStatValue )
				return (byte) _maxStatValue;

			return (byte) statValue;
		}

		private static byte HpClam( int value )
		{
			if ( value < _minHP )
				return (byte) _minHP;

			if ( value > _maxHP )
				return (byte) _maxHP;

			return (byte) value;
		}

		private static byte LevelClam( int value )
		{
			if ( value < _minLevel )
				return (byte) _minLevel;

			if ( value > _maxLevel )
				return (byte) _maxLevel;

			return (byte) value;
		}

		#endregion

		public override string ToString() => Name;
	}
}
