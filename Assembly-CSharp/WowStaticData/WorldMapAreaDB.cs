using System;
using UnityEngine;

namespace WowStaticData
{
	public class WorldMapAreaDB : MODB<int, WorldMapAreaRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/WorldMapArea.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(WorldMapAreaRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
