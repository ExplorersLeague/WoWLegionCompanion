using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class QuestV2DB
	{
		public QuestV2Rec GetRecord(int id)
		{
			return (QuestV2Rec)this.m_records[id];
		}

		public void EnumRecords(Predicate<QuestV2Rec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				QuestV2Rec obj2 = (QuestV2Rec)obj;
				if (!callback(obj2))
				{
					break;
				}
			}
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
			if (this.m_records != null)
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
			this.m_records = new Hashtable();
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

		private Hashtable m_records;
	}
}
