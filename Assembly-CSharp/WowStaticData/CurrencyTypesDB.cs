using System;
using UnityEngine;

namespace WowStaticData
{
	public class CurrencyTypesDB : MODB<int, CurrencyTypesRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/CurrencyTypes_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(CurrencyTypesRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
