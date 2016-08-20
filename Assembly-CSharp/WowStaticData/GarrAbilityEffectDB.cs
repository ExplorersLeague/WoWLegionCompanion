using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class GarrAbilityEffectDB
	{
		public GarrAbilityEffectRec GetRecord(int id)
		{
			return (GarrAbilityEffectRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<GarrAbilityEffectRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				GarrAbilityEffectRec obj2 = (GarrAbilityEffectRec)obj;
				if (!callback(obj2))
				{
					break;
				}
			}
		}

		public void EnumRecordsByParentID(int parentID, Predicate<GarrAbilityEffectRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				GarrAbilityEffectRec garrAbilityEffectRec = (GarrAbilityEffectRec)obj;
				if ((ulong)garrAbilityEffectRec.GarrAbilityID == (ulong)((long)parentID) && !callback(garrAbilityEffectRec))
				{
					break;
				}
			}
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = path + "NonLocalized/GarrAbilityEffect.txt";
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
					GarrAbilityEffectRec garrAbilityEffectRec = new GarrAbilityEffectRec();
					garrAbilityEffectRec.Deserialize(valueLine);
					this.m_records.Add(garrAbilityEffectRec.ID, garrAbilityEffectRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
