using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrTalentDB : MODB<int, GarrTalentRec>
	{
		public IEnumerable<GarrTalentRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (ulong)rec.GarrTalentTreeID == (ulong)((long)parentID)
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrTalent_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrTalentRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
