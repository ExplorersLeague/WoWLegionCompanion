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
		this.m_calledStartCallback = false;
		if (this.m_activateOnEnable)
		{
			this.Activate();
		}
	}

	public void Activate()
	{
		base.gameObject.transform.localScale = Vector3.one;
		this.m_entranceDelayDuration = this.m_timeToDelayEntrance;
		this.m_fadeInTimeElapsed = 0f;
		this.m_fadeInCanvasGroup.alpha = 0f;
		this.m_punchedScale = false;
		this.m_scaledUp = false;
		this.m_active = true;
	}

	private void OnPunchScaleComplete()
	{
		base.gameObject.transform.localScale = Vector3.one;
	}

	private void OnScaleUpComplete()
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
		if (!this.m_calledStartCallback)
		{
			if (this.m_objectToNotifyOnBegin != null && this.m_notifyOnBeginCallbackName != null)
			{
				this.m_objectToNotifyOnBegin.BroadcastMessage(this.m_notifyOnBeginCallbackName, 1);
			}
			this.m_calledStartCallback = true;
		}
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
		if (this.m_scaleUp && !this.m_scaledUp)
		{
			iTween.ScaleFrom(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FancyAppearanceScaleUp",
				"x",
				this.m_scaleUpFrom,
				"y",
				this.m_scaleUpFrom,
				"z",
				this.m_scaleUpFrom,
				"time",
				this.m_scaleUpDuration,
				"oncomplete",
				"OnScaleUpComplete"
			}));
			this.m_scaledUp = true;
		}
	}

	[Header("We Fancy")]
	public bool m_activateOnEnable;

	public float m_timeToDelayEntrance;

	public GameObject m_objectToNotifyOnBegin;

	public string m_notifyOnBeginCallbackName;

	[Header("Fade In")]
	public CanvasGroup m_fadeInCanvasGroup;

	public float m_fadeInTime;

	[Header("Punch Scale")]
	public bool m_punchScale;

	public float m_punchScaleAmount;

	public float m_punchScaleDuration;

	[Header("Scale Up")]
	public bool m_scaleUp;

	public float m_scaleUpFrom;

	public float m_scaleUpDuration;

	private float m_entranceDelayDuration;

	private float m_fadeInTimeElapsed;

	private bool m_punchedScale;

	private bool m_scaledUp;

	private bool m_active;

	private bool m_calledStartCallback;
}
