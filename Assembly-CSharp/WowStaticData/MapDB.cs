using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class MapDB : MODB<int, MapRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/Map_",
				locale,
				".txt"
			}), localizedBundle);
			MapWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, MapRec> keyValuePair in this.m_records)
			{
				MapWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.MapName);
			}
			return result;
		}

		protected override void AddRecord(MapRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
