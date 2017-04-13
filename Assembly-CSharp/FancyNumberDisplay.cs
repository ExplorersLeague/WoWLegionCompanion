using System;
using UnityEngine;
using UnityEngine.UI;

public class FancyNumberDisplay : MonoBehaviour
{
	public void SetNumberLabel(string numberLabel)
	{
		this.m_numberLabel = numberLabel;
	}

	public void SetValue(int newValue, float delayStartTimerTime = 0f)
	{
		this.SetValue(newValue, false, delayStartTimerTime);
	}

	public void SetValue(int newValue, bool instant, float delayStartTimerTime = 0f)
	{
		this.m_actualValue = newValue;
		this.m_instant = instant;
		this.m_timeRemainingUntilStartTimer = delayStartTimerTime;
		this.m_startedTimer = false;
		if (instant)
		{
			this.m_currentValue = newValue;
			if (this.m_numberLabel != null)
			{
				this.m_numberText.text = GeneralHelpers.TextOrderString(newValue.ToString(), this.m_numberLabel);
			}
			else
			{
				this.m_numberText.text = newValue.ToString();
			}
		}
		this.m_initialized = true;
	}

	private void Update()
	{
		if (this.m_initialized && !this.m_instant && !this.m_startedTimer)
		{
			this.m_timeRemainingUntilStartTimer -= Time.deltaTime;
			if (this.m_timeRemainingUntilStartTimer <= 0f)
			{
				if (this.TimerStartedAction != null)
				{
					this.TimerStartedAction();
				}
				iTween.Stop(base.gameObject);
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"name",
					"Fancy Number Display",
					"from",
					this.m_currentValue,
					"to",
					this.m_actualValue,
					"easeType",
					this.m_fillEaseType,
					"time",
					this.m_duration,
					"onupdate",
					"TimerUpdateCB",
					"oncomplete",
					"TimerEndedCB"
				}));
				this.m_startedTimer = true;
			}
		}
	}

	private void TimerUpdateCB(int newValue)
	{
		this.m_currentValue = newValue;
		if (this.m_numberLabel != null)
		{
			this.m_numberText.text = GeneralHelpers.TextOrderString(newValue.ToString(), this.m_numberLabel);
		}
		else
		{
			this.m_numberText.text = newValue.ToString();
		}
		if (this.TimerUpdateAction != null)
		{
			this.TimerUpdateAction(newValue);
		}
	}

	private void TimerEndedCB()
	{
		this.m_currentValue = this.m_actualValue;
		if (this.m_numberLabel != null)
		{
			this.m_numberText.text = GeneralHelpers.TextOrderString(this.m_currentValue.ToString(), this.m_numberLabel);
		}
		else
		{
			this.m_numberText.text = this.m_currentValue.ToString();
		}
		if (this.TimerEndedAction != null)
		{
			this.TimerEndedAction();
		}
	}

	public Text m_numberText;

	public int m_actualValue;

	public float m_duration;

	public iTween.EaseType m_fillEaseType;

	public Action TimerStartedAction;

	public Action<int> TimerUpdateAction;

	public Action TimerEndedAction;

	private int m_currentValue;

	private bool m_instant;

	private bool m_initialized;

	private bool m_startedTimer;

	private float m_timeRemainingUntilStartTimer;

	private string m_numberLabel;
}
