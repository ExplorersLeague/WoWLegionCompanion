using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class ItemEffectDB : MODB<int, ItemEffectRec>
	{
		public IEnumerable<ItemEffectRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.ParentItemID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/ItemEffect.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(ItemEffectRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
