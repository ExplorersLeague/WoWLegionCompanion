using System;
using UnityEngine;

namespace WowStaticData
{
	public class ItemDB : MODB<int, ItemRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/Item_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(ItemRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
