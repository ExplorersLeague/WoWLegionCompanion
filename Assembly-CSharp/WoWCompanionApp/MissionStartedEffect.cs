using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class MissionStartedEffect : MonoBehaviour
	{
		private void Awake()
		{
			this.m_toastCanvasGroup.alpha = 0f;
			this.m_toastCanvasGroup.blocksRaycasts = false;
			this.m_toastCanvasGroup.interactable = false;
			this.m_glowCanvasGroup.alpha = 0f;
			this.m_glowCanvasGroup.blocksRaycasts = false;
			this.m_glowCanvasGroup.interactable = false;
			this.m_missionStartedLabel.font = GeneralHelpers.LoadFancyFont();
			this.m_missionStartedLabel.text = StaticDB.GetString("MISSION_STARTED", "Mission Started PH");
		}

		private void Start()
		{
			this.m_rootRT.localScale = Vector3.one;
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Phase1",
				"delay",
				0.6f,
				"from",
				0f,
				"to",
				1f,
				"easeType",
				"easeOutCubic",
				"time",
				0.75f,
				"onupdate",
				"Phase1Update",
				"oncomplete",
				"Phase1Complete"
			}));
		}

		private void Phase1Update(float val)
		{
			this.m_rootRT.anchoredPosition = new Vector2(this.m_rootRT.anchoredPosition.x, this.m_loweredHeight + val * (this.m_raisedHeight - this.m_loweredHeight));
			this.m_toastCanvasGroup.alpha = Mathf.Min(val / 0.6f, 1f);
		}

		private void Phase1Complete()
		{
			this.m_rootRT.anchoredPosition = new Vector2(this.m_rootRT.anchoredPosition.x, this.m_raisedHeight);
			this.m_toastCanvasGroup.alpha = 1f;
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Phase2",
				"delay",
				0.5f,
				"from",
				1f,
				"to",
				0f,
				"easeType",
				"easeOutCubic",
				"time",
				0.5f,
				"onupdate",
				"Phase2Update",
				"oncomplete",
				"Phase2Complete"
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Phase3",
				"delay",
				0.5f,
				"from",
				0f,
				"to",
				3.14159274f,
				"easeType",
				iTween.EaseType.easeInOutCubic,
				"time",
				1f,
				"onupdate",
				"Phase3Update",
				"oncomplete",
				"Phase3Complete"
			}));
		}

		private void Phase2Update(float val)
		{
			this.m_rootRT.anchoredPosition = new Vector2(this.m_rootRT.anchoredPosition.x, this.m_loweredHeight + val * (this.m_raisedHeight - this.m_loweredHeight));
			this.m_toastCanvasGroup.alpha = val;
			this.m_rootRT.localScale = (0.5f + val * 0.5f) * Vector3.one;
		}

		private void Phase2Complete()
		{
			this.m_rootRT.anchoredPosition = new Vector2(this.m_rootRT.anchoredPosition.x, this.m_loweredHeight);
			this.m_toastCanvasGroup.alpha = 0f;
			this.m_rootRT.localScale = 0.5f * Vector3.one;
		}

		private void Phase3Update(float val)
		{
			this.m_glowCanvasGroup.alpha = Mathf.Sin(val);
		}

		private void Phase3Complete()
		{
			Object.DestroyObject(base.gameObject);
		}

		private void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				Object.DestroyObject(base.gameObject);
			}
		}

		public CanvasGroup m_toastCanvasGroup;

		public RectTransform m_rootRT;

		public float m_raisedHeight;

		public float m_loweredHeight;

		public Text m_missionStartedLabel;

		public CanvasGroup m_glowCanvasGroup;

		public Image m_glowImage;
	}
}
