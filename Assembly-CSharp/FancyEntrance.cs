using System;
using UnityEngine;

public class FancyEntrance : MonoBehaviour
{
	private void OnEnable()
	{
		this.Reset();
	}

	public void Reset()
	{
		iTween.StopByName(base.gameObject, "FancyAppearancePunch");
		if (this.m_activateOnEnable)
		{
			this.Activate();
		}
	}

	public void Activate()
	{
		this.m_entranceDelayDuration = this.m_timeToDelayEntrance;
		this.m_fadeInTimeElapsed = 0f;
		this.m_fadeInCanvasGroup.alpha = 0f;
		this.m_punchedScale = false;
		this.m_active = true;
	}

	private void OnPunchScaleComplete()
	{
		base.gameObject.transform.localScale = Vector3.one;
	}

	private void Update()
	{
		if (!this.m_active)
		{
			return;
		}
		if (!this.m_fadeInCanvasGroup.interactable)
		{
			return;
		}
		this.m_entranceDelayDuration -= Time.deltaTime;
		if (this.m_entranceDelayDuration > 0f)
		{
			return;
		}
		this.m_entranceDelayDuration = 0f;
		if (this.m_fadeInTimeElapsed < this.m_fadeInTime)
		{
			this.m_fadeInTimeElapsed += Time.deltaTime;
			float alpha = Mathf.Clamp(this.m_fadeInTimeElapsed / this.m_fadeInTime, 0f, 1f);
			this.m_fadeInCanvasGroup.alpha = alpha;
		}
		if (this.m_punchScale && !this.m_punchedScale)
		{
			iTween.PunchScale(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FancyAppearancePunch",
				"x",
				this.m_punchScaleAmount,
				"y",
				this.m_punchScaleAmount,
				"z",
				this.m_punchScaleAmount,
				"time",
				this.m_punchScaleDuration,
				"oncomplete",
				"OnPunchScaleComplete"
			}));
			this.m_punchedScale = true;
		}
	}

	[Header("We Fancy")]
	public bool m_activateOnEnable;

	public float m_timeToDelayEntrance;

	public CanvasGroup m_fadeInCanvasGroup;

	public float m_fadeInTime;

	public bool m_punchScale;

	public float m_punchScaleAmount;

	public float m_punchScaleDuration;

	private float m_entranceDelayDuration;

	private float m_fadeInTimeElapsed;

	private bool m_punchedScale;

	private bool m_active;
}
