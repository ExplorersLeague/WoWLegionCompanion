using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrMechanicTypeDB : MODB<int, GarrMechanicTypeRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrMechanicType_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrMechanicTypeRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
