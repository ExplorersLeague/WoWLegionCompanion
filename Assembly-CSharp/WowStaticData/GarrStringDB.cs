using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrStringDB : MODB<int, GarrStringRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrString_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrStringRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
