using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class FollowerListView : MonoBehaviour
	{
		private void Start()
		{
			this.InitFollowerList();
			Main instance = Main.instance;
			instance.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(instance.GarrisonDataResetFinishedAction, new Action(this.InitFollowerList));
		}

		private void OnEnable()
		{
			if (this.m_usedForMissionList)
			{
				if (AdventureMapPanel.instance != null)
				{
					AdventureMapPanel instance = AdventureMapPanel.instance;
					instance.MissionSelectedFromListAction = (Action<int>)Delegate.Combine(instance.MissionSelectedFromListAction, new Action<int>(this.HandleMissionChanged));
				}
			}
			else if (AdventureMapPanel.instance != null)
			{
				AdventureMapPanel instance2 = AdventureMapPanel.instance;
				instance2.MissionMapSelectionChangedAction = (Action<int>)Delegate.Combine(instance2.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
			}
		}

		private void OnDisable()
		{
			if (this.m_usedForMissionList)
			{
				if (AdventureMapPanel.instance != null)
				{
					AdventureMapPanel instance = AdventureMapPanel.instance;
					instance.MissionSelectedFromListAction = (Action<int>)Delegate.Remove(instance.MissionSelectedFromListAction, new Action<int>(this.HandleMissionChanged));
				}
			}
			else if (AdventureMapPanel.instance != null)
			{
				AdventureMapPanel instance2 = AdventureMapPanel.instance;
				instance2.MissionMapSelectionChangedAction = (Action<int>)Delegate.Remove(instance2.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
			}
		}

		private void SyncVisibleListOrderToSortedFollowerList()
		{
			FollowerListItem[] componentsInChildren = this.m_followerListViewContents.GetComponentsInChildren<FollowerListItem>(true);
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

		public void HandleMissionChanged(int garrMissionID)
		{
			if (garrMissionID == 0)
			{
				return;
			}
			this.SortFollowerListData();
			this.SyncVisibleListOrderToSortedFollowerList();
			this.UpdateUsefulAbilitiesDisplay(garrMissionID);
			this.RemoveAllFromParty();
			this.UpdateAllAvailabilityStatus();
		}

		private FollowerListItem InsertFollowerIntoListView(WrapperGarrisonFollower follower)
		{
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
			if (record == null)
			{
				return null;
			}
			if (record.GarrFollowerTypeID != 4u)
			{
				return null;
			}
			if (this.m_isCombatAllyList)
			{
				bool flag = (follower.Flags & 8) != 0;
				FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(follower);
				if (flag || follower.ZoneSupportSpellID <= 0 || followerStatus == FollowerStatus.inactive || followerStatus == FollowerStatus.fatigued || followerStatus == FollowerStatus.inBuilding)
				{
					return null;
				}
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_followerListItemPrefab);
			gameObject.transform.SetParent(this.m_followerListViewContents.transform, false);
			FollowerListItem component = gameObject.GetComponent<FollowerListItem>();
			component.SetFollower(follower);
			return component;
		}

		private void SortFollowerListData()
		{
			this.m_sortedFollowerList = PersistentFollowerData.followerDictionary.ToList<KeyValuePair<int, WrapperGarrisonFollower>>();
			FollowerListView.FollowerComparer followerComparer = new FollowerListView.FollowerComparer();
			followerComparer.m_missionDetailViewForComparer = this.m_missionDetailView;
			this.m_sortedFollowerList.Sort(followerComparer);
		}

		public void InitFollowerList()
		{
			FollowerListItem[] componentsInChildren = this.m_followerListViewContents.GetComponentsInChildren<FollowerListItem>(true);
			foreach (FollowerListItem followerListItem in componentsInChildren)
			{
				if (!PersistentFollowerData.followerDictionary.ContainsKey(followerListItem.m_followerID))
				{
					followerListItem.gameObject.SetActive(false);
					followerListItem.transform.SetParent(Main.instance.transform);
				}
				else
				{
					WrapperGarrisonFollower follower = PersistentFollowerData.followerDictionary[followerListItem.m_followerID];
					bool flag = (follower.Flags & 8) != 0;
					if (flag && follower.Durability <= 0)
					{
						followerListItem.gameObject.SetActive(false);
						followerListItem.transform.SetParent(Main.instance.transform);
					}
					else
					{
						followerListItem.SetFollower(follower);
					}
				}
			}
			this.m_followerListViewContents.transform.localPosition = new Vector3(this.m_followerListViewContents.transform.localPosition.x, 0f, this.m_followerListViewContents.transform.localPosition.z);
			this.SortFollowerListData();
			componentsInChildren = this.m_followerListViewContents.GetComponentsInChildren<FollowerListItem>(true);
			foreach (KeyValuePair<int, WrapperGarrisonFollower> keyValuePair in this.m_sortedFollowerList)
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
					bool flag3 = (keyValuePair.Value.Flags & 8) != 0;
					if (!flag3 || keyValuePair.Value.Durability > 0)
					{
						this.InsertFollowerIntoListView(keyValuePair.Value);
					}
				}
			}
		}

		public void UpdateUsefulAbilitiesDisplay(int missionID)
		{
			FollowerListItem[] componentsInChildren = this.m_followerListViewContents.GetComponentsInChildren<FollowerListItem>(true);
			Dictionary<uint, int> usefulCounterAbilityIDs = new Dictionary<uint, int>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].UpdateUsefulAbilitiesDisplay((!this.m_usedForMissionList) ? AdventureMapPanel.instance.GetCurrentMapMission() : missionID, usefulCounterAbilityIDs);
			}
		}

		public void RemoveAllFromParty()
		{
			FollowerListItem[] componentsInChildren = this.m_followerListViewContents.GetComponentsInChildren<FollowerListItem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].RemoveFromParty();
			}
		}

		private void UpdateAllAvailabilityStatus()
		{
			FollowerListItem[] componentsInChildren = this.m_followerListViewContents.GetComponentsInChildren<FollowerListItem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetAvailabilityStatus(PersistentFollowerData.followerDictionary[componentsInChildren[i].m_followerID]);
			}
		}

		private List<KeyValuePair<int, WrapperGarrisonFollower>> m_sortedFollowerList;

		public GameObject m_followerListViewContents;

		public GameObject m_followerListItemPrefab;

		public MissionDetailView m_missionDetailView;

		public bool m_isCombatAllyList;

		public bool m_usedForMissionList;

		private class FollowerComparer : IComparer<KeyValuePair<int, WrapperGarrisonFollower>>
		{
			private bool HasUsefulAbility(WrapperGarrisonFollower follower)
			{
				if (this.m_missionDetailViewForComparer == null)
				{
					return false;
				}
				MissionMechanic[] mechanics = this.m_missionDetailViewForComparer.gameObject.GetComponentsInChildren<MissionMechanic>(true);
				if (mechanics == null)
				{
					return false;
				}
				return (from id in follower.AbilityIDs
				select StaticDB.garrAbilityDB.GetRecord(id) into garrAbilityRec
				where garrAbilityRec != null && (garrAbilityRec.Flags & 1u) == 0u
				select garrAbilityRec).SelectMany((GarrAbilityRec garrAbilityRec) => StaticDB.garrAbilityEffectDB.GetRecordsByParentID(garrAbilityRec.ID)).Any(delegate(GarrAbilityEffectRec garrAbilityEffectRec)
				{
					if (garrAbilityEffectRec.GarrMechanicTypeID == 0u || garrAbilityEffectRec.AbilityAction != 0u)
					{
						return false;
					}
					GarrMechanicTypeRec garrMechanicTypeRec = StaticDB.garrMechanicTypeDB.GetRecord((int)garrAbilityEffectRec.GarrMechanicTypeID);
					return garrMechanicTypeRec != null && mechanics.Any((MissionMechanic mechanic) => mechanic.m_missionMechanicTypeID == garrMechanicTypeRec.ID);
				});
			}

			public int Compare(KeyValuePair<int, WrapperGarrisonFollower> follower1, KeyValuePair<int, WrapperGarrisonFollower> follower2)
			{
				WrapperGarrisonFollower value = follower1.Value;
				WrapperGarrisonFollower value2 = follower2.Value;
				FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(value);
				FollowerStatus followerStatus2 = GeneralHelpers.GetFollowerStatus(value2);
				if (followerStatus != followerStatus2)
				{
					return followerStatus - followerStatus2;
				}
				bool flag = this.HasUsefulAbility(value);
				bool flag2 = this.HasUsefulAbility(value2);
				if (flag != flag2)
				{
					return (!flag) ? 1 : -1;
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
				bool flag3 = (value.Flags & 8) != 0;
				bool flag4 = (value2.Flags & 8) != 0;
				if (flag3 != flag4)
				{
					return (!flag4) ? 1 : -1;
				}
				return 0;
			}

			public MissionDetailView m_missionDetailViewForComparer;
		}
	}
}
