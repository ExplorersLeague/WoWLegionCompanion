using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class RewardPackXItemDB : MODB<int, RewardPackXItemRec>
	{
		public IEnumerable<RewardPackXItemRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.RewardPackID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/RewardPackXItem.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(RewardPackXItemRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
