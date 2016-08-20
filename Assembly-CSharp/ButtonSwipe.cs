using System;
using UnityEngine;

public class ButtonSwipe : MonoBehaviour
{
	public void OnBeginDrag()
	{
		RectTransform component = base.GetComponent<RectTransform>();
		this.m_initialX = component.localPosition.x;
		if (Input.touchCount > 0)
		{
			this.m_initialTouchX = Input.GetTouch(0).position.x;
		}
		else
		{
			this.m_initialTouchX = Input.mousePosition.x;
		}
	}

	public void OnDrag()
	{
		if (Input.touchCount > 0)
		{
			this.m_currentTouchX = Input.GetTouch(0).position.x;
		}
		else
		{
			this.m_currentTouchX = Input.mousePosition.x;
		}
		float num = this.m_currentTouchX - this.m_initialTouchX;
		RectTransform component = base.GetComponent<RectTransform>();
		Vector3 localPosition = component.localPosition;
		localPosition.x = this.m_initialX + num;
		component.localPosition = localPosition;
	}

	public void OnEndDrag()
	{
	}

	public float m_initialX;

	public float m_initialTouchX;

	public float m_currentTouchX;
}
