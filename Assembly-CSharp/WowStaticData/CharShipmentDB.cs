using System;
using System.Collections;
using UnityEngine;

namespace WowStaticData
{
	public class CharShipmentDB
	{
		public CharShipmentRec GetRecord(int id)
		{
			return (CharShipmentRec)this.m_records[id];
		}

		public void EnumRecords(Predicate<CharShipmentRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				CharShipmentRec obj2 = (CharShipmentRec)obj;
				if (!callback(obj2))
				{
					break;
				}
			}
		}

		public void EnumRecordsByParentID(int parentID, Predicate<CharShipmentRec> callback)
		{
			foreach (object obj in this.m_records.Values)
			{
				CharShipmentRec charShipmentRec = (CharShipmentRec)obj;
				if ((ulong)charShipmentRec.ContainerID == (ulong)((long)parentID) && !callback(charShipmentRec))
				{
					break;
				}
			}
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = path + "NonLocalized/CharShipment.txt";
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
					CharShipmentRec charShipmentRec = new CharShipmentRec();
					charShipmentRec.Deserialize(valueLine);
					this.m_records.Add(charShipmentRec.ID, charShipmentRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		private Hashtable m_records;
	}
}
