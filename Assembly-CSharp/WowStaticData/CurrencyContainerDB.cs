using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WoWCompanionApp;
using WowStatConstants;

namespace WowStaticData
{
	public class CurrencyContainerDB
	{
		public CurrencyContainerRec GetRecord(int id)
		{
			return (!this.m_records.ContainsKey(id)) ? null : this.m_records[id];
		}

		public IEnumerable<CurrencyContainerRec> GetRecordsWhere(Func<CurrencyContainerRec, bool> matcher)
		{
			return this.m_records.Values.Where(matcher);
		}

		public CurrencyContainerRec GetRecordFirstOrDefault(Func<CurrencyContainerRec, bool> matcher)
		{
			return this.m_records.Values.FirstOrDefault(matcher);
		}

		public IEnumerable<CurrencyContainerRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.CurrencyTypeId == parentID
			select rec;
		}

		public bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			string text = string.Concat(new string[]
			{
				path,
				locale,
				"/CurrencyContainer_",
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
					CurrencyContainerRec currencyContainerRec = new CurrencyContainerRec();
					currencyContainerRec.Deserialize(valueLine);
					this.m_records.Add(currencyContainerRec.ID, currencyContainerRec);
					num = num2 + 1;
				}
			}
			while (num2 > 0);
			return true;
		}

		public static bool IsCurrencyContainerValid(int quantity, int minAmount, int maxAmount)
		{
			return minAmount <= quantity && (maxAmount == 0 || maxAmount >= quantity);
		}

		public static CurrencyContainerRec CheckAndGetValidCurrencyContainer(int currencyType, int quantity)
		{
			foreach (CurrencyContainerRec currencyContainerRec in StaticDB.currencyContainerDB.GetRecordsByParentID(currencyType))
			{
				if (CurrencyContainerDB.IsCurrencyContainerValid(quantity, currencyContainerRec.MinAmount, currencyContainerRec.MaxAmount))
				{
					return currencyContainerRec;
				}
			}
			return null;
		}

		public static Sprite LoadCurrencyContainerIcon(int currencyType, int quantity)
		{
			CurrencyContainerRec currencyContainerRec = CurrencyContainerDB.CheckAndGetValidCurrencyContainer(currencyType, quantity);
			if (currencyContainerRec != null)
			{
				return GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, currencyContainerRec.ContainerIconID);
			}
			return GeneralHelpers.LoadCurrencyIcon(currencyType);
		}

		private Dictionary<int, CurrencyContainerRec> m_records = new Dictionary<int, CurrencyContainerRec>();
	}
}
