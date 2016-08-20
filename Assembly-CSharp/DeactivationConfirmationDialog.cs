using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

public class DeactivationConfirmationDialog : MonoBehaviour
{
	private void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	private void Start()
	{
		this.m_areYouSureLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_reactivationCostLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_cancelButtonLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_okButtonLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_reactivationCostText.font = GeneralHelpers.LoadStandardFont();
	}

	public void Show(FollowerDetailView followerDetailView)
	{
		base.gameObject.SetActive(true);
		this.m_followerDetailView = followerDetailView;
		this.m_areYouSureLabel.text = StaticDB.GetString("ARE_YOU_SURE", null);
		this.m_reactivationCostLabel.text = StaticDB.GetString("CHAMPION_REACTIVATION_COST", null);
		this.m_cancelButtonLabel.text = StaticDB.GetString("NO", null);
		this.m_okButtonLabel.text = StaticDB.GetString("YES_DEACTIVATE", null);
		this.m_reactivationCostText.text = string.Empty + GarrisonStatus.GetFollowerActivationGoldCost();
	}

	public void ConfirmDeactivate()
	{
		this.m_followerDetailView.DeactivateFollower();
		base.gameObject.SetActive(false);
	}

	public Text m_areYouSureLabel;

	public Text m_reactivationCostLabel;

	public Text m_cancelButtonLabel;

	public Text m_okButtonLabel;

	public Text m_reactivationCostText;

	private FollowerDetailView m_followerDetailView;
}
