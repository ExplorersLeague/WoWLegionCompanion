using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class VW_MobileSpellDB
	{
		public VW_MobileSpellRec GetRecord(int id)
		{
			return (VW_MobileSpellRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<VW_MobileSpellRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				VW_MobileSpellRec obj2 = (VW_MobileSpellRec)obj;
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
				"/VW_MobileSpell_",
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
					VW_MobileSpellRec vw_MobileSpellRec = new VW_MobileSpellRec();
					vw_MobileSpellRec.Deserialize(valueLine);
					this.m_records.Add(vw_MobileSpellRec.ID, vw_MobileSpellRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
