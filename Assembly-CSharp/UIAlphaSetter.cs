using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAlphaSetter : MonoBehaviour
{
	public void SetAlpha(float alpha)
	{
		if (this.m_image != null)
		{
			Color color = this.m_image.color;
			color.a = alpha;
			this.m_image.color = color;
		}
		if (this.m_canvasGroup != null)
		{
			this.m_canvasGroup.alpha = alpha;
		}
	}

	public void SetAlphaZero()
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
	}

	public Image m_image;

	public CanvasGroup m_canvasGroup;
}
