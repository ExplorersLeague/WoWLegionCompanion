using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrMissionDB : MODB<int, GarrMissionRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrMission_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrMissionRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
