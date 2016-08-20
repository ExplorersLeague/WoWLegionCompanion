using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class MiniMissionListItem : MonoBehaviour
{
	private void Awake()
	{
		this.m_missionName.font = GeneralHelpers.LoadFancyFont();
		this.m_missionLevel.font = GeneralHelpers.LoadStandardFont();
		this.m_missionTime.font = GeneralHelpers.LoadStandardFont();
		this.m_rareMissionLabel.font = GeneralHelpers.LoadFancyFont();
		this.m_statusText.font = GeneralHelpers.LoadStandardFont();
		this.m_rareMissionLabel.text = StaticDB.GetString("RARE", "Rare!");
	}

	public void SetMission(JamGarrisonMobileMission mission)
	{
		this.m_statusDarkener.gameObject.SetActive(false);
		this.m_statusText.gameObject.SetActive(false);
		this.m_mission = mission;
		GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(mission.MissionRecID);
		if (record == null)
		{
			return;
		}
		if (this.m_missionTypeIcon != null)
		{
			GarrMissionTypeRec record2 = StaticDB.garrMissionTypeDB.GetRecord((int)record.GarrMissionTypeID);
			this.m_missionTypeIcon.sprite = TextureAtlas.instance.GetAtlasSprite((int)record2.UiTextureAtlasMemberID);
		}
		bool flag = false;
		if (mission.MissionState == 1)
		{
			flag = true;
			this.m_statusDarkener.gameObject.SetActive(true);
			this.m_statusDarkener.color = new Color(0f, 0f, 0f, 0.3529412f);
			this.m_statusText.gameObject.SetActive(true);
			this.m_missionTime.gameObject.SetActive(false);
		}
		this.m_previewMechanicsGroup.SetActive(!flag);
		Duration duration = new Duration(record.MissionDuration);
		string str;
		if (duration.DurationValue >= 28800)
		{
			str = "<color=#ff8600ff>" + duration.DurationString + "</color>";
		}
		else
		{
			str = "<color=#BEBEBEFF>" + duration.DurationString + "</color>";
		}
		this.m_missionTime.text = "(" + str + ")";
		this.m_missionName.text = record.Name;
		if (this.m_missionLevel != null)
		{
			if (record.TargetLevel < 110)
			{
				this.m_missionLevel.text = string.Empty + record.TargetLevel;
			}
			else
			{
				this.m_missionLevel.text = string.Concat(new object[]
				{
					string.Empty,
					record.TargetLevel,
					"\n(",
					record.TargetItemLevel,
					")"
				});
			}
		}
		bool flag2 = (record.Flags & 1u) != 0u;
		this.m_rareMissionLabel.gameObject.SetActive(flag2);
		this.m_rareMissionHighlight.gameObject.SetActive(flag2);
		if (flag2)
		{
			this.m_missionTypeBG.color = new Color(0f, 0f, 1f, 0.24f);
		}
		else
		{
			this.m_missionTypeBG.color = new Color(0f, 0f, 0f, 0.478f);
		}
		this.m_missionLocation.enabled = false;
		UiTextureKitRec record3 = StaticDB.uiTextureKitDB.GetRecord((int)record.UiTextureKitID);
		if (record3 != null)
		{
			int uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID(record3.KitPrefix + "-List");
			if (uitextureAtlasMemberID > 0)
			{
				Sprite atlasSprite = TextureAtlas.instance.GetAtlasSprite(uitextureAtlasMemberID);
				if (atlasSprite != null)
				{
					this.m_missionLocation.enabled = true;
					this.m_missionLocation.sprite = atlasSprite;
				}
			}
		}
		this.UpdateMechanicPreview(flag, mission);
		MissionRewardDisplay[] componentsInChildren = this.m_previewLootGroup.GetComponentsInChildren<MissionRewardDisplay>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i] != null)
			{
				Object.DestroyImmediate(componentsInChildren[i].gameObject);
			}
		}
		MissionRewardDisplay.InitMissionRewards(this.m_missionRewardDisplayPrefab.gameObject, this.m_previewLootGroup.transform, mission.Reward);
	}

	public void UpdateMechanicPreview(bool missionInProgress, JamGarrisonMobileMission mission)
	{
		if (this.m_previewMechanicsGroup != null)
		{
			AbilityDisplay[] componentsInChildren = this.m_previewMechanicsGroup.GetComponentsInChildren<AbilityDisplay>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] != null)
				{
					Object.DestroyImmediate(componentsInChildren[i].gameObject);
				}
			}
		}
		if (!missionInProgress)
		{
			for (int j = 0; j < mission.Encounter.Length; j++)
			{
				int id = (mission.Encounter[j].MechanicID.Length <= 0) ? 0 : mission.Encounter[j].MechanicID[0];
				GarrMechanicRec record = StaticDB.garrMechanicDB.GetRecord(id);
				if (record != null && record.GarrAbilityID != 0)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_previewMechanicEffectPrefab);
					gameObject.transform.SetParent(this.m_previewMechanicsGroup.transform, false);
					AbilityDisplay component = gameObject.GetComponent<AbilityDisplay>();
					component.SetAbility(record.GarrAbilityID, false, false, null);
					FollowerCanCounterMechanic canCounterStatus = GeneralHelpers.HasFollowerWhoCanCounter((int)record.GarrMechanicTypeID);
					component.SetCanCounterStatus(canCounterStatus);
				}
			}
		}
	}

	public void OnTap()
	{
		this.PlayClickSound();
		AllPopups.instance.HideAllPopups();
		if (this.m_mission.MissionState == 1)
		{
			if (AdventureMapPanel.instance.ShowMissionResultAction != null)
			{
				AdventureMapPanel.instance.ShowMissionResultAction(this.m_mission.MissionRecID, 0, false);
			}
			return;
		}
		if (this.m_mission.MissionState == 0)
		{
			AdventureMapPanel.instance.SelectMissionFromList(this.m_mission.MissionRecID);
			return;
		}
	}

	private void Update()
	{
		if (this.m_mission.MissionState == 1)
		{
			long num = GarrisonStatus.CurrentTime() - this.m_mission.StartTime;
			long num2 = this.m_mission.MissionDuration - num;
			num2 = ((num2 <= 0L) ? 0L : num2);
			Duration duration = new Duration((int)num2);
			if (num2 > 0L)
			{
				this.m_statusText.text = duration.DurationString + " <color=#ff0000ff>(" + StaticDB.GetString("IN_PROGRESS", null) + ")</color>";
			}
			else
			{
				this.m_statusText.text = "<color=#00ff00ff>(" + StaticDB.GetString("TAP_TO_COMPLETE", null) + ")</color>";
			}
		}
	}

	public void PlayClickSound()
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
	}

	public int GetMissionID()
	{
		return (this.m_mission != null) ? this.m_mission.MissionRecID : 0;
	}

	public Text m_missionName;

	public Image m_missionTypeIcon;

	public Image m_rareMissionHighlight;

	public Image m_missionTypeBG;

	public Image m_missionLocation;

	public Image m_statusDarkener;

	public Text m_missionLevel;

	public Text m_missionTime;

	public Text m_rareMissionLabel;

	public Text m_statusText;

	public MissionRewardDisplay m_missionRewardDisplayPrefab;

	public GameObject m_previewLootGroup;

	public GameObject m_previewMechanicEffectPrefab;

	public GameObject m_previewMechanicsGroup;

	private JamGarrisonMobileMission m_mission;
}
