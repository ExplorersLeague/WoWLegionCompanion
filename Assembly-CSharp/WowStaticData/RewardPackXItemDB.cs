using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class RewardPackXItemDB
	{
		public RewardPackXItemRec GetRecord(int id)
		{
			return (RewardPackXItemRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<RewardPackXItemRec> callback)
		{
			IEnumerator enumerator = this.m_records.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RewardPackXItemRec obj2 = (RewardPackXItemRec)obj;
					if (!callback(obj2))
					{
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void EnumRecordsByParentID(int parentID, Predicate<RewardPackXItemRec> callback)
		{
			IEnumerator enumerator = this.m_records.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RewardPackXItemRec rewardPackXItemRec = (RewardPackXItemRec)obj;
					if (rewardPackXItemRec.RewardPackID == parentID && !callback(rewardPackXItemRec))
					{
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = path + "NonLocalized/RewardPackXItem.txt";
			if (this.m_records != null)
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
			this.m_records = new Hashtable();
			int num = 0;
			int num2;
			do
			{
				num2 = text2.IndexOf('\n', num);
				if (num2 >= 0)
				{
					string valueLine = text2.Substring(num, num2 - num + 1).Trim();
					RewardPackXItemRec rewardPackXItemRec = new RewardPackXItemRec();
					rewardPackXItemRec.Deserialize(valueLine);
					this.m_records.Add(rewardPackXItemRec.ID, rewardPackXItemRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
