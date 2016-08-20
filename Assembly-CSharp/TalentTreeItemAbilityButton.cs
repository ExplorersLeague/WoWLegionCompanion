using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobilePlayerJSON;
using WowStatConstants;
using WowStaticData;

public class TalentTreeItemAbilityButton : MonoBehaviour
{
	private void Awake()
	{
		this.m_playedTalentToast = false;
		this.m_shouldShowCheckAnim = false;
		this.m_playedShowCheckAnim = false;
		this.m_inactiveColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		this.m_canResearch = false;
		this.m_greenFrameGlow.gameObject.SetActive(false);
		this.m_yellowFrameGlow.gameObject.SetActive(false);
		this.m_researchProgressBG.gameObject.SetActive(false);
		this.m_visualState = TalentVisualState.cannotResearch;
	}

	public void HandleGarrisonDataResetFinished()
	{
		this.m_requestedUpdate = false;
	}

	public void HandleCanResearchGarrisonTalentResult(int garrTalentID, int result, string whyCantResearch)
	{
		if (this.m_garrTalentID != garrTalentID)
		{
			return;
		}
		if (PersistentTalentData.talentDictionary.ContainsKey(garrTalentID))
		{
			this.m_talent = (JamGarrisonTalent)PersistentTalentData.talentDictionary[garrTalentID];
		}
		this.m_garrTalentRec = StaticDB.garrTalentDB.GetRecord(garrTalentID);
		this.m_canResearch = false;
		if (result == 0)
		{
			this.m_canResearch = true;
		}
		else
		{
			this.m_whyCantResearch = whyCantResearch;
		}
	}

	public void SetTalent(int garrTalentID)
	{
		this.m_garrTalentID = garrTalentID;
		this.m_talent = null;
		if (PersistentTalentData.talentDictionary.ContainsKey(garrTalentID))
		{
			this.m_talent = (JamGarrisonTalent)PersistentTalentData.talentDictionary[garrTalentID];
		}
		this.m_garrTalentRec = StaticDB.garrTalentDB.GetRecord(garrTalentID);
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, this.m_garrTalentRec.IconFileDataID);
		if (sprite != null)
		{
			this.m_missingIconText.gameObject.SetActive(false);
			this.m_abilityIcon.sprite = sprite;
		}
		else
		{
			this.m_missingIconText.gameObject.SetActive(true);
			this.m_missingIconText.text = string.Empty + this.m_garrTalentRec.IconFileDataID;
		}
		Material material = new Material(this.m_grayscaleShader);
		this.m_abilityIcon.material = material;
		this.m_abilityIcon.material.SetFloat("_GrayscaleAmount", 1f);
	}

	public long GetRemainingResearchTime()
	{
		return (long)this.m_garrTalentRec.ResearchDurationSecs - (GarrisonStatus.CurrentTime() - (long)this.m_talent.StartTime);
	}

	public long GetRemainingRespecTime()
	{
		return (long)this.m_garrTalentRec.RespecDurationSecs - (GarrisonStatus.CurrentTime() - (long)this.m_talent.StartTime);
	}

	public bool IsOwned()
	{
		return this.m_talent != null && (this.m_talent.Flags & 1) != 0;
	}

	public bool IsRespec()
	{
		return this.m_talent != null && (this.m_talent.Flags & 2) != 0;
	}

	public bool IsResearching()
	{
		return this.m_talent != null && !this.IsOwned() && this.m_talent.StartTime > 0;
	}

	public bool IsSet()
	{
		return this.m_garrTalentRec != null;
	}

	public int GetTalentID()
	{
		return this.m_garrTalentID;
	}

	public bool CanResearch()
	{
		return this.m_canResearch;
	}

	public bool CanRespec()
	{
		return this.m_visualState == TalentVisualState.canRespec;
	}

	public string GetWhyCantResearch()
	{
		return this.m_whyCantResearch;
	}

	private void Update()
	{
		if (this.IsResearching())
		{
			this.m_researchProgressBG.gameObject.SetActive(true);
			float num = 1f - ((!this.IsRespec()) ? ((float)this.GetRemainingResearchTime() / (float)this.m_garrTalentRec.ResearchDurationSecs) : ((float)this.GetRemainingRespecTime() / (float)this.m_garrTalentRec.RespecDurationSecs));
			this.m_researchProgressFill.fillAmount = num;
			this.SetVisualState(TalentVisualState.researching);
			if (!this.m_requestedUpdate && num >= 1f)
			{
				this.m_requestedUpdate = true;
				MobilePlayerGarrisonDataRequest mobilePlayerGarrisonDataRequest = new MobilePlayerGarrisonDataRequest();
				mobilePlayerGarrisonDataRequest.GarrTypeID = 3;
				Login.instance.SendToMobileServer(mobilePlayerGarrisonDataRequest);
				this.m_shouldShowCheckAnim = true;
				AutoCenterItem componentInParent = base.gameObject.GetComponentInParent<AutoCenterItem>();
				if (componentInParent && !componentInParent.IsCentered() && !this.m_playedTalentToast)
				{
					Main.instance.m_UISound.Play_TalentReadyToast();
					this.m_playedTalentToast = true;
				}
			}
		}
		if (this.m_shouldShowCheckAnim && !this.m_playedShowCheckAnim)
		{
			AutoCenterItem componentInParent2 = base.gameObject.GetComponentInParent<AutoCenterItem>();
			if (componentInParent2 && componentInParent2.IsCentered())
			{
				UiAnimMgr.instance.PlayAnim("TalentDoneAnim", base.transform, Vector3.zero, 1f, 0f);
				Main.instance.m_UISound.Play_TalentReadyCheck();
				this.m_shouldShowCheckAnim = false;
				this.m_playedShowCheckAnim = true;
			}
		}
	}

	public void HandleTap()
	{
		AllPopups.instance.ShowTalentTooltip(this);
	}

	public void StartResearch()
	{
		if (this.m_canResearch)
		{
			MobilePlayerResearchGarrisonTalent mobilePlayerResearchGarrisonTalent = new MobilePlayerResearchGarrisonTalent();
			mobilePlayerResearchGarrisonTalent.GarrTalentID = this.m_garrTalentRec.ID;
			Login.instance.SendToMobileServer(mobilePlayerResearchGarrisonTalent);
		}
	}

	public void SetVisualState(TalentVisualState visualState)
	{
		this.m_visualState = visualState;
		switch (visualState)
		{
		case TalentVisualState.canResearch:
			this.m_greenFrameGlow.gameObject.SetActive(true);
			this.m_yellowFrameGlow.gameObject.SetActive(false);
			this.m_researchProgressBG.gameObject.SetActive(false);
			this.m_abilityIcon.color = Color.white;
			this.m_abilityIcon.material.SetFloat("_GrayscaleAmount", 0f);
			break;
		case TalentVisualState.canRespec:
			this.m_greenFrameGlow.gameObject.SetActive(false);
			this.m_yellowFrameGlow.gameObject.SetActive(false);
			this.m_researchProgressBG.gameObject.SetActive(false);
			this.m_abilityIcon.color = this.m_inactiveColor;
			this.m_abilityIcon.material.SetFloat("_GrayscaleAmount", 0f);
			break;
		case TalentVisualState.cannotResearch:
			this.m_greenFrameGlow.gameObject.SetActive(false);
			this.m_yellowFrameGlow.gameObject.SetActive(false);
			this.m_researchProgressBG.gameObject.SetActive(false);
			this.m_abilityIcon.color = Color.white;
			this.m_abilityIcon.material.SetFloat("_GrayscaleAmount", 1f);
			break;
		case TalentVisualState.researching:
			this.m_greenFrameGlow.gameObject.SetActive(false);
			this.m_yellowFrameGlow.gameObject.SetActive(true);
			this.m_researchProgressBG.gameObject.SetActive(true);
			this.m_abilityIcon.color = this.m_inactiveColor;
			this.m_abilityIcon.material.SetFloat("_GrayscaleAmount", 1f);
			break;
		case TalentVisualState.owned:
			this.m_greenFrameGlow.gameObject.SetActive(false);
			this.m_yellowFrameGlow.gameObject.SetActive(true);
			this.m_researchProgressBG.gameObject.SetActive(false);
			this.m_abilityIcon.color = Color.white;
			this.m_abilityIcon.material.SetFloat("_GrayscaleAmount", 0f);
			break;
		}
	}

	public Image m_abilityIcon;

	public Image m_greenFrameGlow;

	public Image m_yellowFrameGlow;

	public Image m_researchProgressBG;

	public Image m_researchProgressFill;

	public Text m_missingIconText;

	public Shader m_grayscaleShader;

	private string m_whyCantResearch;

	private JamGarrisonTalent m_talent;

	private int m_garrTalentID;

	private GarrTalentRec m_garrTalentRec;

	private Color m_inactiveColor;

	private bool m_canResearch;

	private TalentVisualState m_visualState;

	private bool m_requestedUpdate;

	private bool m_shouldShowCheckAnim;

	private bool m_playedShowCheckAnim;

	private bool m_playedTalentToast;
}
