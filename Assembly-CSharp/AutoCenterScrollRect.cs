using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoCenterScrollRect : MonoBehaviour
{
	private void Awake()
	{
		this.m_horizontalLayoutGroup = this.m_scrollRectContentsRT.GetComponentInChildren<HorizontalLayoutGroup>();
	}

	private void OnAutoCenterUpdate(float newX)
	{
		Vector3 vector = this.m_scrollRectContentsRT.anchoredPosition;
		vector.x = newX;
		this.m_scrollRectContentsRT.anchoredPosition = vector;
	}

	private void OnAutoCenterComplete()
	{
		Vector3 vector = this.m_scrollRectContentsRT.anchoredPosition;
		vector.x = this.m_targetX;
		this.m_scrollRectContentsRT.anchoredPosition = vector;
		this.m_centeredItem.SetCentered(true);
	}

	public void OnTouchStart(BaseEventData eventData)
	{
		Vector2 vector;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_scrollViewportRT, ((PointerEventData)eventData).position, Camera.main, ref vector);
		this.m_touchStartX = vector.x;
		iTween.Stop(this.m_scrollRectContentsRT.gameObject);
	}

	public void OnTouchEnd(BaseEventData eventData)
	{
		this.OnTouchEnd(eventData, -1);
	}

	public void CenterOnItem(int itemIndex)
	{
		AutoCenterItem[] componentsInChildren = this.m_scrollRectContentsRT.GetComponentsInChildren<AutoCenterItem>(true);
		RectTransform component = componentsInChildren[itemIndex].GetComponent<RectTransform>();
		this.m_targetX = -component.anchoredPosition.x;
		if (componentsInChildren[itemIndex].IsCentered())
		{
			return;
		}
		foreach (AutoCenterItem autoCenterItem in componentsInChildren)
		{
			autoCenterItem.SetCentered(false);
		}
		this.m_centeredItem = componentsInChildren[itemIndex];
		iTween.Stop(this.m_scrollRectContentsRT.gameObject);
		iTween.ValueTo(this.m_scrollRectContentsRT.gameObject, iTween.Hash(new object[]
		{
			"name",
			"autocenter",
			"from",
			this.m_scrollRectContentsRT.anchoredPosition.x,
			"to",
			this.m_targetX,
			"time",
			this.m_autoCenterDuration,
			"easetype",
			this.m_easeType,
			"onupdatetarget",
			base.gameObject,
			"onupdate",
			"OnAutoCenterUpdate",
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"OnAutoCenterComplete"
		}));
	}

	public void OnTouchEnd(BaseEventData eventData, int itemIndex)
	{
		if (eventData != null)
		{
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_scrollViewportRT, ((PointerEventData)eventData).position, Camera.main, ref vector);
			this.m_touchEndX = vector.x;
		}
		bool flag = this.m_touchEndX - this.m_touchStartX >= this.m_minSwipeDistance;
		bool flag2 = this.m_touchStartX - this.m_touchEndX >= this.m_minSwipeDistance;
		bool flag3 = false;
		bool flag4 = (flag && this.m_touchEndX - this.m_touchStartX > this.m_maxSwipeDistance) || (flag2 && this.m_touchStartX - this.m_touchEndX > this.m_maxSwipeDistance);
		AutoCenterItem[] componentsInChildren = this.m_scrollRectContentsRT.GetComponentsInChildren<AutoCenterItem>(true);
		int num = 0;
		if (itemIndex >= 0)
		{
			num = itemIndex;
		}
		else
		{
			float num2 = -1f;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				RectTransform component = componentsInChildren[i].GetComponent<RectTransform>();
				float num3 = Mathf.Abs(-this.m_scrollRectContentsRT.anchoredPosition.x - component.anchoredPosition.x);
				if (num2 < 0f || num3 < num2)
				{
					num2 = num3;
					num = i;
				}
			}
			if (num == 0 && this.m_scrollRectContentsRT.anchoredPosition.x > 0f)
			{
				flag3 = true;
			}
			if (num == componentsInChildren.Length - 1)
			{
				RectTransform component2 = componentsInChildren[num].GetComponent<RectTransform>();
				if (-this.m_scrollRectContentsRT.anchoredPosition.x > component2.anchoredPosition.x)
				{
					flag3 = true;
				}
			}
		}
		if (flag3)
		{
			return;
		}
		if (eventData != null && !flag4)
		{
			if (flag2 && num < componentsInChildren.Length - 1)
			{
				num++;
			}
			if (flag && num > 0)
			{
				num--;
			}
		}
		RectTransform component3 = componentsInChildren[num].GetComponent<RectTransform>();
		this.m_targetX = -component3.anchoredPosition.x + (float)this.m_horizontalLayoutGroup.padding.left;
		foreach (AutoCenterItem autoCenterItem in componentsInChildren)
		{
			autoCenterItem.SetCentered(false);
		}
		this.m_centeredItem = componentsInChildren[num];
		iTween.Stop(this.m_scrollRectContentsRT.gameObject);
		iTween.ValueTo(this.m_scrollRectContentsRT.gameObject, iTween.Hash(new object[]
		{
			"name",
			"autocenter",
			"from",
			this.m_scrollRectContentsRT.anchoredPosition.x,
			"to",
			this.m_targetX,
			"time",
			this.m_autoCenterDuration,
			"easetype",
			this.m_easeType,
			"onupdatetarget",
			base.gameObject,
			"onupdate",
			"OnAutoCenterUpdate",
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"OnAutoCenterComplete"
		}));
	}

	public RectTransform m_scrollViewportRT;

	public RectTransform m_scrollRectContentsRT;

	public float m_autoCenterDuration;

	public iTween.EaseType m_easeType;

	public float m_minSwipeDistance;

	public float m_maxSwipeDistance;

	public HorizontalLayoutGroup m_horizontalLayoutGroup;

	private float m_targetX;

	private float m_touchStartX;

	private float m_touchEndX;

	private AutoCenterItem m_centeredItem;
}
