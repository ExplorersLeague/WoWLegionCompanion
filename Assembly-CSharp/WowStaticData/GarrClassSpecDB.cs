using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrClassSpecDB : MODB<int, GarrClassSpecRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrClassSpec_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrClassSpecRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
