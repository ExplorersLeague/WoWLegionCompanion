using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class Cfg_RegionsDB : MODB<int, Cfg_RegionsRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/Cfg_Regions_",
				locale,
				".txt"
			}), localizedBundle);
			Cfg_RegionsWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, Cfg_RegionsRec> keyValuePair in this.m_records)
			{
				Cfg_RegionsWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Name, keyValuePair.Value.Region_ID, keyValuePair.Value.Region_group_mask);
			}
			return result;
		}

		protected override void AddRecord(Cfg_RegionsRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
