using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollItemSetMemberDB : MODB<int, GarrFollItemSetMemberRec>
	{
		public IEnumerable<GarrFollItemSetMemberRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.GarrFollItemSetID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrFollItemSetMember.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrFollItemSetMemberRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
