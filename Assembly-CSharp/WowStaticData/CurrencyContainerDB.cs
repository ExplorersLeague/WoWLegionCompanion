using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WoWCompanionApp;
using WowStatConstants;

namespace WowStaticData
{
	public class CurrencyContainerDB : MODB<int, CurrencyContainerRec>
	{
		public IEnumerable<CurrencyContainerRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.CurrencyTypeId == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/CurrencyContainer_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(CurrencyContainerRec rec)
		{
			this.m_records.Add(rec.ID, rec);
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
	}
}
