using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollowerLevelXPDB
	{
		public GarrFollowerLevelXPRec GetRecord(int id)
		{
			return (GarrFollowerLevelXPRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<GarrFollowerLevelXPRec> callback)
		{
			IEnumerator enumerator = this.m_records.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					GarrFollowerLevelXPRec obj2 = (GarrFollowerLevelXPRec)obj;
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

		public void EnumRecordsByParentID(int parentID, Predicate<GarrFollowerLevelXPRec> callback)
		{
			IEnumerator enumerator = this.m_records.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					GarrFollowerLevelXPRec garrFollowerLevelXPRec = (GarrFollowerLevelXPRec)obj;
					if ((ulong)garrFollowerLevelXPRec.FollowerLevel == (ulong)((long)parentID) && !callback(garrFollowerLevelXPRec))
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
			string text = path + "NonLocalized/GarrFollowerLevelXP.txt";
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
					GarrFollowerLevelXPRec garrFollowerLevelXPRec = new GarrFollowerLevelXPRec();
					garrFollowerLevelXPRec.Deserialize(valueLine);
					this.m_records.Add(garrFollowerLevelXPRec.ID, garrFollowerLevelXPRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
