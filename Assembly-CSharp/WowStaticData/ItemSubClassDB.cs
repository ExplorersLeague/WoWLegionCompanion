using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class ItemSubClassDB : MODB<int, ItemSubClassRec>
	{
		public IEnumerable<ItemSubClassRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.ClassID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/ItemSubClass_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(ItemSubClassRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
