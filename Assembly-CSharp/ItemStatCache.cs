using System;
using System.Collections;
using UnityEngine;
using WowJamMessages.MobileClientJSON;
using WowJamMessages.MobilePlayerJSON;

public class ItemStatCache : MonoBehaviour
{
	private void Awake()
	{
		this.m_records = new Hashtable();
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

	public MobileItemStats GetItemStats(int itemID, int itemContext)
	{
		MobileItemStats mobileItemStats = (MobileItemStats)this.m_records[itemID];
		if (mobileItemStats != null)
		{
			return mobileItemStats;
		}
		MobilePlayerGetItemTooltipInfo mobilePlayerGetItemTooltipInfo = new MobilePlayerGetItemTooltipInfo();
		mobilePlayerGetItemTooltipInfo.ItemID = itemID;
		mobilePlayerGetItemTooltipInfo.ItemContext = itemContext;
		Login.instance.SendToMobileServer(mobilePlayerGetItemTooltipInfo);
		return null;
	}

	public void AddMobileItemStats(int itemID, int itemContext, MobileItemStats stats)
	{
		if (this.m_records[itemID] == null)
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

	private Hashtable m_records;

	public Action<int, int, MobileItemStats> ItemStatCacheUpdateAction;
}
