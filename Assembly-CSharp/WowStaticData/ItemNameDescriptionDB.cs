using System;
using UnityEngine;

namespace WowStaticData
{
	public class ItemNameDescriptionDB : MODB<int, ItemNameDescriptionRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/ItemNameDescription_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(ItemNameDescriptionRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
