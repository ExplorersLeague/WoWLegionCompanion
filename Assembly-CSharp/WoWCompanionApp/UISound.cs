using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WoWCompanionApp
{
	public class UISound : MonoBehaviour
	{
		private void Awake()
		{
			this.m_oneShotAudioSources = new AudioSource[10];
			for (int i = 0; i < 10; i++)
			{
				this.m_oneShotAudioSources[i] = base.gameObject.AddComponent<AudioSource>();
			}
			this.m_audioConfigs = this.m_soundConfig.clips.ToDictionary((SoundConfig.AudioClipConfig clip) => clip.name);
		}

		public void EnableSFX(bool enable)
		{
			this.m_enableSFX = enable;
		}

		public bool IsSFXEnabled()
		{
			return this.m_enableSFX;
		}

		public void PlayUISound(string soundName)
		{
			if (!this.IsSFXEnabled())
			{
				return;
			}
			if (!this.m_audioConfigs.ContainsKey(soundName))
			{
				Debug.LogError("No config saved for audio clip: " + soundName);
				return;
			}
			SoundConfig.AudioClipConfig config = this.m_audioConfigs[soundName];
			if (this.m_oneShotAudioSources.Count((AudioSource source) => source.isPlaying && source.clip == config.clip) >= config.maxInstances)
			{
				return;
			}
			AudioSource audioSource = this.m_oneShotAudioSources.FirstOrDefault((AudioSource source) => !source.isPlaying);
			if (audioSource != null)
			{
				audioSource.PlayOneShot(config.clip, config.volume);
			}
		}

		public void Play_ButtonRedClick()
		{
			this.PlayUISound("ButtonRedClick");
		}

		public void Play_ButtonBlackClick()
		{
			this.PlayUISound("ButtonBlackClick");
		}

		public void Play_DefaultNavClick()
		{
			this.PlayUISound("DefaultNavClick");
		}

		public void Play_OrderHallTalentSelect()
		{
			this.PlayUISound("OrderHallTalentSelect");
		}

		public void Play_MapZoomIn()
		{
			this.PlayUISound("MapZoomIn");
		}

		public void Play_SlotChampion()
		{
			this.PlayUISound("SlotChampion");
		}

		public void Play_SlotTroop()
		{
			this.PlayUISound("SlotTroop");
		}

		public void Play_100Percent()
		{
			this.PlayUISound("100Percent");
		}

		public void Play_200Percent()
		{
			this.PlayUISound("200Percent");
		}

		public void Play_IncreasePercent()
		{
			this.PlayUISound("IncreasePercent");
		}

		public void Play_LootReady()
		{
			this.PlayUISound("LootReady");
		}

		public void Play_StartMission()
		{
			this.PlayUISound("StartMission");
		}

		public void Play_MissionFailure()
		{
			this.PlayUISound("MissionFailure");
		}

		public void Play_MissionSuccess()
		{
			this.PlayUISound("MissionSuccess");
		}

		public void Play_RecruitTroop()
		{
			this.PlayUISound("RecruitTroop");
		}

		public void Play_TroopsReadyToast()
		{
			this.PlayUISound("TroopsReadyToast");
		}

		public void Play_CollectTroop()
		{
			this.PlayUISound("CollectTroop");
		}

		public void Play_GreenCheck()
		{
			this.PlayUISound("GreenCheck");
		}

		public void Play_SelectMission()
		{
			this.PlayUISound("SelectMission");
		}

		public void Play_ActivateChampion()
		{
			this.PlayUISound("ActivateChampion");
		}

		public void Play_DeactivateChampion()
		{
			this.PlayUISound("DeactivateChampion");
		}

		public void Play_ShowGenericTooltip()
		{
			this.PlayUISound("ShowGenericTooltip");
		}

		public void Play_BeginResearch()
		{
			this.PlayUISound("BeginResearch");
		}

		public void Play_TalentReadyCheck()
		{
			this.PlayUISound("TalentReadyCheck");
		}

		public void Play_TalentReadyToast()
		{
			this.PlayUISound("TalentReadyToast");
		}

		public void Play_SelectWorldQuest()
		{
			this.PlayUISound("SelectWorldQuest");
		}

		public void Play_UpgradeEquipment()
		{
			this.PlayUISound("UpgradeEquipment");
		}

		public void Play_UpgradeArmament()
		{
			this.PlayUISound("UpgradeArmament");
		}

		public void Play_PlayerLevelUp()
		{
			this.PlayUISound("PlayerLevelUp");
		}

		public void Play_RedFailX()
		{
			this.PlayUISound("RedFailX");
		}

		public void Play_ChampionLevelUp()
		{
			this.PlayUISound("ChampionLevelUp");
		}

		public void Play_CloseButton()
		{
			this.PlayUISound("CloseButton");
		}

		public void Play_ContributeSuccess()
		{
			this.PlayUISound("ContributeSuccess");
		}

		public void Play_GetItem()
		{
			this.PlayUISound("GetItem");
		}

		public void Play_ArtifactClick()
		{
			this.PlayUISound("ArtifactClick");
		}

		public void Play_RewardChestClick()
		{
			this.PlayUISound("RewardChestClick");
		}

		public void Play_MapZoomOut()
		{
			this.PlayUISound("MapZoomOut");
		}

		public SoundConfig m_soundConfig;

		public AudioSource[] m_oneShotAudioSources;

		private const int maxSounds = 10;

		private bool m_enableSFX = true;

		private Dictionary<string, SoundConfig.AudioClipConfig> m_audioConfigs;
	}
}
