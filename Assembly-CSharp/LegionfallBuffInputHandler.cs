using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegionfallBuffInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IEventSystemHandler
{
	private void Start()
	{
		this.m_autoCenterScrollRect = base.GetComponentInParent<AutoCenterScrollRect>();
		this.m_scrollRect = base.GetComponentInParent<ScrollRect>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			return;
		}
		if (this.m_button != null && !this.m_button.interactable)
		{
			return;
		}
		if (this.m_objectToNotifyOnClick != null)
		{
			this.m_objectToNotifyOnClick.SendMessage(this.m_notifyFuncName);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.m_autoCenterScrollRect.OnTouchStart(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			return;
		}
		this.m_autoCenterScrollRect.OnTouchEnd(eventData);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		this.m_scrollRect.OnBeginDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.m_scrollRect.OnDrag(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.m_autoCenterScrollRect.OnTouchEnd(eventData);
		this.m_scrollRect.OnEndDrag(eventData);
	}

	private AutoCenterScrollRect m_autoCenterScrollRect;

	private ScrollRect m_scrollRect;

	public GameObject m_objectToNotifyOnClick;

	public string m_notifyFuncName;

	public Button m_button;
}
