using System;
using UnityEngine;

public class SlidingPanel : MonoBehaviour
{
	private bool IsVerticalSlide()
	{
		return this.m_slideDirection == SlidingPanel.SlideDirection.FromTop || this.m_slideDirection == SlidingPanel.SlideDirection.FromBottom;
	}

	private bool IsHorizontalSlide()
	{
		return this.m_slideDirection == SlidingPanel.SlideDirection.FromLeft || this.m_slideDirection == SlidingPanel.SlideDirection.FromRight;
	}

	private Vector2 GetOffsetFromSlideDirection(Rect rect)
	{
		float num = this.IsHorizontalSlide() ? ((this.m_slideDirection != SlidingPanel.SlideDirection.FromLeft) ? (rect.xMin - rect.xMax) : (rect.xMax - rect.xMin)) : 0f;
		float num2 = this.IsVerticalSlide() ? ((this.m_slideDirection != SlidingPanel.SlideDirection.FromTop) ? (rect.yMin - rect.yMax) : (rect.yMax - rect.yMin)) : 0f;
		return new Vector2(num, num2);
	}

	public void SlideIn()
	{
		this.StartSlide(true);
	}

	public void SlideOut()
	{
		this.StartSlide(false);
	}

	private void SetAnchoredPosition(Vector2 position)
	{
		base.GetComponent<RectTransform>().anchoredPosition = position;
	}

	private void FinishedMoving()
	{
		if (this.m_closing && this.m_closedAction != null)
		{
			this.m_closedAction();
		}
		else if (this.m_openedAction != null)
		{
			this.m_openedAction();
		}
		this.m_sliding = false;
		this.m_closing = false;
	}

	private void StartSlide(bool slidingIn)
	{
		if (!this.m_sliding)
		{
			this.m_sliding = true;
			RectTransform component = base.GetComponent<RectTransform>();
			Vector2 vector = this.GetOffsetFromSlideDirection(component.rect);
			if (!slidingIn)
			{
				vector *= -1f;
				this.m_closing = true;
			}
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				component.anchoredPosition,
				"to",
				component.anchoredPosition + vector,
				"time",
				this.m_slideDuration,
				"onupdate",
				"SetAnchoredPosition",
				"oncomplete",
				"FinishedMoving",
				"oncompletetarget",
				base.gameObject
			}));
		}
	}

	private bool m_closing;

	private bool m_sliding;

	public float m_slideDuration;

	public SlidingPanel.SlideDirection m_slideDirection;

	public Action m_closedAction;

	public Action m_openedAction;

	public enum SlideDirection
	{
		FromTop,
		FromBottom,
		FromLeft,
		FromRight
	}
}
