using Aow2.Collections;
using Aow2.Items;
using Aow2.Modding.Generated;
using Aow2.Units;

namespace Aow2.Modding.Abilities
{
	[AowClass]
	public class AbilityResource
	{
		public AbilityResource()
		{
			UpgradeCost = 5;
			MaskingAbilities = new IntegerList();
			UsageTypesInternal = new UsageTypesAdapter();
		}

		public string Name { get; set; }
		public AbilityID ID { get; set; }

		[Field( 0x0f )] public LongPascalString _description;
		public string Description
		{
			get => _description;
			set => _description = value;
		}

		[Field( 0x10 )] public int UpgradeCost { get; set; }
		[Field( 0x17 )] public int ForgeCost { get; set; }

		[Field( 0x16 )] public Races Races { get; set; }

		[Field( 0x18 )] public IntegerList MaskingAbilities { get; protected set; }

		[Field( 0x13 )] internal UsageTypesAdapter UsageTypesInternal { get; set; }

		public ItemTypes ItemTypes
		{
			get => UsageTypesInternal.ItemTypes;
			set => UsageTypesInternal.ItemTypes = value;
		}

		public HeroClasses HeroClasses
		{
			get => UsageTypesInternal.HeroClasses;
			set => UsageTypesInternal.HeroClasses = value;
		}

		public bool IsForgeable
		{
			get => UsageTypesInternal.IsForgeable;
			set => UsageTypesInternal.IsForgeable = value;
		}

		public override string ToString() => Name;

		#region Generated
		[Field( 0x11 )] public AowList<SfxSequence> u_11_sounds_effects;
		[Field( 0x12 )] public UnknownData u_12_TImageSequenceList;
		[Field( 0x15 )] public TEFilename u_15;
		#endregion
	}
}
