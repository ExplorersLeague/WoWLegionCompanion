using System;
using UnityEngine;
using UnityEngine.UI;

public class TroopHeartEffect : MonoBehaviour
{
	public void SetHeartEffectProgress(float progress)
	{
		if (this.m_image != null)
		{
			Color color = this.m_image.color;
			color.a = 1f - progress;
			this.m_image.color = color;
		}
		if (this.m_canvasGroup != null)
		{
			this.m_canvasGroup.alpha = 1f - progress;
		}
		base.transform.localScale = Vector3.one * (1f + progress * (this.m_targetScale - 1f));
	}

	public void FinishHeartEffect()
	{
		if (this.m_image != null)
		{
			Color color = this.m_image.color;
			color.a = 0f;
			this.m_image.color = color;
		}
		if (this.m_canvasGroup != null)
		{
			this.m_canvasGroup.alpha = 0f;
		}
		base.transform.localScale = Vector3.one * this.m_targetScale;
	}

	public Image m_image;

	public CanvasGroup m_canvasGroup;

	public float m_targetScale;
}
