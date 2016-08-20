using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class OrderHallFollowersPanel : MonoBehaviour
{
	private void Awake()
	{
		OrderHallFollowersPanel.instance = this;
	}

	private void Start()
	{
		this.InitFollowerList();
		Main main = Main.instance;
		main.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(main.GarrisonDataResetFinishedAction, new Action(this.InitFollowerList));
		Main main2 = Main.instance;
		main2.FollowerDataChangedAction = (Action)Delegate.Combine(main2.FollowerDataChangedAction, new Action(this.InitFollowerList));
	}

	private void Update()
	{
		if (this.m_panelViewRT.sizeDelta.x != this.m_parentViewRT.rect.width)
		{
			this.m_multiPanelViewSizeDelta = this.m_panelViewRT.sizeDelta;
			this.m_multiPanelViewSizeDelta.x = this.m_parentViewRT.rect.width;
			this.m_panelViewRT.sizeDelta = this.m_multiPanelViewSizeDelta;
		}
	}

	private void SortFollowerListData()
	{
		this.m_sortedFollowerList = PersistentFollowerData.followerDictionary.ToList<KeyValuePair<int, JamGarrisonFollower>>();
		OrderHallFollowersPanel.FollowerComparer comparer = new OrderHallFollowersPanel.FollowerComparer();
		this.m_sortedFollowerList.Sort(comparer);
	}

	private void InsertFollowerIntoListView(JamGarrisonFollower follower, FollowerCategory followerCategory)
	{
		GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
		if (record == null)
		{
			return;
		}
		if (record.GarrFollowerTypeID != 4u)
		{
			return;
		}
		bool flag = (follower.Flags & 8) != 0;
		bool flag2 = !flag;
		FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(follower);
		switch (followerCategory)
		{
		case FollowerCategory.ActiveChampion:
			if (!flag2 || followerStatus == FollowerStatus.inactive)
			{
				return;
			}
			break;
		case FollowerCategory.InactiveChampion:
			if (!flag2 || followerStatus != FollowerStatus.inactive)
			{
				return;
			}
			break;
		case FollowerCategory.Troop:
			if (!flag || follower.Durability <= 0)
			{
				return;
			}
			break;
		default:
			return;
		}
		FollowerListItem followerListItem = Object.Instantiate<FollowerListItem>(this.m_followerDetailListItemPrefab);
		followerListItem.transform.SetParent(this.m_followerDetailListContent.transform, false);
		followerListItem.SetFollower(follower);
		AutoHide autoHide = followerListItem.m_followerDetailView.gameObject.AddComponent<AutoHide>();
		autoHide.m_clipRT = this.m_panelViewRT;
		AutoHide autoHide2 = followerListItem.m_listItemArea.gameObject.AddComponent<AutoHide>();
		autoHide2.m_clipRT = this.m_panelViewRT;
	}

	private void SyncVisibleListOrderToSortedFollowerList()
	{
		FollowerListItem[] componentsInChildren = this.m_followerDetailListContent.GetComponentsInChildren<FollowerListItem>(true);
		for (int i = 0; i < this.m_sortedFollowerList.Count; i++)
		{
			foreach (FollowerListItem followerListItem in componentsInChildren)
			{
				if (followerListItem.m_followerID == this.m_sortedFollowerList[i].Value.GarrFollowerID)
				{
					followerListItem.transform.SetSiblingIndex(i);
					break;
				}
			}
		}
	}

	private void InitFollowerList()
	{
		FollowerListItem[] componentsInChildren = this.m_followerDetailListContent.GetComponentsInChildren<FollowerListItem>(true);
		foreach (FollowerListItem followerListItem in componentsInChildren)
		{
			if (!PersistentFollowerData.followerDictionary.ContainsKey(followerListItem.m_followerID))
			{
				followerListItem.gameObject.SetActive(false);
				followerListItem.transform.SetParent(Main.instance.transform);
			}
			else
			{
				JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[followerListItem.m_followerID];
				bool flag = (jamGarrisonFollower.Flags & 8) != 0;
				if (flag && jamGarrisonFollower.Durability <= 0)
				{
					followerListItem.gameObject.SetActive(false);
					followerListItem.transform.SetParent(Main.instance.transform);
				}
				else
				{
					followerListItem.SetFollower(jamGarrisonFollower);
				}
			}
		}
		this.SortFollowerListData();
		if (this.m_championsHeader == null)
		{
			this.m_championsHeader = Object.Instantiate<FollowerListHeader>(this.m_followerListHeaderPrefab);
		}
		this.m_championsHeader.transform.SetParent(this.m_followerDetailListContent.transform, false);
		this.m_championsHeader.m_title.font = GeneralHelpers.LoadStandardFont();
		this.m_championsHeader.m_title.text = StaticDB.GetString("CHAMPIONS", null) + ": ";
		int numActiveChampions = GeneralHelpers.GetNumActiveChampions();
		int maxActiveChampions = GeneralHelpers.GetMaxActiveChampions();
		if (numActiveChampions <= maxActiveChampions)
		{
			this.m_championsHeader.m_count.text = string.Concat(new object[]
			{
				string.Empty,
				numActiveChampions,
				"/",
				maxActiveChampions
			});
		}
		else
		{
			this.m_championsHeader.m_count.text = string.Concat(new object[]
			{
				"<color=#ff0000ff>",
				numActiveChampions,
				"/",
				maxActiveChampions,
				"</color>"
			});
		}
		AutoHide autoHide = this.m_championsHeader.gameObject.AddComponent<AutoHide>();
		autoHide.m_clipRT = this.m_panelViewRT;
		foreach (KeyValuePair<int, JamGarrisonFollower> keyValuePair in this.m_sortedFollowerList)
		{
			bool flag2 = false;
			foreach (FollowerListItem followerListItem2 in componentsInChildren)
			{
				if (followerListItem2.m_followerID == keyValuePair.Value.GarrFollowerID)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				this.InsertFollowerIntoListView(keyValuePair.Value, FollowerCategory.ActiveChampion);
			}
		}
		int numTroops = GeneralHelpers.GetNumTroops();
		if (this.m_troopsHeader == null)
		{
			this.m_troopsHeader = Object.Instantiate<FollowerListHeader>(this.m_followerListHeaderPrefab);
		}
		this.m_troopsHeader.transform.SetParent(this.m_followerDetailListContent.transform, false);
		this.m_troopsHeader.m_title.font = GeneralHelpers.LoadStandardFont();
		this.m_troopsHeader.m_title.text = StaticDB.GetString("TROOPS", null) + ": ";
		this.m_troopsHeader.m_count.font = GeneralHelpers.LoadStandardFont();
		this.m_troopsHeader.m_count.text = string.Empty + numTroops;
		autoHide = this.m_troopsHeader.gameObject.AddComponent<AutoHide>();
		autoHide.m_clipRT = this.m_panelViewRT;
		foreach (KeyValuePair<int, JamGarrisonFollower> keyValuePair2 in this.m_sortedFollowerList)
		{
			bool flag3 = false;
			foreach (FollowerListItem followerListItem3 in componentsInChildren)
			{
				if (followerListItem3.m_followerID == keyValuePair2.Value.GarrFollowerID)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				this.InsertFollowerIntoListView(keyValuePair2.Value, FollowerCategory.Troop);
			}
		}
		int numInactiveChampions = GeneralHelpers.GetNumInactiveChampions();
		if (this.m_inactiveHeader == null)
		{
			this.m_inactiveHeader = Object.Instantiate<FollowerListHeader>(this.m_followerListHeaderPrefab);
		}
		this.m_inactiveHeader.transform.SetParent(this.m_followerDetailListContent.transform, false);
		this.m_inactiveHeader.m_title.font = GeneralHelpers.LoadStandardFont();
		this.m_inactiveHeader.m_title.text = StaticDB.GetString("INACTIVE", null) + ": ";
		this.m_inactiveHeader.m_count.font = GeneralHelpers.LoadStandardFont();
		this.m_inactiveHeader.m_count.text = string.Empty + numInactiveChampions;
		autoHide = this.m_inactiveHeader.gameObject.AddComponent<AutoHide>();
		autoHide.m_clipRT = this.m_panelViewRT;
		foreach (KeyValuePair<int, JamGarrisonFollower> keyValuePair3 in this.m_sortedFollowerList)
		{
			bool flag4 = false;
			foreach (FollowerListItem followerListItem4 in componentsInChildren)
			{
				if (followerListItem4.m_followerID == keyValuePair3.Value.GarrFollowerID)
				{
					flag4 = true;
					break;
				}
			}
			if (!flag4)
			{
				this.InsertFollowerIntoListView(keyValuePair3.Value, FollowerCategory.InactiveChampion);
			}
		}
		this.SyncVisibleListOrderToSortedFollowerList();
		this.m_championsHeader.gameObject.SetActive(numActiveChampions > 0);
		this.m_troopsHeader.gameObject.SetActive(numTroops > 0);
		this.m_inactiveHeader.gameObject.SetActive(numInactiveChampions > 0);
		this.m_championsHeader.transform.SetSiblingIndex(0);
		this.m_troopsHeader.transform.SetSiblingIndex(numActiveChampions + 1);
		this.m_inactiveHeader.transform.SetSiblingIndex(numActiveChampions + numTroops + 2);
	}

	private void ScrollListTo_Update(float offsetY)
	{
		Vector3 localPosition = this.m_followerDetailListContent.transform.localPosition;
		localPosition.y = offsetY;
		this.m_followerDetailListContent.transform.localPosition = localPosition;
	}

	private void ScrollListTo_Complete()
	{
		Vector3 localPosition = this.m_followerDetailListContent.transform.localPosition;
		localPosition.y = this.m_scrollListToOffset;
		this.m_followerDetailListContent.transform.localPosition = localPosition;
		this.m_listScrollRect.enabled = true;
	}

	public void ScrollListTo(float offsetY)
	{
		this.m_scrollListToOffset = offsetY;
		this.m_listScrollRect.enabled = false;
		iTween.StopByName(base.gameObject, "ScrollListTo");
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"name",
			"ScrollListTo",
			"from",
			this.m_followerDetailListContent.transform.localPosition.y,
			"to",
			offsetY,
			"time",
			0.25f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"onupdate",
			"ScrollListTo_Update",
			"oncomplete",
			"ScrollListTo_Complete"
		}));
	}

	public void FollowerDetailListItemSelected(int garrFollowerID)
	{
		if (this.FollowerDetailListItemSelectedAction != null)
		{
			this.FollowerDetailListItemSelectedAction(garrFollowerID);
		}
	}

	public RectTransform m_parentViewRT;

	public RectTransform m_panelViewRT;

	public FollowerListItem m_followerDetailListItemPrefab;

	public GameObject m_followerDetailListContent;

	public FollowerListHeader m_followerListHeaderPrefab;

	public ScrollRect m_listScrollRect;

	public static OrderHallFollowersPanel instance;

	public Action<int> FollowerDetailListItemSelectedAction;

	private Vector2 m_multiPanelViewSizeDelta;

	private List<KeyValuePair<int, JamGarrisonFollower>> m_sortedFollowerList;

	private float m_scrollListToOffset;

	private FollowerListHeader m_championsHeader;

	private FollowerListHeader m_troopsHeader;

	private FollowerListHeader m_inactiveHeader;

	private class FollowerComparer : IComparer<KeyValuePair<int, JamGarrisonFollower>>
	{
		public int Compare(KeyValuePair<int, JamGarrisonFollower> follower1, KeyValuePair<int, JamGarrisonFollower> follower2)
		{
			JamGarrisonFollower value = follower1.Value;
			JamGarrisonFollower value2 = follower2.Value;
			FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(value);
			FollowerStatus followerStatus2 = GeneralHelpers.GetFollowerStatus(value2);
			bool flag = (value.Flags & 8) != 0;
			bool flag2 = (value2.Flags & 8) != 0;
			bool flag3 = !flag && followerStatus != FollowerStatus.inactive;
			bool flag4 = !flag2 && followerStatus2 != FollowerStatus.inactive;
			if (flag3 != flag4)
			{
				return (!flag3) ? 1 : -1;
			}
			if (followerStatus != followerStatus2)
			{
				return followerStatus - followerStatus2;
			}
			int num = (value.ItemLevelArmor + value.ItemLevelWeapon) / 2;
			int num2 = (value2.ItemLevelArmor + value2.ItemLevelWeapon) / 2;
			if (num2 != num)
			{
				return num2 - num;
			}
			if (value.Quality != value2.Quality)
			{
				return value2.Quality - value.Quality;
			}
			return 0;
		}
	}
}
