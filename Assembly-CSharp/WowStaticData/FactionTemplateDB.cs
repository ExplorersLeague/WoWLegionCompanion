using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WoWCompanionApp;

namespace WowStaticData
{
	public class FactionTemplateDB : MODB<int, FactionTemplateRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(path + "NonLocalized/FactionTemplate.txt", nonLocalizedBundle);
			FactionTemplateWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, FactionTemplateRec> keyValuePair in this.m_records)
			{
				FactionTemplateWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Faction, keyValuePair.Value.Flags, keyValuePair.Value.FactionGroup, keyValuePair.Value.FriendGroup, keyValuePair.Value.EnemyGroup, keyValuePair.Value.Enemies.ToList<int>(), keyValuePair.Value.Friend.ToList<int>());
			}
			return result;
		}

		protected override void AddRecord(FactionTemplateRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}

		public int? GetFactionGroup(int raceID)
		{
			ChrRacesRec record = StaticDB.chrRacesDB.GetRecord(raceID);
			if (record != null)
			{
				FactionTemplateRec record2 = StaticDB.factionTemplateDB.GetRecord(record.FactionID);
				if (record2 != null)
				{
					return new int?(record2.FactionGroup);
				}
			}
			return null;
		}
	}
}
