using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class FollowerInventoryListView : MonoBehaviour
	{
		private void OnEnable()
		{
			Main instance = Main.instance;
			instance.ArmamentInventoryChangedAction = (Action)Delegate.Combine(instance.ArmamentInventoryChangedAction, new Action(this.HandleInventoryChanged));
			Main instance2 = Main.instance;
			instance2.EquipmentInventoryChangedAction = (Action)Delegate.Combine(instance2.EquipmentInventoryChangedAction, new Action(this.HandleInventoryChanged));
		}

		private void OnDisable()
		{
			Main instance = Main.instance;
			instance.ArmamentInventoryChangedAction = (Action)Delegate.Remove(instance.ArmamentInventoryChangedAction, new Action(this.HandleInventoryChanged));
			Main instance2 = Main.instance;
			instance2.EquipmentInventoryChangedAction = (Action)Delegate.Remove(instance2.EquipmentInventoryChangedAction, new Action(this.HandleInventoryChanged));
		}

		private void HandleInventoryChanged()
		{
			if (this.m_followerDetailView != null)
			{
				this.Init(this.m_followerDetailView, this.m_abilityToReplace);
			}
		}

		public void Init(FollowerDetailView followerDetailView, int abilityToReplace)
		{
			this.m_followerDetailView = followerDetailView;
			this.m_abilityToReplace = abilityToReplace;
			FollowerInventoryListItem[] componentsInChildren = this.m_equipmentInventoryContent.GetComponentsInChildren<FollowerInventoryListItem>(true);
			foreach (FollowerInventoryListItem followerInventoryListItem in componentsInChildren)
			{
				Object.Destroy(followerInventoryListItem.gameObject);
			}
			int num = 0;
			foreach (WrapperFollowerEquipment item in PersistentEquipmentData.equipmentDictionary.Values)
			{
				if (num == 0)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_headerPrefab);
					gameObject.transform.SetParent(this.m_equipmentInventoryContent.transform, false);
					FollowerInventoryListItem component = gameObject.GetComponent<FollowerInventoryListItem>();
					component.SetHeaderText("Equipment");
				}
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_followerInventoryListItemPrefab);
				gameObject2.transform.SetParent(this.m_equipmentInventoryContent.transform, false);
				FollowerInventoryListItem component2 = gameObject2.GetComponent<FollowerInventoryListItem>();
				component2.SetEquipment(item, followerDetailView, abilityToReplace);
				num++;
			}
			if (num == 0)
			{
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.m_headerPrefab);
				gameObject3.transform.SetParent(this.m_equipmentInventoryContent.transform, false);
				FollowerInventoryListItem component3 = gameObject3.GetComponent<FollowerInventoryListItem>();
				component3.SetHeaderText(StaticDB.GetString("NO_EQUIPMENT", null));
			}
			int num2 = 0;
			foreach (WrapperFollowerArmamentExt item2 in PersistentArmamentData.armamentDictionary.Values)
			{
				if (num2 == 0)
				{
					GameObject gameObject4 = Object.Instantiate<GameObject>(this.m_headerPrefab);
					gameObject4.transform.SetParent(this.m_equipmentInventoryContent.transform, false);
					FollowerInventoryListItem component4 = gameObject4.GetComponent<FollowerInventoryListItem>();
					component4.SetHeaderText("Armaments");
				}
				GameObject gameObject5 = Object.Instantiate<GameObject>(this.m_followerInventoryListItemPrefab);
				gameObject5.transform.SetParent(this.m_equipmentInventoryContent.transform, false);
				FollowerInventoryListItem component5 = gameObject5.GetComponent<FollowerInventoryListItem>();
				component5.SetArmament(item2, followerDetailView);
				num2++;
			}
			if (num == 0)
			{
				GameObject gameObject6 = Object.Instantiate<GameObject>(this.m_headerPrefab);
				gameObject6.transform.SetParent(this.m_equipmentInventoryContent.transform, false);
				FollowerInventoryListItem component6 = gameObject6.GetComponent<FollowerInventoryListItem>();
				component6.SetHeaderText(StaticDB.GetString("NO_ARMAMENTS", null));
			}
		}

		public GameObject m_headerPrefab;

		public GameObject m_followerInventoryListItemPrefab;

		public GameObject m_equipmentInventoryContent;

		private FollowerDetailView m_followerDetailView;

		private int m_abilityToReplace;
	}
}
