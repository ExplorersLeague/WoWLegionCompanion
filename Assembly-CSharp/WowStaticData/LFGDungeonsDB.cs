using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class LFGDungeonsDB : MODB<int, LFGDungeonsRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/LFGDungeons_",
				locale,
				".txt"
			}), localizedBundle);
			LFGDungeonsWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, LFGDungeonsRec> keyValuePair in this.m_records)
			{
				LFGDungeonsWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Name, keyValuePair.Value.TypeID, keyValuePair.Value.Subtype, keyValuePair.Value.Faction, keyValuePair.Value.ExpansionLevel, keyValuePair.Value.DifficultyID, keyValuePair.Value.Flags.ToList<int>(), keyValuePair.Value.IconTextureFileID, keyValuePair.Value.MapID);
			}
			return result;
		}

		protected override void AddRecord(LFGDungeonsRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
