using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class GarrTalentDB
	{
		public GarrTalentRec GetRecord(int id)
		{
			return (GarrTalentRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<GarrTalentRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				GarrTalentRec obj2 = (GarrTalentRec)obj;
				if (!callback(obj2))
				{
					break;
				}
			}
		}

		public void EnumRecordsByParentID(int parentID, Predicate<GarrTalentRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				GarrTalentRec garrTalentRec = (GarrTalentRec)obj;
				if ((ulong)garrTalentRec.GarrTalentTreeID == (ulong)((long)parentID) && !callback(garrTalentRec))
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
				"/GarrTalent_",
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
					GarrTalentRec garrTalentRec = new GarrTalentRec();
					garrTalentRec.Deserialize(valueLine);
					this.m_records.Add(garrTalentRec.ID, garrTalentRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
