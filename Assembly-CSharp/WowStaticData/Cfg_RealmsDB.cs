using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class Cfg_RealmsDB : MODB<int, Cfg_RealmsRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(path + "NonLocalized/Cfg_Realms.txt", nonLocalizedBundle);
			Cfg_RealmsWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, Cfg_RealmsRec> keyValuePair in this.m_records)
			{
				Cfg_RealmsWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Region_ID);
			}
			return result;
		}

		protected override void AddRecord(Cfg_RealmsRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
