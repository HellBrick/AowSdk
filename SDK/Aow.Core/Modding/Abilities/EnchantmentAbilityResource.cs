namespace Aow2.Modding.Abilities
{
	[AowClass]
	public class EnchantmentAbilityResource : AbilityResource
	{
		[Field( 0x33 )]
		[Field( 0x1f, ImportOnly = true )]
		public sbyte AttackBonus { get; set; }


		[Field( 0x35 )]
		[Field( 0x21, ImportOnly = true )]
		public sbyte DamageBonus { get; set; }

		[Field( 0x34 )]
		[Field( 0x1e, ImportOnly = true )]
		public sbyte DefenseBonus { get; set; }

		[Field( 0x36 )]
		[Field( 0x20, ImportOnly = true )]
		public sbyte ResistanceBonus { get; set; }
	}
}
