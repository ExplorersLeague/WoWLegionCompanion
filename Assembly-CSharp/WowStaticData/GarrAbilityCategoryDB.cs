using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrAbilityCategoryDB
	{
		public GarrAbilityCategoryRec GetRecord(int id)
		{
			return (!this.m_records.ContainsKey(id)) ? null : this.m_records[id];
		}

		public IEnumerable<GarrAbilityCategoryRec> GetRecordsWhere(Func<GarrAbilityCategoryRec, bool> matcher)
		{
			return this.m_records.Values.Where(matcher);
		}

		public GarrAbilityCategoryRec GetRecordFirstOrDefault(Func<GarrAbilityCategoryRec, bool> matcher)
		{
			return this.m_records.Values.FirstOrDefault(matcher);
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = string.Concat(new string[]
			{
				path,
				locale,
				"/GarrAbilityCategory_",
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
					GarrAbilityCategoryRec garrAbilityCategoryRec = new GarrAbilityCategoryRec();
					garrAbilityCategoryRec.Deserialize(valueLine);
					this.m_records.Add(garrAbilityCategoryRec.ID, garrAbilityCategoryRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Dictionary<int, GarrAbilityCategoryRec> m_records = new Dictionary<int, GarrAbilityCategoryRec>();
	}
}
