using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class SpamMessagesDB : MODB<int, SpamMessagesRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(path + "NonLocalized/SpamMessages.txt", nonLocalizedBundle);
			SpamMessagesWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, SpamMessagesRec> keyValuePair in this.m_records)
			{
				SpamMessagesWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Text);
			}
			return result;
		}

		protected override void AddRecord(SpamMessagesRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
