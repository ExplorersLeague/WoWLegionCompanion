using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrEncounterDB : MODB<int, GarrEncounterRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/GarrEncounter_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(GarrEncounterRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
