using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class HolidayNamesDB : MODB<int, HolidayNamesRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/HolidayNames_",
				locale,
				".txt"
			}), localizedBundle);
			HolidayNamesWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, HolidayNamesRec> keyValuePair in this.m_records)
			{
				HolidayNamesWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Name, keyValuePair.Value.Name_flag);
			}
			return result;
		}

		protected override void AddRecord(HolidayNamesRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
