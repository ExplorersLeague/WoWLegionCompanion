using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollowerQualityDB : MODB<int, GarrFollowerQualityRec>
	{
		public IEnumerable<GarrFollowerQualityRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.Quality == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrFollowerQuality.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrFollowerQualityRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
