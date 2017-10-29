using System;
using System.Collections.Generic;
using Aow2.Items;
using Aow2.Modding.Abilities;
using Aow2.Modding.Internal.Files;
using Aow2.Modding.MPE;
using Aow2.Modding.Skills;
using Aow2.Modding.Skills.Legacy;
using Aow2.Modding.Spells;
using Aow2.Modding.Units;

namespace Aow2.Modding
{
	public class ModData : IDisposable, IModData
	{
		private ModData() => FillFileList();

		internal ModData( AowMod mod )
			: this()
		{
			string folder = mod.Info.FullPath;
			LinkToNewFolder( folder );
		}

		private const string _fileMain = "Resource.ahr";
		private const string _fileUnits = "Unitres.pfs";
		private const string _fileUnitModels = "UnitGfx.pfs";
		private const string _fileAbilities = "Ability.pfs";
		private const string _fileItems = "Items.pfs";
		private const string _fileSpells = "Spells.pfs";
		private const string _fileProduction = "Productions.pfs";
		private const string _fileRaces = "RaceRes.pfs";
		private const string _fileSkillsLegacy = "WizardSkills.pfs";
		private const string _fileSkills = "WizardSkills2.pfs";
		private const string _fileMpe = "MPE.arc";

		private AhrFile<Animations> _main = new AhrFile<Animations>( _fileMain );
		private PfsFile<UnitResourceList> _units = new PfsFile<UnitResourceList>( _fileUnits );
		private PfsFile<UnitModelList> _unitModels = new PfsFile<UnitModelList>( _fileUnitModels );
		private PfsFile<AbilityResourceList> _abilities = new PfsFile<AbilityResourceList>( _fileAbilities );
		private PfsFile<ItemLibrary> _items = new PfsFile<ItemLibrary>( _fileItems );
		private PfsFile<RaceResourceList> _races = new PfsFile<RaceResourceList>( _fileRaces );
		private PfsFile<SpellResourceList> _spells = new PfsFile<SpellResourceList>( _fileSpells );
		private PfsFile<MpeSettings> _mpe = new PfsFile<MpeSettings>( _fileMpe );

		private AutoConvertedModFile<SkillResourceList, SkillResourceListLegacy> _skills = new AutoConvertedModFile<SkillResourceList, SkillResourceListLegacy>(
			_fileSkills,
			_fileSkillsLegacy,
			new PfsStorageStrategy<SkillResourceList>(),
			new PfsStorageStrategy<SkillResourceListLegacy>(),
			new SkillFormatConverter() );

		private List<IModFile> _files = new List<IModFile>();

		private void FillFileList()
		{
			_files.Add( _main );
			_files.Add( _units );
			_files.Add( _unitModels );
			_files.Add( _abilities );
			_files.Add( _items );
			_files.Add( _races );
			_files.Add( _skills );
			_files.Add( _spells );
			_files.Add( _mpe );
		}

		internal void LinkToNewFolder( string newFolderPath )
		{
			foreach ( IModFile file in _files )
				file.Folder = newFolderPath;
		}

		public Animations Main
		{
			get => _main.Data;
			set => _main.Data = value;
		}

		public int ID
		{
			get => _main.Data.ModID;
			set => _main.Data.ModID = value;
		}

		public MpeSettings Mpe
		{
			get => _mpe.Data;
			set => _mpe.Data = value;
		}

		public UnitResourceList Units
		{
			get => _units.Data;
			set => _units.Data = value;
		}

		public UnitModelList UnitModels
		{
			get => _unitModels.Data;
			set => _unitModels.Data = value;
		}

		public AbilityResourceList Abilities
		{
			get => _abilities.Data;
			set => _abilities.Data = value;
		}

		public ItemLibrary Items
		{
			get => _items.Data;
			set => _items.Data = value;
		}

		public RaceResourceList Races
		{
			get => _races.Data;
			set => _races.Data = value;
		}

		public SkillResourceList Skills
		{
			get => _skills.Data;
			set => _skills.Data = value;
		}

		public SpellResourceList Spells
		{
			get => _spells.Data;
			set => _spells.Data = value;
		}

		internal void Save()
		{
			foreach ( IModFile file in _files )
			{
				if ( file.IsParsed )
					file.Save();
			}
		}

		public void Dispose()
		{
			foreach ( IModFile file in _files )
				file.Dispose();
		}
	}
}
