using System;
using System.IO;
using Aow2.Items;
using Aow2.Serialization;
using Aow2.Units;

namespace Aow2.Modding.Abilities
{
	internal class UsageTypesAdapter : ICustomFormatted
	{
		#region Properties

		public ItemTypes ItemTypes { get; set; }
		public HeroClasses HeroClasses { get; set; }
		public bool IsForgeable { get; set; }

		#endregion

		#region ICustomFormatted Members

		public void Serialize( Stream outStream )
		{
			AbilityUsageTypes fullEnum = CombineFlags();
			BinaryWriter writer = new BinaryWriter( outStream );
			writer.Write( (int) fullEnum );
		}

		public void Deserialize( Stream inStream, long length )
		{
			AbilityUsageTypes fullEnum = (AbilityUsageTypes) new BinaryReader( inStream ).ReadInt32();
			ParseFlags( fullEnum );
		}

		public bool ShouldBeOmitted() => false;

		#endregion

		#region Private

		#region Masks and offsets

		private const AbilityUsageTypes _itemTypesMask =
			AbilityUsageTypes.HeadItem | AbilityUsageTypes.TorsoItem | AbilityUsageTypes.AttackItem |
			AbilityUsageTypes.DefenseItem | AbilityUsageTypes.RingItem | AbilityUsageTypes.UseItem;

		private const int _itemTypesOffset = ( (int) AbilityUsageTypes.HeadItem ) / ( (int) ItemTypes.Helmet );

		private const AbilityUsageTypes _heroClassesMask =
			AbilityUsageTypes.HeroWarrior | AbilityUsageTypes.HeroPaladin | AbilityUsageTypes.HeroPriest |
			AbilityUsageTypes.HeroRanger | AbilityUsageTypes.HeroRogue | AbilityUsageTypes.HeroShaman;

		private const int _heroClassesOffset = ( (int) AbilityUsageTypes.HeroWarrior ) / ( (int) HeroClasses.Warrior );

		#endregion

		private AbilityUsageTypes CombineFlags()
		{
			int fullEnumValue =
				( (int) ItemTypes * _itemTypesOffset ) |
				( (int) HeroClasses * _heroClassesOffset ) |
				( IsForgeable ? (int) AbilityUsageTypes.ItemForge : 0 );

			return (AbilityUsageTypes) fullEnumValue;
		}

		private void ParseFlags( AbilityUsageTypes fullEnum )
		{
			ItemTypes = (ItemTypes) ( (int) ( fullEnum & _itemTypesMask ) / _itemTypesOffset );
			HeroClasses = (HeroClasses) ( (int) ( fullEnum & _heroClassesMask ) / _heroClassesOffset );
			IsForgeable = ( fullEnum & AbilityUsageTypes.ItemForge ) == AbilityUsageTypes.ItemForge;
		}

		#endregion
	}
}
