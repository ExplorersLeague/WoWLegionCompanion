using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class ChrClassesDB : MODB<int, ChrClassesRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/ChrClasses_",
				locale,
				".txt"
			}), localizedBundle);
			ChrClassesWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, ChrClassesRec> keyValuePair in this.m_records)
			{
				ChrClassesWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Name, keyValuePair.Value.IconFileDataID, keyValuePair.Value.Filename);
			}
			return result;
		}

		protected override void AddRecord(ChrClassesRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
