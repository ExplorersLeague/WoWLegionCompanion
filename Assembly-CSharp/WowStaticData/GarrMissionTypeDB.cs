using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrMissionTypeDB : MODB<int, GarrMissionTypeRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrMissionType_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrMissionTypeRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
