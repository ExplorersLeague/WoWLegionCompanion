using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrAbilityCategoryDB : MODB<int, GarrAbilityCategoryRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrAbilityCategory_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrAbilityCategoryRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
