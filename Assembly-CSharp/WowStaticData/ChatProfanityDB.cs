using System;
using System.Collections.Generic;
using UnityEngine;

namespace WowStaticData
{
	public class ChatProfanityDB : MODB<int, ChatProfanityRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			bool result = base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/ChatProfanity_",
				locale,
				".txt"
			}), localizedBundle);
			ChatProfanityWrapper.SetRecordsCount(this.m_records.Count);
			foreach (KeyValuePair<int, ChatProfanityRec> keyValuePair in this.m_records)
			{
				ChatProfanityWrapper.AddRecord(keyValuePair.Key, keyValuePair.Value.ID, keyValuePair.Value.Text, keyValuePair.Value.Language);
			}
			return result;
		}

		protected override void AddRecord(ChatProfanityRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
