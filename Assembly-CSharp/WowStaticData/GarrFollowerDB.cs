using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollowerDB : MODB<int, GarrFollowerRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrFollower_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrFollowerRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
