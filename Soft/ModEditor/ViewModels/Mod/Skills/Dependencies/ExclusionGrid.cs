using System;
using System.Collections.Generic;
using System.Linq;

namespace ModEditor.ViewModels.Mod.Skills.Dependencies
{
	class ExclusionGrid
	{
		private Dictionary<DoubleKey, DoubleCheck> _grid;

		public ExclusionGrid( IEnumerable<SkillVM> skills )
		{
			DoubleKeyComparer comparer = new DoubleKeyComparer( skills.Min( s => s.ID ), skills.Max( s => s.ID ) );
			_grid = new Dictionary<DoubleKey, DoubleCheck>( comparer );

			foreach ( SkillVM s1 in skills )
			{
				foreach ( SkillVM s2 in skills )
				{
					if ( s1.ID != s2.ID )
					{
						DoubleKey key = new DoubleKey( s1.ID, s2.ID );
						if ( !_grid.ContainsKey( key ) )
						{
							bool defaultChecked = GetDefaultChecked( s1, s2 );
							DoubleCheck value = new DoubleCheck()
							{
								Skill1 = s1,
								Skill2 = s2,
								IsChecked = defaultChecked
							};
							_grid.Add( key, value );
						}
					}
				}
			}
		}

		public List<SkillCkeckVM> GetChecks( SkillVM owner )
		{
			List<SkillCkeckVM> list = new List<SkillCkeckVM>();

			foreach ( KeyValuePair<DoubleKey, DoubleCheck> record in _grid )
			{
				if ( record.Key.Key1 == owner.ID || record.Key.Key2 == owner.ID )
				{
					SkillCkeckVM newCheck = new SkillCkeckVM( record.Value )
					{
						Owner = owner,
						Skill = record.Value.Skill1 == owner ? record.Value.Skill2 : record.Value.Skill1
					};
					list.Add( newCheck );
				}
			}

			return list;
		}

		private bool GetDefaultChecked( SkillVM s1, SkillVM s2 ) => s1.Model.ExcludedSkillIDs.Contains( s2.ID ) || s2.Model.ExcludedSkillIDs.Contains( s1.ID );

		private class DoubleCheck: IExclusionCheck
		{
			public SkillVM Skill1 { get; set; }
			public SkillVM Skill2 { get; set; }

			#region IExclusionCheck Members

			private bool _isChecked;
			public bool IsChecked
			{
				get => _isChecked;
				set => SetChecked( value, null );
			}

			public void SetChecked( bool value, SkillVM senderCheckerOwner )
			{
				_isChecked = value;
				if ( _isChecked )
					Skill1.Model.MutualExclude( Skill2.Model );
				else
					Skill2.Model.MutualUnexclude( Skill1.Model );

				if ( IsCheckedChanged != null )
					IsCheckedChanged( senderCheckerOwner, new EventArgs() );
			}

			public event EventHandler<SkillVM, EventArgs> IsCheckedChanged;

			#endregion
		}

		private struct DoubleKey
		{
			public DoubleKey( int key1, int key2 )
				: this()
			{
				Key1 = key1;
				Key2 = key2;
			}

			public int Key1 { get; private set; }
			public int Key2 { get; private set; }
		}

		private class DoubleKeyComparer: IEqualityComparer<DoubleKey>
		{
			private int _offset;
			private int _dimension;

			public DoubleKeyComparer( int minID, int maxID )
			{
				_offset = 1 - minID;
				_dimension = maxID - minID;
			}

			#region IEqualityComparer<Tuple<SkillVM>> Members

			public bool Equals( DoubleKey x, DoubleKey y ) => x.Key1 == y.Key1 && x.Key2 == y.Key2 ||
					x.Key1 == y.Key2 && x.Key2 == y.Key1;

			public int GetHashCode( DoubleKey obj )
			{
				int id1 = _offset + obj.Key1;
				int id2 = _offset + obj.Key2;

				if ( id1 < id2 )
					return id1 * _dimension + id2;
				else
					return id2 * _dimension + id1;
			}

			#endregion
		}
	}
}
