using System;
using UnityEngine;

namespace WowStaticData
{
	public class FactionDB : MODB<int, FactionRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/Faction_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(FactionRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
