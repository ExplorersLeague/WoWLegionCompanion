using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoWCompanionApp
{
	public class ItemStatCache : MonoBehaviour
	{
		private void Awake()
		{
			if (ItemStatCache.s_instance == null)
			{
				ItemStatCache.s_instance = this;
			}
		}

		public static ItemStatCache instance
		{
			get
			{
				return ItemStatCache.s_instance;
			}
		}

		public WrapperItemStats? GetItemStats(int itemID, int itemContext)
		{
			if (this.m_records.ContainsKey(itemID))
			{
				return new WrapperItemStats?(this.m_records[itemID]);
			}
			LegionCompanionWrapper.GetItemTooltipInfo(itemID, itemContext);
			return null;
		}

		public void AddMobileItemStats(int itemID, int itemContext, WrapperItemStats stats)
		{
			if (!this.m_records.ContainsKey(itemID))
			{
				this.m_records.Add(itemID, stats);
			}
			else
			{
				this.m_records[itemID] = stats;
			}
			if (this.ItemStatCacheUpdateAction != null)
			{
				this.ItemStatCacheUpdateAction(itemID, itemContext, stats);
			}
		}

		public void ClearItemStats()
		{
			this.m_records.Clear();
		}

		private static ItemStatCache s_instance;

		private Dictionary<int, WrapperItemStats> m_records = new Dictionary<int, WrapperItemStats>();

		public Action<int, int, WrapperItemStats> ItemStatCacheUpdateAction;
	}
}
