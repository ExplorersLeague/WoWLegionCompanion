using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class HolidaysDB : MODB<int, HolidaysRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(path + "NonLocalized/Holidays.txt", nonLocalizedBundle);
			HolidaysWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, HolidaysRec> keyValuePair in this.m_records)
			{
				HolidaysWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Duration.ToList<ushort>(), keyValuePair.Value.Date.ToList<uint>(), keyValuePair.Value.Region, keyValuePair.Value.Looping, keyValuePair.Value.CalendarFlags.ToList<byte>(), keyValuePair.Value.HolidayNameID, keyValuePair.Value.HolidayDescriptionID, keyValuePair.Value.TextureFileName, keyValuePair.Value.TextureFileDataID.ToList<int>(), keyValuePair.Value.Priority, keyValuePair.Value.CalendarFilterType, keyValuePair.Value.Flags);
			}
			return result;
		}

		protected override void AddRecord(HolidaysRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
