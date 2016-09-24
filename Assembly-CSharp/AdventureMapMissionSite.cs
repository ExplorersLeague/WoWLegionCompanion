using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class AdventureMapMissionSite : MonoBehaviour
{
	private void OnEnable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.TestIconSizeChanged = (Action<float>)Delegate.Combine(instance.TestIconSizeChanged, new Action<float>(this.OnTestIconSizeChanged));
		Main instance2 = Main.instance;
		instance2.ClaimMissionBonusResultAction = (Action<int, bool, int>)Delegate.Combine(instance2.ClaimMissionBonusResultAction, new Action<int, bool, int>(this.HandleClaimMissionBonusResult));
		Main instance3 = Main.instance;
		instance3.CompleteMissionResultAction = (Action<int, bool>)Delegate.Combine(instance3.CompleteMissionResultAction, new Action<int, bool>(this.HandleCompleteMissionResult));
		PinchZoomContentManager pinchZoomContentManager = AdventureMapPanel.instance.m_pinchZoomContentManager;
		pinchZoomContentManager.ZoomFactorChanged = (Action)Delegate.Combine(pinchZoomContentManager.ZoomFactorChanged, new Action(this.HandleZoomChanged));
	}

	private void OnDisable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.TestIconSizeChanged = (Action<float>)Delegate.Remove(instance.TestIconSizeChanged, new Action<float>(this.OnTestIconSizeChanged));
		Main instance2 = Main.instance;
		instance2.ClaimMissionBonusResultAction = (Action<int, bool, int>)Delegate.Remove(instance2.ClaimMissionBonusResultAction, new Action<int, bool, int>(this.HandleClaimMissionBonusResult));
		Main instance3 = Main.instance;
		instance3.CompleteMissionResultAction = (Action<int, bool>)Delegate.Remove(instance3.CompleteMissionResultAction, new Action<int, bool>(this.HandleCompleteMissionResult));
		PinchZoomContentManager pinchZoomContentManager = AdventureMapPanel.instance.m_pinchZoomContentManager;
		pinchZoomContentManager.ZoomFactorChanged = (Action)Delegate.Remove(pinchZoomContentManager.ZoomFactorChanged, new Action(this.HandleZoomChanged));
	}

	private void OnTestIconSizeChanged(float newScale)
	{
		base.transform.localScale = Vector3.one * newScale;
	}

	private void HandleZoomChanged()
	{
		this.m_zoomScaleRoot.sizeDelta = this.m_myRT.sizeDelta * AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor;
	}

	private void Awake()
	{
		this.m_selectionRing.gameObject.SetActive(false);
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.MissionMapSelectionChangedAction = (Action<int>)Delegate.Combine(instance.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
		this.m_missionCompleteText.text = StaticDB.GetString("MISSION_COMPLETE", null);
		this.m_isStackablePreview = false;
		this.m_missionTimeRemaining = new Duration(0, false);
	}

	private void Update()
	{
		this.UpdateMissionRemainingTimeDisplay();
	}

	private void UpdateMissionRemainingTimeDisplay()
	{
		if (!this.m_inProgressMissionGroup.gameObject.activeSelf)
		{
			return;
		}
		if (this.m_missionSiteGroup != null && this.m_missionSiteGroup.alpha < 0.1f)
		{
			return;
		}
		long num = GarrisonStatus.CurrentTime() - this.m_missionStartedTime;
		long num2 = (long)this.m_missionDurationInSeconds - num;
		num2 = ((num2 <= 0L) ? 0L : num2);
		if (!this.m_isSupportMission)
		{
			this.m_missionTimeRemaining.FormatDurationString((int)num2, false);
			this.m_missionTimeRemainingText.text = this.m_missionTimeRemaining.DurationString;
		}
		if (num2 == 0L)
		{
			if (this.m_isSupportMission)
			{
				if (!this.m_autoCompletedSupportMission)
				{
					if (AdventureMapPanel.instance.ShowMissionResultAction != null)
					{
						AdventureMapPanel.instance.ShowMissionResultAction(this.m_garrMissionID, 1, false);
					}
					Main.instance.CompleteMission(this.m_garrMissionID);
					this.m_autoCompletedSupportMission = true;
				}
			}
			else
			{
				this.m_availableMissionGroup.gameObject.SetActive(false);
				this.m_inProgressMissionGroup.gameObject.SetActive(false);
				this.m_completeMissionGroup.gameObject.SetActive(true);
			}
		}
	}

	public void SetMission(int garrMissionID)
	{
		base.gameObject.name = "AdvMapMissionSite " + garrMissionID;
		if (!PersistentMissionData.missionDictionary.ContainsKey(garrMissionID))
		{
			return;
		}
		this.m_garrMissionID = garrMissionID;
		GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
		if (record == null || !PersistentMissionData.missionDictionary.ContainsKey(garrMissionID))
		{
			return;
		}
		this.m_areaID = record.AreaID;
		this.m_isSupportMission = false;
		if ((record.Flags & 16u) != 0u)
		{
			this.m_isSupportMission = true;
			this.m_missionTimeRemainingText.text = "Fortified";
		}
		GarrMissionTypeRec record2 = StaticDB.garrMissionTypeDB.GetRecord((int)record.GarrMissionTypeID);
		if (record2.UiTextureAtlasMemberID > 0u)
		{
			Sprite atlasSprite = TextureAtlas.instance.GetAtlasSprite((int)record2.UiTextureAtlasMemberID);
			if (atlasSprite != null)
			{
				this.m_availableMissionTypeIcon.sprite = atlasSprite;
				this.m_inProgressMissionTypeIcon.sprite = atlasSprite;
			}
		}
		JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[garrMissionID];
		if (jamGarrisonMobileMission.MissionState == 1 || jamGarrisonMobileMission.MissionState == 2)
		{
			this.m_missionDurationInSeconds = (int)jamGarrisonMobileMission.MissionDuration;
		}
		else
		{
			this.m_missionDurationInSeconds = record.MissionDuration;
		}
		this.m_missionStartedTime = jamGarrisonMobileMission.StartTime;
		this.m_availableMissionGroup.gameObject.SetActive(jamGarrisonMobileMission.MissionState == 0);
		this.m_inProgressMissionGroup.gameObject.SetActive(jamGarrisonMobileMission.MissionState == 1);
		this.m_completeMissionGroup.gameObject.SetActive(jamGarrisonMobileMission.MissionState == 2 || jamGarrisonMobileMission.MissionState == 3);
		if (jamGarrisonMobileMission.MissionState == 1)
		{
			foreach (KeyValuePair<int, JamGarrisonFollower> keyValuePair in PersistentFollowerData.followerDictionary)
			{
				if (keyValuePair.Value.CurrentMissionID == garrMissionID)
				{
					GarrFollowerRec record3 = StaticDB.garrFollowerDB.GetRecord(keyValuePair.Value.GarrFollowerID);
					if (record3 != null)
					{
						Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.PortraitIcons, (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record3.AllianceIconFileDataID : record3.HordeIconFileDataID);
						if (sprite != null)
						{
							this.m_followerPortraitImage.sprite = sprite;
						}
						this.m_followerPortraitRingImage.GetComponent<Image>().enabled = true;
						break;
					}
				}
			}
		}
		this.m_missionLevelText.text = string.Empty + record.TargetLevel + ((record.TargetLevel != 110) ? string.Empty : (" (" + record.TargetItemLevel + ")"));
		this.UpdateMissionRemainingTimeDisplay();
	}

	public void HandleMissionChanged(int newMissionID)
	{
		if (this.m_isStackablePreview || this.m_garrMissionID == 0)
		{
			return;
		}
		if (this.m_selectedEffectAnimHandle != null)
		{
			UiAnimation anim = this.m_selectedEffectAnimHandle.GetAnim();
			if (anim != null)
			{
				anim.Stop(0.5f);
			}
		}
		if (newMissionID == this.m_garrMissionID)
		{
			this.m_selectedEffectAnimHandle = UiAnimMgr.instance.PlayAnim("MinimapLoopPulseAnim", this.m_selectedEffectRoot, Vector3.zero, 2.5f, 0f);
		}
		if (this.m_selectionRing != null)
		{
			this.m_selectionRing.gameObject.SetActive(newMissionID == this.m_garrMissionID);
		}
	}

	public void OnTapAvailableMission()
	{
		AdventureMapPanel.instance.SelectMissionFromMap(this.m_garrMissionID);
		this.JustZoomToMission();
	}

	public void JustZoomToMission()
	{
		UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 3f, 0f);
		Main.instance.m_UISound.Play_SelectMission();
		if (StaticDB.garrMissionDB.GetRecord(this.m_garrMissionID) == null)
		{
			return;
		}
		AdventureMapPanel instance = AdventureMapPanel.instance;
		StackableMapIcon component = base.GetComponent<StackableMapIcon>();
		StackableMapIconContainer stackableMapIconContainer = null;
		if (component != null)
		{
			stackableMapIconContainer = component.GetContainer();
			AdventureMapPanel.instance.SetSelectedIconContainer(stackableMapIconContainer);
		}
		Vector2 tapPos;
		if (stackableMapIconContainer != null)
		{
			tapPos..ctor(stackableMapIconContainer.transform.position.x, stackableMapIconContainer.transform.position.y);
		}
		else
		{
			tapPos..ctor(base.transform.position.x, base.transform.position.y);
		}
		instance.CenterAndZoom(tapPos, null, true);
	}

	public void OnTapCompletedMission()
	{
		UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 3f, 0f);
		Main.instance.m_UISound.Play_SelectMission();
		if (AdventureMapPanel.instance.ShowMissionResultAction != null)
		{
			AdventureMapPanel.instance.ShowMissionResultAction(this.m_garrMissionID, 1, false);
		}
		Main.instance.CompleteMission(this.m_garrMissionID);
	}

	public void HandleCompleteMissionResult(int garrMissionID, bool missionSucceeded)
	{
		if (garrMissionID == this.m_garrMissionID)
		{
			this.OnMissionStatusChanged(false, missionSucceeded);
		}
	}

	public void HandleClaimMissionBonusResult(int garrMissionID, bool awardOvermax, int result)
	{
		if (garrMissionID == this.m_garrMissionID)
		{
			if (result == 0)
			{
				this.OnMissionStatusChanged(awardOvermax, true);
			}
			else
			{
				Debug.LogWarning("CLAIM MISSION FAILED! Result = " + (GARRISON_RESULT)result);
			}
		}
	}

	public void OnMissionStatusChanged(bool awardOvermax, bool missionSucceeded)
	{
		JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[this.m_garrMissionID];
		if (jamGarrisonMobileMission.MissionState == 6 && !missionSucceeded)
		{
			Debug.Log("OnMissionStatusChanged() MISSION FAILED " + this.m_garrMissionID);
			this.m_claimedMyLoot = true;
			this.ShowMissionFailure();
			return;
		}
		if (!this.m_claimedMyLoot)
		{
			if (jamGarrisonMobileMission.MissionState == 2 || jamGarrisonMobileMission.MissionState == 3)
			{
				Main.instance.ClaimMissionBonus(this.m_garrMissionID);
				this.m_claimedMyLoot = true;
			}
			return;
		}
		if (!this.m_showedMyLoot)
		{
			this.ShowMissionSuccess(awardOvermax);
			this.m_showedMyLoot = true;
		}
	}

	private void ShowMissionFailure()
	{
		if (AdventureMapPanel.instance.ShowMissionResultAction != null)
		{
			AdventureMapPanel.instance.ShowMissionResultAction(this.m_garrMissionID, 3, false);
		}
		Object.Destroy(base.gameObject);
	}

	private void ShowMissionSuccess(bool awardOvermax)
	{
		if (AdventureMapPanel.instance.ShowMissionResultAction != null)
		{
			AdventureMapPanel.instance.ShowMissionResultAction(this.m_garrMissionID, 2, awardOvermax);
		}
		Object.Destroy(base.gameObject);
	}

	public void ShowInProgressMissionDetails()
	{
		Main.instance.m_UISound.Play_SelectWorldQuest();
		if (AdventureMapPanel.instance.ShowMissionResultAction != null)
		{
			AdventureMapPanel.instance.ShowMissionResultAction(this.m_garrMissionID, 0, false);
		}
	}

	public void SetPreviewMode(bool isPreview)
	{
		this.m_isStackablePreview = isPreview;
		foreach (GameObject gameObject in this.m_stuffToHideInPreviewMode)
		{
			gameObject.SetActive(!isPreview);
		}
	}

	public Image m_errorImage;

	public CanvasGroup m_availableMissionGroup;

	public CanvasGroup m_inProgressMissionGroup;

	public CanvasGroup m_completeMissionGroup;

	public Image m_availableMissionTypeIcon;

	public Image m_inProgressMissionTypeIcon;

	public Text m_missingMissionTypeIconErrorText;

	public Text m_missionLevelText;

	public Text m_missionTimeRemainingText;

	public Image m_followerPortraitRingImage;

	public Image m_followerPortraitImage;

	public CanvasGroup m_missionSiteGroup;

	public RectTransform m_myRT;

	public int m_areaID;

	public Transform m_selectedEffectRoot;

	public Image m_selectionRing;

	public RectTransform m_zoomScaleRoot;

	public bool m_isStackablePreview;

	public GameObject[] m_stuffToHideInPreviewMode;

	private int m_garrMissionID;

	private int m_missionDurationInSeconds;

	private long m_missionStartedTime;

	private bool m_claimedMyLoot;

	private bool m_showedMyLoot;

	private bool m_isSupportMission;

	private bool m_autoCompletedSupportMission;

	public Text m_missionCompleteText;

	private UiAnimMgr.UiAnimHandle m_selectedEffectAnimHandle;

	private Duration m_missionTimeRemaining;
}
