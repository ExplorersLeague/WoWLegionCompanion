using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class RewardPackXCurrencyTypeDB : MODB<int, RewardPackXCurrencyTypeRec>
	{
		public IEnumerable<RewardPackXCurrencyTypeRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.RewardPackID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/RewardPackXCurrencyType.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(RewardPackXCurrencyTypeRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
