using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrTalentTreeDB : MODB<int, GarrTalentTreeRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrTalentTree.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrTalentTreeRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
