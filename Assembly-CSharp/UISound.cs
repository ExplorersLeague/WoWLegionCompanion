using System;
using UnityEngine;

public class UISound : MonoBehaviour
{
	private void Awake()
	{
		this.m_oneShotAudioSources = new AudioSource[10];
		this.m_oneShotAudioSourceNames = new string[10];
		for (int i = 0; i < 10; i++)
		{
			this.m_oneShotAudioSourceNames[i] = "Unused";
			this.m_oneShotAudioSources[i] = base.gameObject.AddComponent<AudioSource>();
		}
	}

	public void EnableSFX(bool enable)
	{
		this.m_enableSFX = enable;
	}

	public bool IsSFXEnabled()
	{
		return this.m_enableSFX;
	}

	public void PlayUISound(string soundName, float volumeScale = 1f, int maxInstances = 3)
	{
		if (!Main.instance.m_UISound.IsSFXEnabled())
		{
			return;
		}
		if (maxInstances > 0)
		{
			int num = 0;
			for (int i = 0; i < 10; i++)
			{
				if (this.m_oneShotAudioSources[i].isPlaying && this.m_oneShotAudioSourceNames[i] == soundName)
				{
					num++;
					if (num >= maxInstances)
					{
						return;
					}
				}
			}
		}
		for (int j = 0; j < this.m_oneShotAudioSources.Length; j++)
		{
			if (!this.m_oneShotAudioSources[j].isPlaying)
			{
				AudioClip audioClip = Resources.Load<AudioClip>(soundName);
				this.m_oneShotAudioSources[j].PlayOneShot(audioClip, volumeScale);
				this.m_oneShotAudioSourceNames[j] = soundName;
				break;
			}
		}
	}

	public void Play_ButtonRedClick()
	{
		this.PlayUISound("SFX/UI_ButtonRed_Click", 1f, 3);
	}

	public void Play_ButtonBlackClick()
	{
		this.PlayUISound("SFX/UI_ButtonBlack_Click", 1f, 3);
	}

	public void Play_DefaultNavClick()
	{
		this.PlayUISound("SFX/UI_DefaultNav_Click", 1f, 3);
	}

	public void Play_OrderHallTalentSelect()
	{
		this.PlayUISound("SFX/UI_OrderHall_Talent_Select_V2", 1f, 3);
	}

	public void Play_MapZoomIn()
	{
		this.PlayUISound("SFX/UI_Mission_Map_Zoom_ALT", 0.3f, 3);
	}

	public void Play_SlotChampion()
	{
		this.PlayUISound("SFX/UI_Mission_SlotChampion", 1f, 3);
	}

	public void Play_SlotTroop()
	{
		this.PlayUISound("SFX/UI_Mission_SlotTroop", 0.6f, 3);
	}

	public void Play_100Percent()
	{
		this.PlayUISound("SFX/UI_Misison_100Percent", 1f, 3);
	}

	public void Play_200Percent()
	{
		this.PlayUISound("SFX/UI_Mission_200Percent", 1f, 3);
	}

	public void Play_IncreasePercent()
	{
		this.PlayUISound("SFX/UI_Mission_IncreasePercent", 1f, 3);
	}

	public void Play_LootReady()
	{
		this.PlayUISound("SFX/UI_Mission_Loot_Ready", 0.8f, 3);
	}

	public void Play_StartMission()
	{
		this.PlayUISound("SFX/UI_Mission_Start_V2", 1f, 3);
	}

	public void Play_MissionFailure()
	{
		this.PlayUISound("SFX/UI_Mission_Fail", 1f, 3);
	}

	public void Play_MissionSuccess()
	{
		this.PlayUISound("SFX/UI_Mission_Loot_Open_V2", 1f, 3);
	}

	public void Play_RecruitTroop()
	{
		this.PlayUISound("SFX/UI_Recruit_Troop", 1f, 3);
	}

	public void Play_TroopsReadyToast()
	{
		this.PlayUISound("SFX/UI_Mission_Troops_Ready_Toast", 0.8f, 1);
	}

	public void Play_CollectTroop()
	{
		this.PlayUISound("SFX/UI_Mobile_Collect_Troop", 1f, 3);
	}

	public void Play_GreenCheck()
	{
		this.PlayUISound("SFX/UI_Mission_GreenCheck", 1f, 3);
	}

	public void Play_SelectMission()
	{
		this.PlayUISound("SFX/UI_Mission_Select", 1f, 3);
	}

	public void Play_ActivateChampion()
	{
		this.PlayUISound("SFX/UI_Activate_Champion", 1f, 3);
	}

	public void Play_DeactivateChampion()
	{
		this.PlayUISound("SFX/UI_Deactivate_Champion", 1f, 3);
	}

	public void Play_ShowGenericTooltip()
	{
		this.PlayUISound("SFX/UI_InfoWindow_Popup", 0.25f, 3);
	}

	public void Play_BeginResearch()
	{
		this.PlayUISound("SFX/UI_OrderHall_Talent_BeginResearch", 1f, 3);
	}

	public void Play_TalentReadyCheck()
	{
		this.PlayUISound("SFX/UI_OrderHall_Talent_Ready_Check", 1f, 3);
	}

	public void Play_TalentReadyToast()
	{
		this.PlayUISound("SFX/UI_OrderHall_Talent_Ready_Toast", 1f, 3);
	}

	public void Play_SelectWorldQuest()
	{
		this.PlayUISound("SFX/UI_WorldQuest_Map_Select", 1f, 3);
	}

	public void Play_UpgradeEquipment()
	{
		this.PlayUISound("SFX/UI_Upgrade_Equipment_Slot", 0.8f, 3);
	}

	public void Play_UpgradeArmament()
	{
		this.PlayUISound("SFX/UI_Upgrade_Follower", 0.8f, 3);
	}

	public void Play_PlayerLevelUp()
	{
		this.PlayUISound("SFX/LevelUp", 1f, 3);
	}

	public void Play_RedFailX()
	{
		this.PlayUISound("SFX/UI_Mission_Fail_Red_X", 0.3f, 3);
	}

	public void Play_ChampionLevelUp()
	{
		this.PlayUISound("SFX/UI_Upgrade_Follower", 1f, 3);
	}

	public void Play_CloseButton()
	{
		this.PlayUISound("SFX/uEscapeScreenClose", 1f, 3);
	}

	public void Play_ContributeSuccess()
	{
		this.PlayUISound("SFX/UI_72_Buildings_Contribute_Resources", 1f, 3);
	}

	public void Play_GetItem()
	{
		this.PlayUISound("SFX/UI_72_Buildings_Get_Item", 1f, 3);
	}

	public void Play_ArtifactClick()
	{
		this.PlayUISound("SFX/UI_72_ArtifactNote_Click", 1f, 3);
	}

	private const int maxSounds = 10;

	public AudioSource[] m_oneShotAudioSources;

	public string[] m_oneShotAudioSourceNames;

	private bool m_enableSFX = true;
}
