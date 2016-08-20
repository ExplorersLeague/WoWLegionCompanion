using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

public class GenericPopup : MonoBehaviour
{
	private void Start()
	{
		this.m_headerText.font = GeneralHelpers.LoadStandardFont();
		this.m_descriptionText.font = GeneralHelpers.LoadStandardFont();
		this.m_fullText.font = GeneralHelpers.LoadStandardFont();
	}

	public void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_canvasBlurManager.AddBlurRef_Level2Canvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_canvasBlurManager.RemoveBlurRef_Level2Canvas();
		Main.instance.m_backButtonManager.PopBackAction();
		if (GenericPopup.DisabledAction != null)
		{
			GenericPopup.DisabledAction();
		}
	}

	public void SetText(string headerText, string descriptionText)
	{
		this.m_headerText.gameObject.SetActive(true);
		this.m_descriptionText.gameObject.SetActive(true);
		this.m_fullText.gameObject.SetActive(false);
		this.m_headerText.text = headerText;
		this.m_descriptionText.text = descriptionText;
	}

	public void SetFullText(string fullText)
	{
		this.m_headerText.gameObject.SetActive(false);
		this.m_descriptionText.gameObject.SetActive(false);
		this.m_fullText.gameObject.SetActive(true);
		this.m_fullText.text = fullText;
	}

	public Text m_headerText;

	public Text m_descriptionText;

	public Text m_fullText;

	public static Action DisabledAction;
}
