using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class Cfg_LanguagesDB : MODB<int, Cfg_LanguagesRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(path + "NonLocalized/Cfg_Languages.txt", nonLocalizedBundle);
			Cfg_LanguagesWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, Cfg_LanguagesRec> keyValuePair in this.m_records)
			{
				Cfg_LanguagesWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Wowlocale_ID, keyValuePair.Value.Wowlocale_code);
			}
			return result;
		}

		protected override void AddRecord(Cfg_LanguagesRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
