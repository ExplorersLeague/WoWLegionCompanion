using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrStringDB
	{
		public GarrStringRec GetRecord(int id)
		{
			return (!this.m_records.ContainsKey(id)) ? null : this.m_records[id];
		}

		public IEnumerable<GarrStringRec> GetRecordsWhere(Func<GarrStringRec, bool> matcher)
		{
			return this.m_records.Values.Where(matcher);
		}

		public GarrStringRec GetRecordFirstOrDefault(Func<GarrStringRec, bool> matcher)
		{
			return this.m_records.Values.FirstOrDefault(matcher);
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = string.Concat(new string[]
			{
				path,
				locale,
				"/GarrString_",
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
					GarrStringRec garrStringRec = new GarrStringRec();
					garrStringRec.Deserialize(valueLine);
					this.m_records.Add(garrStringRec.ID, garrStringRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Dictionary<int, GarrStringRec> m_records = new Dictionary<int, GarrStringRec>();
	}
}
