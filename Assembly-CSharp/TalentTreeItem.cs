using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobilePlayerJSON;
using WowStatConstants;
using WowStaticData;

public class TalentTreeItem : MonoBehaviour
{
	private void Awake()
	{
		this.m_BGChoiceDisabled.gameObject.SetActive(true);
		this.m_BGChoiceEnabled.gameObject.SetActive(false);
		this.m_ChoiceEnabledGlowyArrows.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		Main instance = Main.instance;
		instance.CanResearchGarrisonTalentResultAction = (Action<int, int, string>)Delegate.Combine(instance.CanResearchGarrisonTalentResultAction, new Action<int, int, string>(this.HandleCanResearchGarrisonTalentResult));
		Main instance2 = Main.instance;
		instance2.ResearchGarrisonTalentResultAction = (Action<int, int>)Delegate.Combine(instance2.ResearchGarrisonTalentResultAction, new Action<int, int>(this.HandleResearchGarrisonTalentResult));
	}

	private void OnDisable()
	{
		Main instance = Main.instance;
		instance.CanResearchGarrisonTalentResultAction = (Action<int, int, string>)Delegate.Remove(instance.CanResearchGarrisonTalentResultAction, new Action<int, int, string>(this.HandleCanResearchGarrisonTalentResult));
		Main instance2 = Main.instance;
		instance2.ResearchGarrisonTalentResultAction = (Action<int, int>)Delegate.Remove(instance2.ResearchGarrisonTalentResultAction, new Action<int, int>(this.HandleResearchGarrisonTalentResult));
	}

	public void UpdateVisualStates()
	{
		if (this.m_talentButtonLeft.CanResearch() && this.m_talentButtonRight.CanResearch())
		{
			this.m_talentButtonLeft.SetVisualState(TalentVisualState.canResearch);
			this.m_talentButtonRight.SetVisualState(TalentVisualState.canResearch);
		}
		else if (this.m_talentButtonLeft.IsOwned() && this.m_talentButtonRight.CanResearch())
		{
			this.m_talentButtonRight.SetVisualState(TalentVisualState.canRespec);
		}
		else if (this.m_talentButtonLeft.CanResearch() && this.m_talentButtonRight.IsOwned())
		{
			this.m_talentButtonLeft.SetVisualState(TalentVisualState.canRespec);
		}
		if (this.m_talentButtonLeft.IsOwned())
		{
			this.m_talentButtonLeft.SetVisualState(TalentVisualState.owned);
		}
		else if (this.m_talentButtonLeft.IsResearching())
		{
			this.m_talentButtonLeft.SetVisualState(TalentVisualState.researching);
		}
		else if (!this.m_talentButtonLeft.CanResearch())
		{
			this.m_talentButtonLeft.SetVisualState(TalentVisualState.cannotResearch);
		}
		if (this.m_talentButtonRight.IsOwned())
		{
			this.m_talentButtonRight.SetVisualState(TalentVisualState.owned);
		}
		else if (this.m_talentButtonRight.IsResearching())
		{
			this.m_talentButtonRight.SetVisualState(TalentVisualState.researching);
		}
		else if (!this.m_talentButtonRight.CanResearch())
		{
			this.m_talentButtonRight.SetVisualState(TalentVisualState.cannotResearch);
		}
		if (this.m_talentButtonSolo.IsOwned())
		{
			this.m_talentButtonSolo.SetVisualState(TalentVisualState.owned);
		}
		else if (this.m_talentButtonSolo.IsResearching())
		{
			this.m_talentButtonSolo.SetVisualState(TalentVisualState.researching);
		}
		else if (this.m_talentButtonSolo.CanResearch())
		{
			this.m_talentButtonSolo.SetVisualState(TalentVisualState.canResearch);
		}
		else if (!this.m_talentButtonSolo.CanResearch())
		{
			this.m_talentButtonSolo.SetVisualState(TalentVisualState.cannotResearch);
		}
		bool flag = this.m_talentButtonLeft.IsSet() && this.m_talentButtonRight.IsSet();
		this.m_dualDisplayRoot.SetActive(flag);
		this.m_soloDisplayRoot.SetActive(!flag);
		if (this.m_talentButtonLeft.CanResearch() && this.m_talentButtonRight.CanResearch())
		{
			this.m_BGChoiceDisabled.gameObject.SetActive(false);
			this.m_BGChoiceEnabled.gameObject.SetActive(true);
			this.m_ChoiceEnabledGlowyArrows.gameObject.SetActive(true);
			iTween.Stop(this.m_ChoiceEnabledGlowyArrows.gameObject);
			iTween.ValueTo(this.m_ChoiceEnabledGlowyArrows.gameObject, iTween.Hash(new object[]
			{
				"name",
				"fade",
				"from",
				1f,
				"to",
				0f,
				"time",
				1f,
				"looptype",
				iTween.LoopType.pingPong,
				"easetype",
				iTween.EaseType.easeInOutSine,
				"onupdatetarget",
				this.m_ChoiceEnabledGlowyArrows.gameObject,
				"onupdate",
				"SetAlpha"
			}));
		}
		else
		{
			this.m_BGChoiceDisabled.gameObject.SetActive(true);
			this.m_BGChoiceEnabled.gameObject.SetActive(false);
			this.m_ChoiceEnabledGlowyArrows.gameObject.SetActive(false);
		}
	}

	public void HandleGarrisonDataResetFinished()
	{
		this.m_talentButtonLeft.HandleGarrisonDataResetFinished();
		this.m_talentButtonRight.HandleGarrisonDataResetFinished();
		this.m_talentButtonSolo.HandleGarrisonDataResetFinished();
		this.UpdateVisualStates();
	}

	private void HandleCanResearchGarrisonTalentResult(int garrTalentID, int result, string whyCantResearch)
	{
		bool flag = false;
		if (garrTalentID == this.m_talentButtonLeft.GetTalentID())
		{
			this.m_talentButtonLeft.HandleCanResearchGarrisonTalentResult(garrTalentID, result, whyCantResearch);
			flag = true;
		}
		if (garrTalentID == this.m_talentButtonRight.GetTalentID())
		{
			this.m_talentButtonRight.HandleCanResearchGarrisonTalentResult(garrTalentID, result, whyCantResearch);
			flag = true;
		}
		if (garrTalentID == this.m_talentButtonSolo.GetTalentID())
		{
			this.m_talentButtonSolo.HandleCanResearchGarrisonTalentResult(garrTalentID, result, whyCantResearch);
			flag = true;
		}
		if (flag)
		{
			this.UpdateVisualStates();
		}
	}

	private void HandleResearchGarrisonTalentResult(int garrTalentID, int result)
	{
		if (this.m_talentButtonLeft.GetTalentID() != garrTalentID && this.m_talentButtonRight.GetTalentID() != garrTalentID)
		{
			return;
		}
		if (result == 0)
		{
			Main.instance.m_UISound.Play_BeginResearch();
		}
		AllPanels.instance.m_talentTreePanel.SetNeedsFullInit();
		MobilePlayerGarrisonDataRequest mobilePlayerGarrisonDataRequest = new MobilePlayerGarrisonDataRequest();
		mobilePlayerGarrisonDataRequest.GarrTypeID = 3;
		Login.instance.SendToMobileServer(mobilePlayerGarrisonDataRequest);
	}

	public void SetTalent(GarrTalentRec garrTalentRec)
	{
		TalentTreeItemAbilityButton talentTreeItemAbilityButton;
		TalentTreeItemAbilityButton sameTierButton;
		if (garrTalentRec.UiOrder == 0)
		{
			talentTreeItemAbilityButton = this.m_talentButtonLeft;
			sameTierButton = this.m_talentButtonRight;
		}
		else
		{
			talentTreeItemAbilityButton = this.m_talentButtonRight;
			sameTierButton = this.m_talentButtonLeft;
		}
		talentTreeItemAbilityButton.SetTalent(garrTalentRec.ID, sameTierButton);
		this.m_talentButtonSolo.SetTalent(garrTalentRec.ID, null);
	}

	public void PlayClickSound()
	{
		Main.instance.m_UISound.Play_OrderHallTalentSelect();
	}

	public Image m_BGChoiceDisabled;

	public Image m_BGChoiceEnabled;

	public GameObject m_ChoiceEnabledGlowyArrows;

	public TalentTreeItemAbilityButton m_talentButtonLeft;

	public TalentTreeItemAbilityButton m_talentButtonRight;

	public TalentTreeItemAbilityButton m_talentButtonSolo;

	public GameObject m_dualDisplayRoot;

	public GameObject m_soloDisplayRoot;

	public Image m_talentTier;
}
