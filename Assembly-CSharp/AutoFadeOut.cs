using System;
using UnityEngine;

public class AutoFadeOut : MonoBehaviour
{
	public void EnableFadeOut()
	{
		this.m_enableFadeOut = true;
	}

	public void Reset()
	{
		this.m_enableFadeOut = false;
		this.m_canvasGroupToFadeOut.alpha = 1f;
		this.m_elapsedFadeTime = 0f;
		this.m_canvasGroupToFadeOut.blocksRaycasts = true;
	}

	private void Update()
	{
		if (this.m_enableFadeOut)
		{
			this.m_elapsedFadeTime += Time.deltaTime;
			this.m_canvasGroupToFadeOut.alpha = 1f - Mathf.Clamp01(this.m_elapsedFadeTime / this.m_fadeOutTime);
			if (this.m_canvasGroupToFadeOut.alpha < 0.99f)
			{
				this.m_canvasGroupToFadeOut.blocksRaycasts = false;
			}
		}
	}

	public CanvasGroup m_canvasGroupToFadeOut;

	public float m_fadeOutTime;

	private bool m_enableFadeOut;

	private float m_elapsedFadeTime;
}
