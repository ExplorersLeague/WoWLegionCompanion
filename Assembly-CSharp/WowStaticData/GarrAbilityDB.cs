using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrAbilityDB : MODB<int, GarrAbilityRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrAbility_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrAbilityRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
