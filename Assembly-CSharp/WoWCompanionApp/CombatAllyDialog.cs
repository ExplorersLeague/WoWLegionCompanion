using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class CombatAllyDialog : MonoBehaviour
	{
		public void Start()
		{
			this.m_combatAllyCost.font = GeneralHelpers.LoadStandardFont();
			this.m_titleText.text = StaticDB.GetString("COMBAT_ALLY", null);
		}

		public void OnEnable()
		{
			Main.instance.m_UISound.Play_ShowGenericTooltip();
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void CreateCombatAllyItems(int combatAllyMissionID, int combatAllyMissionCost)
		{
			foreach (WrapperGarrisonFollower follower in PersistentFollowerData.followerDictionary.Values)
			{
				FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(follower);
				if (follower.ZoneSupportSpellID > 0 && (followerStatus == FollowerStatus.available || followerStatus == FollowerStatus.onMission))
				{
					FollowerInventoryListItem followerInventoryListItem = Object.Instantiate<FollowerInventoryListItem>(this.m_combatAllyChampionListItemPrefab);
					followerInventoryListItem.transform.SetParent(this.m_combatAllyListContent.transform, false);
					followerInventoryListItem.SetCombatAllyChampion(follower, combatAllyMissionID, combatAllyMissionCost);
				}
			}
		}

		public void Init()
		{
			FollowerInventoryListItem[] componentsInChildren = this.m_combatAllyListContent.GetComponentsInChildren<FollowerInventoryListItem>(true);
			foreach (FollowerInventoryListItem followerInventoryListItem in componentsInChildren)
			{
				Object.Destroy(followerInventoryListItem.gameObject);
			}
			int num = 0;
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null)
				{
					if ((record.Flags & 16u) != 0u)
					{
						this.CreateCombatAllyItems(wrapperGarrisonMission.MissionRecID, (int)record.MissionCost);
						num = (int)record.MissionCost;
						break;
					}
				}
			}
			if (num <= GarrisonStatus.Resources())
			{
				this.m_combatAllyCost.text = string.Concat(new object[]
				{
					StaticDB.GetString("COST2", "Cost:"),
					" <color=#ffffffff>",
					num,
					"</color>"
				});
			}
			else
			{
				this.m_combatAllyCost.text = string.Concat(new object[]
				{
					StaticDB.GetString("COST2", "Cost:"),
					" <color=#ff0000ff>",
					num,
					"</color>"
				});
			}
			Sprite sprite = GeneralHelpers.LoadCurrencyIcon(1220);
			if (sprite != null)
			{
				this.m_combatAllyCostResourceIcon.sprite = sprite;
			}
		}

		public FollowerInventoryListItem m_combatAllyChampionListItemPrefab;

		public GameObject m_combatAllyListContent;

		public Text m_combatAllyCost;

		public Image m_combatAllyCostResourceIcon;

		public Text m_titleText;
	}
}
