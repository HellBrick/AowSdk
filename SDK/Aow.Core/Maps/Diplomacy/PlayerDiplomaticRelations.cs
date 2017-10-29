using System.Collections.Generic;
using Aow2.Collections;

namespace Aow2.Maps.Diplomacy
{
	[AowClass]
	public class PlayerDiplomaticRelations
	{
		public PlayerDiplomaticRelations()
		{
			_relations = new EnumByteList<DiplomaticRelation>();
			History = new AowList<DiplomaticHistory>();
		}

		[Field( 0x14 )] private EnumByteList<DiplomaticRelation> _relations;
		public IList<DiplomaticRelation> Relations => _relations;

		[Field( 0x15 )] public AowList<DiplomaticHistory> History { get; set; }
	}
}
