using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollowerTypeDB : MODB<int, GarrFollowerTypeRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrFollowerType.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrFollowerTypeRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
