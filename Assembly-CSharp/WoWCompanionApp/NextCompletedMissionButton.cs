﻿using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class NextCompletedMissionButton : MonoBehaviour
	{
		private void OnEnable()
		{
			this.m_theActualButton.SetActive(false);
			this.m_numReadyTroops = 0;
			this.m_numReadyTroopsText.text = string.Empty;
			this.m_numReadyTroopsTextBG.SetActive(false);
			if (this.m_treasureChestHorde != null && this.m_treasureChestAlliance != null)
			{
				if (GarrisonStatus.Faction() == PVP_FACTION.HORDE)
				{
					this.m_treasureChestHorde.SetActive(true);
					this.m_treasureChestAlliance.SetActive(false);
				}
				else
				{
					this.m_treasureChestHorde.SetActive(false);
					this.m_treasureChestAlliance.SetActive(true);
				}
			}
		}

		private void ClearEffects()
		{
			iTween.StopByName(this.m_theActualButton, "RecruitWobble");
			iTween.StopByName(this.m_theActualButton, "RecruitWobbleL");
			iTween.StopByName(this.m_theActualButton, "RecruitButtonSwing");
			this.m_theActualButton.transform.localScale = Vector3.one;
			this.m_theActualButton.transform.localRotation = Quaternion.identity;
			iTween.StopByName(this.m_numReadyTroopsTextBG, "RecruitNumSwing");
			this.m_numReadyTroopsTextBG.transform.localRotation = Quaternion.identity;
			if (this.m_glowHandle != null)
			{
				UiAnimation anim = this.m_glowHandle.GetAnim();
				if (anim != null)
				{
					anim.Stop(0.5f);
				}
			}
			if (this.m_glowLoopHandle != null)
			{
				UiAnimation anim2 = this.m_glowLoopHandle.GetAnim();
				if (anim2 != null)
				{
					anim2.Stop(0.5f);
				}
			}
		}

		private void Update()
		{
			int numCompletedMissions = PersistentMissionData.GetNumCompletedMissions(true);
			if (numCompletedMissions != this.m_numReadyTroops)
			{
				this.m_theActualButton.SetActive(numCompletedMissions > 0);
				if (numCompletedMissions == 0)
				{
					this.ClearEffects();
				}
				if (numCompletedMissions > this.m_numReadyTroops)
				{
					this.ClearEffects();
					this.m_glowHandle = UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", this.m_theActualButton.transform, Vector3.zero, 3f, 0f);
					this.m_glowLoopHandle = UiAnimMgr.instance.PlayAnim("MinimapLoopPulseAnim", this.m_theActualButton.transform, Vector3.zero, 3f, 0f);
					iTween.PunchScale(this.m_theActualButton, iTween.Hash(new object[]
					{
						"name",
						"RecruitWobble",
						"x",
						this.amount,
						"y",
						this.amount,
						"time",
						this.duration,
						"delay",
						0.1f,
						"looptype",
						iTween.LoopType.none
					}));
					iTween.PunchScale(this.m_theActualButton, iTween.Hash(new object[]
					{
						"name",
						"RecruitWobbleL",
						"x",
						this.amount,
						"y",
						this.amount,
						"time",
						this.duration,
						"delay",
						this.delay,
						"looptype",
						iTween.LoopType.loop
					}));
					iTween.PunchRotation(this.m_theActualButton, iTween.Hash(new object[]
					{
						"name",
						"RecruitButtonSwing",
						"z",
						-30f,
						"time",
						2f
					}));
					iTween.PunchRotation(this.m_numReadyTroopsTextBG, iTween.Hash(new object[]
					{
						"name",
						"RecruitNumSwing",
						"z",
						-50f,
						"time",
						3f
					}));
					Main.instance.m_UISound.Play_LootReady();
				}
				this.m_numReadyTroops = numCompletedMissions;
				this.m_numReadyTroopsText.text = string.Empty + ((this.m_numReadyTroops <= 0) ? string.Empty : (string.Empty + this.m_numReadyTroops));
				this.m_numReadyTroopsTextBG.SetActive(this.m_numReadyTroops > 0);
			}
		}

		public void ZoomToNextCompleteMission()
		{
			this.m_miniMissionListPanel.InitMissionList();
			MiniMissionListItem[] componentsInChildren = this.m_miniMissionListPanel.m_inProgressMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
			foreach (MiniMissionListItem miniMissionListItem in componentsInChildren)
			{
				if (PersistentMissionData.missionDictionary.ContainsKey(miniMissionListItem.GetMissionID()))
				{
					WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[miniMissionListItem.GetMissionID()];
					if (wrapperGarrisonMission.MissionState == 1)
					{
						TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
						if ((wrapperGarrisonMission.MissionDuration - t).TotalSeconds <= 0.0)
						{
							AdventureMapPanel.instance.ShowMissionResultAction(wrapperGarrisonMission.MissionRecID, 0, false);
							break;
						}
					}
				}
			}
		}

		public GameObject m_treasureChestAlliance;

		public GameObject m_treasureChestHorde;

		public GameObject m_numReadyTroopsTextBG;

		public Text m_numReadyTroopsText;

		public GameObject m_theActualButton;

		public MiniMissionListPanel m_miniMissionListPanel;

		public float amount = 0.1f;

		public float duration = 1.2f;

		public float delay = 1.8f;

		public int m_numReadyTroops;

		private UiAnimMgr.UiAnimHandle m_glowHandle;

		private UiAnimMgr.UiAnimHandle m_glowLoopHandle;
	}
}
