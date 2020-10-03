using Aow2;
using Aow2.Modding;
using Aow2.Modding.MPE;
using Microsoft.Practices.Prism.ViewModel;

namespace ModEditor.ViewModels.Mod.Races
{
	public class RaceVM : NotificationObject
	{
		public Race Race { get; set; }
		public MpeSettings MpeSettings { get; set; }
		public RaceResource RaceSettings { get; set; }

		public TerrainTypes FriendlyTerrains
		{
			get => MpeSettings.RaceTerrainMorals[ Race ].Friendly;
			set { MpeSettings.RaceTerrainMorals[Race].Friendly = value; RaisePropertyChanged( () => FriendlyTerrains ); }
		}

		public TerrainTypes HostileTerrains
		{
			get => MpeSettings.RaceTerrainMorals[ Race ].Hostile;
			set { MpeSettings.RaceTerrainMorals[Race].Hostile = value; RaisePropertyChanged( () => HostileTerrains ); }
		}

		public TerrainTypes CropTerrains
		{
			get => MpeSettings.RaceCropsTerrains[ Race ];
			set { MpeSettings.RaceCropsTerrains[Race] = value; RaisePropertyChanged( () => CropTerrains ); }
		}

		public Alignment Alignment
		{
			get => MpeSettings.RaceAlignments[ Race ];
			set { MpeSettings.RaceAlignments[Race] = value; RaisePropertyChanged( () => Alignment ); }
		}
	}
}
