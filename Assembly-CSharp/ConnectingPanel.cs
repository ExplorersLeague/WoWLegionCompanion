using System;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingPanel : MonoBehaviour
{
	private void Start()
	{
		this.m_cancelText.font = GeneralHelpers.LoadStandardFont();
	}

	private void OnEnable()
	{
		this.m_isFadingIn = true;
		this.m_isFadingOut = false;
		this.m_fadeInTimeElapsed = 0f;
		foreach (CanvasGroup canvasGroup in this.m_fadeCanvasGroups)
		{
			canvasGroup.alpha = 0f;
		}
		if (StaticDB.StringsAvailable())
		{
			this.m_cancelText.text = StaticDB.GetString("CANCEL", null);
		}
	}

	public void Hide()
	{
		this.m_isFadingIn = false;
		this.m_isFadingOut = true;
		this.m_fadeOutTimeElapsed = 0f;
	}

	private void Update()
	{
		if (StaticDB.StringsAvailable())
		{
			if (!this.m_cancelButton.activeSelf)
			{
				this.m_cancelButton.SetActive(true);
				this.m_cancelText.text = StaticDB.GetString("CANCEL", null);
			}
		}
		else if (this.m_cancelButton.activeSelf)
		{
			this.m_cancelButton.SetActive(false);
		}
		if (this.m_isFadingIn && this.m_fadeInTimeElapsed < this.m_fadeInDuration)
		{
			this.m_fadeInTimeElapsed += Time.deltaTime;
			float alpha = Mathf.Clamp(this.m_fadeInTimeElapsed / this.m_fadeInDuration, 0f, 1f);
			foreach (CanvasGroup canvasGroup in this.m_fadeCanvasGroups)
			{
				canvasGroup.alpha = alpha;
			}
		}
		if (this.m_isFadingOut && this.m_fadeOutTimeElapsed < this.m_fadeOutDuration)
		{
			this.m_fadeOutTimeElapsed += Time.deltaTime;
			float alpha2 = 1f - Mathf.Clamp(this.m_fadeOutTimeElapsed / this.m_fadeOutDuration, 0f, 1f);
			foreach (CanvasGroup canvasGroup2 in this.m_fadeCanvasGroups)
			{
				canvasGroup2.alpha = alpha2;
			}
			if (this.m_fadeOutTimeElapsed > this.m_fadeOutDuration)
			{
				this.m_isFadingOut = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	public Text m_statusText;

	public Text m_cancelText;

	public GameObject m_cancelButton;

	public float m_fadeInDuration;

	public float m_fadeOutDuration;

	public CanvasGroup[] m_fadeCanvasGroups;

	private bool m_isFadingIn;

	private bool m_isFadingOut;

	private float m_fadeInTimeElapsed;

	private float m_fadeOutTimeElapsed;
}
