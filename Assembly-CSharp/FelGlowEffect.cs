using System;
using UnityEngine;
using UnityEngine.UI;

public class FelGlowEffect : MonoBehaviour
{
	private void Awake()
	{
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"name",
			"Fel Glow Effect",
			"from",
			this.m_alphaMin,
			"to",
			this.m_alphaMax,
			"easeType",
			this.m_easeType,
			"time",
			this.m_duration,
			"looptype",
			iTween.LoopType.pingPong,
			"onupdate",
			"UpdateAlphaCallback"
		}));
	}

	private void UpdateAlphaCallback(float alpha)
	{
		this.m_color = this.m_effectImage.color;
		this.m_color.a = alpha;
		this.m_effectImage.color = this.m_color;
	}

	public Image m_effectImage;

	public iTween.EaseType m_easeType;

	public float m_duration;

	public float m_alphaMin;

	public float m_alphaMax;

	private Color m_color;
}
