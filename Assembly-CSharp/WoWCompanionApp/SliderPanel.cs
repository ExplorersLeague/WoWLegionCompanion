using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WoWCompanionApp
{
	public class SliderPanel : MonoBehaviour
	{
		private void OnEnable()
		{
			this.m_busyMoving = false;
			this.m_movementIsPending = false;
			this.m_isShowing = false;
		}

		private void Update()
		{
			if (this.m_movementIsPending && !this.m_busyMoving)
			{
				this.m_movementIsPending = false;
				if (this.m_pendingMovementIsToShowPreview)
				{
					this.ShowSliderPanel();
				}
				else if (this.m_pendingMovementIsToMaximize)
				{
					this.MaximizeSliderPanel();
				}
				else if (this.m_pendingMovementIsToHide)
				{
					this.HideSliderPanel();
				}
			}
		}

		public void OnBeginDrag(BaseEventData eventData)
		{
			if (this.SliderPanelBeginDragAction != null)
			{
				this.SliderPanelBeginDragAction();
			}
			this.m_startingPos = base.transform.localPosition;
			Vector2 startingPointerPos = AdventureMapPanel.instance.ScreenPointToLocalPointInMapViewRT(((PointerEventData)eventData).position);
			this.m_startingPointerPos = startingPointerPos;
			this.m_busyMoving = false;
			this.m_movementIsPending = false;
			iTween.Stop(base.gameObject);
		}

		public void OnDrag(BaseEventData eventData)
		{
			if (!this.m_isHorizontal)
			{
				float num = AdventureMapPanel.instance.ScreenPointToLocalPointInMapViewRT(((PointerEventData)eventData).position).y - this.m_startingPointerPos.y + this.m_startingPos.y;
				float num2 = (float)this.m_missionPanelSliderFullHeight / 2f;
				num = Mathf.Min(num, num2);
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, num, base.transform.localPosition.z);
				RectTransform component = base.GetComponent<RectTransform>();
				if (this.m_stretchAbovePreviewHight)
				{
					if (num > (float)this.m_missionPanelSliderPreviewHeight - num2)
					{
						Vector2 sizeDelta = component.sizeDelta;
						sizeDelta.y = num2 + num;
						component.sizeDelta = sizeDelta;
					}
					else
					{
						Vector2 sizeDelta2 = component.sizeDelta;
						sizeDelta2.y = (float)this.m_missionPanelSliderPreviewHeight;
						component.sizeDelta = sizeDelta2;
					}
				}
			}
			else
			{
				float num3 = AdventureMapPanel.instance.ScreenPointToLocalPointInMapViewRT(((PointerEventData)eventData).position).x - this.m_startingPointerPos.x + this.m_startingPos.x;
				float num4 = (float)this.m_missionPanelSliderFullWidth / 2f;
				num3 = Mathf.Min(num3, num4);
				base.transform.localPosition = new Vector3(num3, base.transform.localPosition.y, base.transform.localPosition.z);
				RectTransform component2 = base.GetComponent<RectTransform>();
				if (this.m_stretchAbovePreviewWidth)
				{
					if (num3 > (float)this.m_missionPanelSliderPreviewWidth - num4)
					{
						Vector2 sizeDelta3 = component2.sizeDelta;
						sizeDelta3.x = num4 + num3;
						component2.sizeDelta = sizeDelta3;
					}
					else
					{
						Vector2 sizeDelta4 = component2.sizeDelta;
						sizeDelta4.x = (float)this.m_missionPanelSliderPreviewWidth;
						component2.sizeDelta = sizeDelta4;
					}
				}
			}
		}

		private void PanelSliderBottomTweenCallback(Vector2 val)
		{
			RectTransform component = base.GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(val.x, val.y);
			if (!this.m_isHorizontal)
			{
				if (this.m_stretchAbovePreviewHight)
				{
					if (val.y > (float)this.m_missionPanelSliderPreviewHeight)
					{
						Vector2 sizeDelta = component.sizeDelta;
						sizeDelta.y = val.y;
						component.sizeDelta = sizeDelta;
					}
					else
					{
						Vector2 sizeDelta2 = component.sizeDelta;
						sizeDelta2.y = (float)this.m_missionPanelSliderPreviewHeight;
						component.sizeDelta = sizeDelta2;
					}
				}
			}
			else if (this.m_stretchAbovePreviewWidth)
			{
				if (val.x > (float)this.m_missionPanelSliderPreviewWidth)
				{
					Vector2 sizeDelta3 = component.sizeDelta;
					sizeDelta3.x = val.x;
					component.sizeDelta = sizeDelta3;
				}
				else
				{
					Vector2 sizeDelta4 = component.sizeDelta;
					sizeDelta4.x = (float)this.m_missionPanelSliderPreviewWidth;
					component.sizeDelta = sizeDelta4;
				}
			}
			this.m_startingPos = val;
			if (this.m_hidePreviewWhenMaximized && !this.m_isHorizontal)
			{
				float num = (val.y - (float)this.m_missionPanelSliderPreviewHeight) / (float)(this.m_missionPanelSliderFullHeight - this.m_missionPanelSliderPreviewHeight);
				if (this.m_previewCanvasGroup != null)
				{
					this.m_previewCanvasGroup.alpha = 1f - num;
				}
				if (this.m_maximizedCanvasGroup != null)
				{
					this.m_maximizedCanvasGroup.alpha = num;
				}
			}
		}

		private void DisableSliderPanel()
		{
			if (this.m_masterCanvasGroup != null)
			{
				this.m_masterCanvasGroup.alpha = 0f;
			}
			this.m_busyMoving = false;
			this.m_isShowing = false;
			if (this.SliderPanelFinishMinimizeAction != null)
			{
				this.SliderPanelFinishMinimizeAction();
			}
		}

		private void OnDoneSlidingInPreview()
		{
			this.m_busyMoving = false;
			this.m_isShowing = true;
			if (this.m_hidePreviewWhenMaximized && !this.m_isHorizontal && this.m_previewCanvasGroup != null)
			{
				this.m_previewCanvasGroup.blocksRaycasts = true;
			}
		}

		private void OnDoneSlidingInMaximize()
		{
			this.m_busyMoving = false;
			this.m_isShowing = true;
			if (this.m_hidePreviewWhenMaximized && !this.m_isHorizontal)
			{
				if (this.m_previewCanvasGroup != null)
				{
					this.m_previewCanvasGroup.alpha = 0f;
					this.m_previewCanvasGroup.blocksRaycasts = false;
				}
				if (this.m_maximizedCanvasGroup != null)
				{
					this.m_maximizedCanvasGroup.alpha = 1f;
				}
			}
			if (this.SliderPanelMaximizedAction != null)
			{
				this.SliderPanelMaximizedAction();
			}
		}

		public void MissionPanelSlider_HandleAutopositioning_Bottom()
		{
			float num = 5f;
			RectTransform component = base.GetComponent<RectTransform>();
			if (!this.m_isHorizontal)
			{
				if (this.m_startingPos.y < (float)this.m_missionPanelSliderPreviewHeight + num)
				{
					if (component.anchoredPosition.y < (float)this.m_missionPanelSliderPreviewHeight - num)
					{
						this.HideSliderPanel();
						return;
					}
					this.MaximizeSliderPanel();
					return;
				}
			}
			else if (this.m_startingPos.x < (float)this.m_missionPanelSliderPreviewWidth + num)
			{
				if (component.anchoredPosition.x < (float)this.m_missionPanelSliderPreviewWidth - num)
				{
					this.HideSliderPanel();
					return;
				}
				this.MaximizeSliderPanel();
				return;
			}
			if (this.SliderPanelBeginShrinkToPreviewPositionAction != null)
			{
				this.SliderPanelBeginShrinkToPreviewPositionAction();
			}
			this.ShowSliderPanel();
		}

		public void MaximizeSliderPanel()
		{
			if (this.m_busyMoving)
			{
				this.m_movementIsPending = true;
				this.m_pendingMovementIsToShowPreview = false;
				this.m_pendingMovementIsToMaximize = true;
				this.m_pendingMovementIsToHide = false;
				return;
			}
			this.m_busyMoving = true;
			if (this.m_masterCanvasGroup != null)
			{
				this.m_masterCanvasGroup.alpha = 1f;
			}
			Vector2 vector;
			vector..ctor((float)this.m_missionPanelSliderFullWidth, (float)(this.m_missionPanelSliderFullHeight + this.m_startingVerticalOffset));
			Vector2 anchoredPosition = base.GetComponent<RectTransform>().anchoredPosition;
			anchoredPosition.y += (float)this.m_startingVerticalOffset;
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Slide Mission Details In (Bottom)",
				"from",
				anchoredPosition,
				"to",
				vector,
				"easeType",
				"easeOutCubic",
				"time",
				(this.m_maximizeSeconds <= 0f) ? 0.5f : this.m_maximizeSeconds,
				"onupdate",
				"PanelSliderBottomTweenCallback",
				"oncomplete",
				"OnDoneSlidingInMaximize"
			}));
		}

		public void ShowSliderPanel()
		{
			if (this.m_busyMoving)
			{
				this.m_movementIsPending = true;
				this.m_pendingMovementIsToShowPreview = true;
				this.m_pendingMovementIsToMaximize = false;
				this.m_pendingMovementIsToHide = false;
				return;
			}
			this.m_busyMoving = true;
			if (this.m_masterCanvasGroup != null)
			{
				this.m_masterCanvasGroup.alpha = 1f;
			}
			Vector2 vector;
			vector..ctor((float)this.m_missionPanelSliderPreviewWidth, (float)this.m_missionPanelSliderPreviewHeight);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Slide Mission Details In (Bottom)",
				"from",
				base.GetComponent<RectTransform>().anchoredPosition,
				"to",
				vector,
				"easeType",
				"easeOutCubic",
				"time",
				(this.m_previewSeconds <= 0f) ? 0.5f : this.m_previewSeconds,
				"onupdate",
				"PanelSliderBottomTweenCallback",
				"oncomplete",
				"OnDoneSlidingInPreview"
			}));
		}

		public void HideSliderPanel()
		{
			if (this.m_busyMoving)
			{
				this.m_movementIsPending = true;
				this.m_pendingMovementIsToShowPreview = false;
				this.m_pendingMovementIsToMaximize = false;
				this.m_pendingMovementIsToHide = true;
				return;
			}
			this.m_busyMoving = true;
			if (this.SliderPanelBeginMinimizeAction != null)
			{
				this.SliderPanelBeginMinimizeAction();
			}
			Vector2 vector;
			vector..ctor(0f, (float)this.m_startingVerticalOffset);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Slide Mission Details Out (Bottom)",
				"from",
				base.GetComponent<RectTransform>().anchoredPosition,
				"to",
				vector,
				"easeType",
				"easeOutCubic",
				"time",
				(this.m_hideSeconds <= 0f) ? 0.5f : this.m_hideSeconds,
				"onupdate",
				"PanelSliderBottomTweenCallback",
				"oncomplete",
				"DisableSliderPanel"
			}));
		}

		public bool IsShowing()
		{
			return this.m_isShowing;
		}

		public bool IsBusyMoving()
		{
			return this.m_busyMoving;
		}

		public float m_previewSeconds;

		public float m_maximizeSeconds;

		public float m_hideSeconds;

		public bool m_isHorizontal;

		private Vector2 m_startingPos;

		private Vector2 m_startingPointerPos;

		public int m_startingVerticalOffset;

		public int m_missionPanelSliderPreviewHeight;

		public int m_missionPanelSliderFullHeight;

		public bool m_stretchAbovePreviewHight;

		public int m_missionPanelSliderPreviewWidth;

		public int m_missionPanelSliderFullWidth;

		public bool m_stretchAbovePreviewWidth;

		public bool m_hidePreviewWhenMaximized;

		public CanvasGroup m_masterCanvasGroup;

		public CanvasGroup m_previewCanvasGroup;

		public CanvasGroup m_maximizedCanvasGroup;

		public Action SliderPanelMaximizedAction;

		public Action SliderPanelBeginMinimizeAction;

		public Action SliderPanelFinishMinimizeAction;

		public Action SliderPanelBeginDragAction;

		public Action SliderPanelBeginShrinkToPreviewPositionAction;

		private bool m_busyMoving;

		private bool m_movementIsPending;

		private bool m_pendingMovementIsToShowPreview;

		private bool m_pendingMovementIsToMaximize;

		private bool m_pendingMovementIsToHide;

		private bool m_isShowing;
	}
}
