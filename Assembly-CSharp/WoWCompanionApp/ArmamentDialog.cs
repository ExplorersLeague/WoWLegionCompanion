using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class ArmamentDialog : MonoBehaviour
	{
		private void Awake()
		{
			this.m_titleText.font = GeneralHelpers.LoadFancyFont();
			this.m_titleText.text = StaticDB.GetString("CHAMPION_ENHANCEMENT", null);
			this.m_emptyMessage.font = GeneralHelpers.LoadStandardFont();
			this.m_emptyMessage.text = StaticDB.GetString("NO_ARMAMENTS2", "You do not have any armaments to equip.");
		}

		public void OnEnable()
		{
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
			Main.instance.m_UISound.Play_ShowGenericTooltip();
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main instance = Main.instance;
			instance.ArmamentInventoryChangedAction = (Action)Delegate.Combine(instance.ArmamentInventoryChangedAction, new Action(this.HandleArmamentsChanged));
			LegionCompanionWrapper.RequestFollowerArmamentsExtended((int)GarrisonStatus.GarrisonFollowerType);
		}

		private void OnDisable()
		{
			Main.instance.m_backButtonManager.PopBackAction();
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main instance = Main.instance;
			instance.ArmamentInventoryChangedAction = (Action)Delegate.Remove(instance.ArmamentInventoryChangedAction, new Action(this.HandleArmamentsChanged));
			this.m_currentFollowerDetailView = null;
		}

		public void Init(FollowerDetailView followerDetailView)
		{
			this.m_currentFollowerDetailView = followerDetailView;
			FollowerInventoryListItem[] componentsInChildren = this.m_armamentListContent.GetComponentsInChildren<FollowerInventoryListItem>(true);
			foreach (FollowerInventoryListItem followerInventoryListItem in componentsInChildren)
			{
				Object.Destroy(followerInventoryListItem.gameObject);
			}
			bool active = true;
			foreach (WrapperFollowerArmamentExt item in PersistentArmamentData.armamentDictionary.Values)
			{
				FollowerInventoryListItem followerInventoryListItem2 = Object.Instantiate<FollowerInventoryListItem>(this.m_armamentListItemPrefab);
				followerInventoryListItem2.transform.SetParent(this.m_armamentListContent.transform, false);
				followerInventoryListItem2.SetArmament(item, followerDetailView);
				active = false;
			}
			this.m_emptyMessage.gameObject.SetActive(active);
		}

		private void HandleArmamentsChanged()
		{
			if (this.m_currentFollowerDetailView != null)
			{
				this.Init(this.m_currentFollowerDetailView);
			}
		}

		public FollowerInventoryListItem m_armamentListItemPrefab;

		public GameObject m_armamentListContent;

		public Text m_titleText;

		public Text m_emptyMessage;

		private FollowerDetailView m_currentFollowerDetailView;
	}
}
