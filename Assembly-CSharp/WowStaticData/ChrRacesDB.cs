using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class ChrRacesDB : MODB<int, ChrRacesRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(path + "NonLocalized/ChrRaces.txt", nonLocalizedBundle);
			ChrRacesWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, ChrRacesRec> keyValuePair in this.m_records)
			{
				ChrRacesWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Flags, keyValuePair.Value.FactionID);
			}
			return result;
		}

		protected override void AddRecord(ChrRacesRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
