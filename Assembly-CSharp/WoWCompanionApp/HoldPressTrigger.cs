using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WoWCompanionApp
{
	public class HoldPressTrigger : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
	{
		public void OnApplicationPause(bool pause)
		{
			this.m_downState = false;
		}

		public void OnPointerDown(PointerEventData pointerEventData)
		{
			if (!this.m_downState)
			{
				this.m_holdStart = DateTime.Now;
				this.m_downState = true;
			}
		}

		public void OnPointerExit(PointerEventData pointerEventData)
		{
			this.m_downState = false;
		}

		public void OnPointerUp(PointerEventData pointerEventData)
		{
			this.m_downState = false;
		}

		public void Update()
		{
			if (this.m_downState && (DateTime.Now - this.m_holdStart).Seconds >= this.m_secondsToTrigger)
			{
				if (this.m_callbackEvent != null)
				{
					this.m_callbackEvent();
				}
				this.m_downState = false;
			}
		}

		public void SetCallback(Action eventDelegate)
		{
			this.m_callbackEvent = (Action)Delegate.Combine(this.m_callbackEvent, eventDelegate);
		}

		public int m_secondsToTrigger;

		private bool m_downState;

		private DateTime m_holdStart;

		private Action m_callbackEvent;
	}
}
