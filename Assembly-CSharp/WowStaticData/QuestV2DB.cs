using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class QuestV2DB
	{
		public QuestV2Rec GetRecord(int id)
		{
			return (!this.m_records.ContainsKey(id)) ? null : this.m_records[id];
		}

		public IEnumerable<QuestV2Rec> GetRecordsWhere(Func<QuestV2Rec, bool> matcher)
		{
			return this.m_records.Values.Where(matcher);
		}

		public QuestV2Rec GetRecordFirstOrDefault(Func<QuestV2Rec, bool> matcher)
		{
			return this.m_records.Values.FirstOrDefault(matcher);
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = string.Concat(new string[]
			{
				path,
				locale,
				"/QuestV2_",
				locale,
				".txt"
			});
			if (this.m_records.Count > 0)
			{
				Debug.Log("Already loaded static db " + text);
				return false;
			}
			TextAsset textAsset = localizedBundle.LoadAsset<TextAsset>(text);
			if (textAsset == null)
			{
				Debug.Log("Unable to load static db " + text);
				return false;
			}
			string text2 = textAsset.ToString();
			int num = 0;
			int num2;
			do
			{
				num2 = text2.IndexOf('\n', num);
				if (num2 >= 0)
				{
					string valueLine = text2.Substring(num, num2 - num + 1).Trim();
					QuestV2Rec questV2Rec = new QuestV2Rec();
					questV2Rec.Deserialize(valueLine);
					this.m_records.Add(questV2Rec.ID, questV2Rec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Dictionary<int, QuestV2Rec> m_records = new Dictionary<int, QuestV2Rec>();
	}
}
