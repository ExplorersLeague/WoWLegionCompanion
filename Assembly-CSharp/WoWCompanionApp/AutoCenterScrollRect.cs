using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class AutoCenterScrollRect : MonoBehaviour
	{
		public event Action<int> OnCenterComplete;

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
			this.m_scrollRectContentsRT.GetComponentInParent<ScrollRect>().StopMovement();
			AutoCenterItem[] componentsInChildren = base.GetComponentsInChildren<AutoCenterItem>();
			if (componentsInChildren != null && this.OnCenterComplete != null)
			{
				this.OnCenterComplete(componentsInChildren.ToList<AutoCenterItem>().IndexOf(this.m_centeredItem));
			}
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
			float num = this.m_touchEndX - this.m_touchStartX;
			bool flag = Mathf.Abs(num) >= this.m_minSwipeDistance && Mathf.Sign(num) > 0f;
			bool flag2 = Mathf.Abs(num) >= this.m_minSwipeDistance && Mathf.Sign(num) < 0f;
			bool flag3 = Mathf.Abs(num) > this.m_maxSwipeDistance;
			AutoCenterItem[] componentsInChildren = this.m_scrollRectContentsRT.GetComponentsInChildren<AutoCenterItem>(true);
			int num2 = 0;
			if (itemIndex >= 0)
			{
				num2 = itemIndex;
			}
			else
			{
				float num3 = -1f;
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					RectTransform component = componentsInChildren[i].GetComponent<RectTransform>();
					float num4 = Mathf.Abs(-this.m_scrollRectContentsRT.anchoredPosition.x - component.anchoredPosition.x);
					if (num3 < 0f || num4 < num3)
					{
						num3 = num4;
						num2 = i;
					}
				}
			}
			if (eventData != null)
			{
				if (flag2 && num2 < componentsInChildren.Length - 1)
				{
					num2++;
				}
				if (flag && num2 > 0)
				{
					num2--;
				}
			}
			if (num2 < 0 || num2 >= componentsInChildren.Length)
			{
				return;
			}
			RectTransform component2 = componentsInChildren[num2].GetComponent<RectTransform>();
			this.m_targetX = -component2.anchoredPosition.x + (float)this.m_horizontalLayoutGroup.padding.left;
			foreach (AutoCenterItem autoCenterItem in componentsInChildren)
			{
				autoCenterItem.SetCentered(false);
			}
			this.m_centeredItem = componentsInChildren[num2];
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

		public void ForceSetCenteredItemIndex(int itemIndex)
		{
			AutoCenterItem[] componentsInChildren = this.m_scrollRectContentsRT.GetComponentsInChildren<AutoCenterItem>(true);
			if (itemIndex < 0 || itemIndex >= componentsInChildren.Length)
			{
				Debug.LogException(new IndexOutOfRangeException("Item index " + itemIndex + " out of range for AutoCenterScrollRect"));
				return;
			}
			if (componentsInChildren[itemIndex].IsCentered())
			{
				return;
			}
			foreach (AutoCenterItem autoCenterItem in componentsInChildren)
			{
				autoCenterItem.SetCentered(false);
			}
			this.m_centeredItem = componentsInChildren[itemIndex];
			AutoCenterItem[] componentsInChildren2 = base.GetComponentsInChildren<AutoCenterItem>();
			if (componentsInChildren2 != null && this.OnCenterComplete != null)
			{
				this.OnCenterComplete(componentsInChildren2.ToList<AutoCenterItem>().IndexOf(this.m_centeredItem));
			}
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
}
