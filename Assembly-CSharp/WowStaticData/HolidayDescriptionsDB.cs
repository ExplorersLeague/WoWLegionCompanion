using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class HolidayDescriptionsDB : MODB<int, HolidayDescriptionsRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/HolidayDescriptions_",
				locale,
				".txt"
			}), localizedBundle);
			HolidayDescriptionsWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, HolidayDescriptionsRec> keyValuePair in this.m_records)
			{
				HolidayDescriptionsWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Description);
			}
			return result;
		}

		protected override void AddRecord(HolidayDescriptionsRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
