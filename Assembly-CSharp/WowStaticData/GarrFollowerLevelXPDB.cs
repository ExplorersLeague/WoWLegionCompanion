using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollowerLevelXPDB : MODB<int, GarrFollowerLevelXPRec>
	{
		public IEnumerable<GarrFollowerLevelXPRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.FollowerLevel == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrFollowerLevelXP.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrFollowerLevelXPRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
