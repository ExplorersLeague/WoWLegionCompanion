using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionFollowerSlot : MonoBehaviour
	{
		private void Awake()
		{
			this.ClearFollower();
		}

		private void Start()
		{
			if (this.m_missionDetailView != null)
			{
				MissionDetailView missionDetailView = this.m_missionDetailView;
				missionDetailView.FollowerSlotsChangedAction = (Action)Delegate.Combine(missionDetailView.FollowerSlotsChangedAction, new Action(this.InitHeartPanel));
			}
		}

		private void OnDestroy()
		{
			if (this.m_missionDetailView != null)
			{
				MissionDetailView missionDetailView = this.m_missionDetailView;
				missionDetailView.FollowerSlotsChangedAction = (Action)Delegate.Remove(missionDetailView.FollowerSlotsChangedAction, new Action(this.InitHeartPanel));
			}
		}

		private void Update()
		{
		}

		public bool IsOccupied()
		{
			return this.isOccupied;
		}

		public int GetCurrentGarrFollowerID()
		{
			return this.currentGarrFollowerID;
		}

		public void OnTapCombatAllySlot()
		{
			if (this.m_missionDetailView != null && this.m_missionDetailView.GetCombatAllyMissionState() == CombatAllyMissionState.available)
			{
				this.ClearFollower();
			}
		}

		public void OnTapFollowerSlot()
		{
			if (this.m_missionDetailView != null)
			{
				this.ClearFollower();
				this.PlayUnslotSound();
			}
		}

		public void ClearFollower()
		{
			if (this.currentGarrFollowerID != 0)
			{
				GameObject gameObject = base.transform.parent.parent.parent.parent.gameObject;
				gameObject.BroadcastMessage("RemoveFromParty", this.currentGarrFollowerID, 1);
			}
			this.SetFollower(0);
			if (this.m_disableButtonWhenFollowerAssigned)
			{
				this.m_portraitFrameImage.GetComponent<Image>().enabled = true;
			}
		}

		public void SetFollower(int garrFollowerID)
		{
			this.m_garrFollowerRec = null;
			if (this.m_abilityAreaRootObject != null)
			{
				RectTransform[] componentsInChildren = this.m_abilityAreaRootObject.GetComponentsInChildren<RectTransform>(true);
				if (componentsInChildren != null)
				{
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i] != null && componentsInChildren[i] != this.m_abilityAreaRootObject.transform)
						{
							componentsInChildren[i].gameObject.transform.SetParent(null);
							Object.Destroy(componentsInChildren[i].gameObject);
						}
					}
				}
			}
			if (garrFollowerID == 0)
			{
				if (this.m_portraitRingImage != null)
				{
					this.m_portraitRingImage.gameObject.SetActive(false);
				}
				if (this.m_heartPanel != null)
				{
					this.m_heartPanel.SetActive(false);
				}
				this.m_levelBorderImage.gameObject.SetActive(false);
				this.m_levelBorderImage_TitleQuality.gameObject.SetActive(false);
				this.m_portraitImage.gameObject.SetActive(false);
				this.m_qualityColorImage.gameObject.SetActive(false);
				this.m_levelText.gameObject.SetActive(false);
				this.isOccupied = false;
				this.m_portraitFrameImage.enabled = true;
				if (this.currentGarrFollowerID != garrFollowerID)
				{
					AdventureMapPanel.instance.MissionFollowerSlotChanged(this.currentGarrFollowerID, false);
				}
				bool flag = this.currentGarrFollowerID != 0;
				this.currentGarrFollowerID = 0;
				if (flag && this.m_missionDetailView != null)
				{
					this.m_missionDetailView.UpdateMissionStatus();
				}
				if (this.m_disableButtonWhenFollowerAssigned)
				{
					this.m_portraitFrameImage.GetComponent<Image>().enabled = true;
				}
				if (this.m_missionDetailView != null)
				{
					this.m_missionDetailView.NotifyFollowerSlotsChanged();
				}
				return;
			}
			this.m_portraitRingImage.gameObject.SetActive(true);
			this.m_levelBorderImage.gameObject.SetActive(true);
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(garrFollowerID);
			if (record == null)
			{
				return;
			}
			if (record.GarrFollowerTypeID != (uint)GarrisonStatus.GarrisonFollowerType)
			{
				return;
			}
			MissionMechanic[] mechanics = base.gameObject.transform.parent.parent.parent.gameObject.GetComponentsInChildren<MissionMechanic>(true);
			if (mechanics == null)
			{
				return;
			}
			WrapperGarrisonFollower follower = PersistentFollowerData.followerDictionary[garrFollowerID];
			float num = 0f;
			if (this.m_missionDetailView != null)
			{
				num = MissionDetailView.ComputeFollowerBias(follower, follower.FollowerLevel, (follower.ItemLevelWeapon + follower.ItemLevelArmor) / 2, this.m_missionDetailView.GetCurrentMissionID());
			}
			if (num == -1f)
			{
				this.m_levelText.color = Color.red;
			}
			else if (num < 0f)
			{
				this.m_levelText.color = new Color(0.9333f, 0.4392f, 0.2117f);
			}
			else
			{
				this.m_levelText.color = Color.white;
			}
			if (this.m_abilityAreaRootObject != null && num > -1f)
			{
				for (int j = 0; j < follower.AbilityIDs.Count; j++)
				{
					GarrAbilityRec record2 = StaticDB.garrAbilityDB.GetRecord(follower.AbilityIDs[j]);
					GarrAbilityEffectRec garrAbilityEffectRec2 = StaticDB.garrAbilityEffectDB.GetRecordsByParentID(record2.ID).FirstOrDefault(delegate(GarrAbilityEffectRec garrAbilityEffectRec)
					{
						if (garrAbilityEffectRec.GarrMechanicTypeID == 0u || garrAbilityEffectRec.AbilityAction != 0u)
						{
							return false;
						}
						GarrMechanicTypeRec garrMechanicTypeRec = StaticDB.garrMechanicTypeDB.GetRecord((int)garrAbilityEffectRec.GarrMechanicTypeID);
						return garrMechanicTypeRec != null && mechanics.Any((MissionMechanic mechanic) => mechanic.m_missionMechanicTypeID == garrMechanicTypeRec.ID);
					});
					if (garrAbilityEffectRec2 != null)
					{
						GameObject gameObject = Object.Instantiate<GameObject>(this.m_missionMechanicCounterPrefab, this.m_abilityAreaRootObject.transform, false);
						MissionMechanicTypeCounter component = gameObject.GetComponent<MissionMechanicTypeCounter>();
						component.usedIcon.gameObject.SetActive(false);
						Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record2.IconFileDataID);
						if (sprite != null)
						{
							component.missionMechanicIcon.sprite = sprite;
						}
						component.countersMissionMechanicTypeID = (int)garrAbilityEffectRec2.GarrMechanicTypeID;
					}
				}
			}
			this.m_levelText.gameObject.SetActive(true);
			this.m_levelText.text = follower.FollowerLevel.ToString();
			this.m_portraitImage.gameObject.SetActive(true);
			Sprite sprite2 = GeneralHelpers.LoadIconAsset(AssetBundleType.PortraitIcons, (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceIconFileDataID : record.HordeIconFileDataID);
			if (sprite2 != null)
			{
				this.m_portraitImage.sprite = sprite2;
			}
			if (follower.Quality == 6)
			{
				this.m_levelBorderImage_TitleQuality.gameObject.SetActive(true);
				this.m_qualityColorImage.gameObject.SetActive(false);
				this.m_levelBorderImage.gameObject.SetActive(false);
			}
			else
			{
				this.m_levelBorderImage_TitleQuality.gameObject.SetActive(false);
				this.m_qualityColorImage.gameObject.SetActive(true);
				this.m_levelBorderImage.gameObject.SetActive(true);
				Color qualityColor = GeneralHelpers.GetQualityColor(follower.Quality);
				this.m_qualityColorImage.color = qualityColor;
			}
			this.isOccupied = true;
			bool flag2 = (follower.Flags & 8) != 0;
			this.m_qualityColorImage.gameObject.SetActive(!flag2);
			this.m_levelBorderImage.gameObject.SetActive(!flag2);
			if (this.m_heartPanel != null)
			{
				this.m_heartPanel.SetActive(flag2);
				if (flag2)
				{
					this.m_garrFollowerRec = record;
					this.m_follower = follower;
				}
			}
			this.m_portraitFrameImage.enabled = !flag2;
			this.currentGarrFollowerID = garrFollowerID;
			if (this.m_disableButtonWhenFollowerAssigned)
			{
				this.m_portraitFrameImage.GetComponent<Image>().enabled = false;
			}
			if (this.m_missionDetailView != null)
			{
				this.m_missionDetailView.UpdateMissionStatus();
				this.m_missionDetailView.NotifyFollowerSlotsChanged();
			}
		}

		public void InitHeartPanel()
		{
			if (this.m_heartPanel == null || this.m_garrFollowerRec == null)
			{
				return;
			}
			Image[] componentsInChildren = this.m_heartArea.GetComponentsInChildren<Image>(true);
			foreach (Image image in componentsInChildren)
			{
				if (image != null && image.gameObject != this.m_heartArea)
				{
					image.transform.SetParent(null);
					Object.Destroy(image.gameObject);
				}
			}
			WrapperGarrisonFollower wrapperGarrisonFollower = this.m_follower;
			if (PersistentFollowerData.preMissionFollowerDictionary.ContainsKey(this.m_follower.GarrFollowerID))
			{
				wrapperGarrisonFollower = PersistentFollowerData.preMissionFollowerDictionary[this.m_follower.GarrFollowerID];
			}
			int num = 1;
			if (GeneralHelpers.MissionHasUncounteredDeadly(this.m_enemyPortraitsGroup))
			{
				num = wrapperGarrisonFollower.Durability;
			}
			for (int j = 0; j < wrapperGarrisonFollower.Durability - num; j++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_fullHeartPrefab);
				gameObject.transform.SetParent(this.m_heartArea.transform, false);
			}
			for (int k = 0; k < num; k++)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_outlineHeartPrefab);
				gameObject2.transform.SetParent(this.m_heartArea.transform, false);
			}
			for (int l = 0; l < this.m_garrFollowerRec.Vitality - wrapperGarrisonFollower.Durability; l++)
			{
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.m_emptyHeartPrefab);
				gameObject3.transform.SetParent(this.m_heartArea.transform, false);
			}
		}

		public void PlayUnslotSound()
		{
			Main.instance.m_UISound.Play_DefaultNavClick();
		}

		public bool CountersStealth()
		{
			return this.currentGarrFollowerID != 0 && PersistentFollowerData.followerDictionary.ContainsKey(this.currentGarrFollowerID) && PersistentFollowerData.followerDictionary[this.currentGarrFollowerID].AbilityIDs.Contains(1262);
		}

		public Image m_portraitFrameImage;

		public Image m_portraitImage;

		public Image m_qualityColorImage;

		public Image m_portraitRingImage;

		public Image m_levelBorderImage;

		public Image m_levelBorderImage_TitleQuality;

		public Text m_levelText;

		public GameObject m_abilityAreaRootObject;

		public MissionDetailView m_missionDetailView;

		public GameObject m_missionMechanicCounterPrefab;

		public bool m_disableButtonWhenFollowerAssigned;

		private GarrFollowerRec m_garrFollowerRec;

		private WrapperGarrisonFollower m_follower;

		public GameObject m_heartPanel;

		public GameObject m_heartArea;

		public GameObject m_enemyPortraitsGroup;

		public GameObject m_fullHeartPrefab;

		public GameObject m_emptyHeartPrefab;

		public GameObject m_outlineHeartPrefab;

		private bool isOccupied;

		private int currentGarrFollowerID;
	}
}
