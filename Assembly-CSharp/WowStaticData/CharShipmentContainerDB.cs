using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class CharShipmentContainerDB
	{
		public CharShipmentContainerRec GetRecord(int id)
		{
			return (CharShipmentContainerRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<CharShipmentContainerRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				CharShipmentContainerRec obj2 = (CharShipmentContainerRec)obj;
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
				"/CharShipmentContainer_",
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
					CharShipmentContainerRec charShipmentContainerRec = new CharShipmentContainerRec();
					charShipmentContainerRec.Deserialize(valueLine);
					this.m_records.Add(charShipmentContainerRec.ID, charShipmentContainerRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
