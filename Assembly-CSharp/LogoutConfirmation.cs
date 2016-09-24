using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

public class LogoutConfirmation : MonoBehaviour
{
	public bool GoToWebAuth { get; set; }

	private void OnEnable()
	{
		if (this.GoToWebAuth)
		{
			this.m_logoutText.text = StaticDB.GetString("ACCOUNT_SELECTION", null);
		}
		else
		{
			this.m_logoutText.text = StaticDB.GetString("LOG_OUT", null);
		}
		this.m_sureText.text = StaticDB.GetString("ARE_YOU_SURE", null);
		this.m_okayText.text = StaticDB.GetString("OK", null);
		this.m_cancelText.text = StaticDB.GetString("CANCEL", null);
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	public void OnClickOkay()
	{
		AllPopups.instance.HideAllPopups();
		if (this.GoToWebAuth)
		{
			Login.instance.StartNewLogin();
		}
		else
		{
			Login.instance.BackToAccountSelect();
		}
	}

	public void OnClickCancel()
	{
		AllPopups.instance.HideAllPopups();
	}

	public Text m_logoutText;

	public Text m_sureText;

	public Text m_okayText;

	public Text m_cancelText;
}
