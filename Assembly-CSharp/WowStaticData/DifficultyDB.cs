using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class DifficultyDB : MODB<int, DifficultyRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/Difficulty_",
				locale,
				".txt"
			}), localizedBundle);
			DifficultyWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, DifficultyRec> keyValuePair in this.m_records)
			{
				DifficultyWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.OrderIndex, keyValuePair.Value.Name);
			}
			return result;
		}

		protected override void AddRecord(DifficultyRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
