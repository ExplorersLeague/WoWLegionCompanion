using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class RewardPackXCurrencyTypeDB
	{
		public RewardPackXCurrencyTypeRec GetRecord(int id)
		{
			return (!this.m_records.ContainsKey(id)) ? null : this.m_records[id];
		}

		public IEnumerable<RewardPackXCurrencyTypeRec> GetRecordsWhere(Func<RewardPackXCurrencyTypeRec, bool> matcher)
		{
			return this.m_records.Values.Where(matcher);
		}

		public RewardPackXCurrencyTypeRec GetRecordFirstOrDefault(Func<RewardPackXCurrencyTypeRec, bool> matcher)
		{
			return this.m_records.Values.FirstOrDefault(matcher);
		}

		public IEnumerable<RewardPackXCurrencyTypeRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.RewardPackID == parentID
			select rec;
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = path + "NonLocalized/RewardPackXCurrencyType.txt";
			if (this.m_records.Count > 0)
			{
				Debug.Log("Already loaded static db " + text);
				return false;
			}
			TextAsset textAsset = nonLocalizedBundle.LoadAsset<TextAsset>(text);
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
					RewardPackXCurrencyTypeRec rewardPackXCurrencyTypeRec = new RewardPackXCurrencyTypeRec();
					rewardPackXCurrencyTypeRec.Deserialize(valueLine);
					this.m_records.Add(rewardPackXCurrencyTypeRec.ID, rewardPackXCurrencyTypeRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Dictionary<int, RewardPackXCurrencyTypeRec> m_records = new Dictionary<int, RewardPackXCurrencyTypeRec>();
	}
}
