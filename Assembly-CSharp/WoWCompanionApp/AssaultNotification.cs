using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class AssaultNotification : MonoBehaviour
	{
		private void Start()
		{
			this.m_allianceBG.SetActive(false);
			this.m_hordeBG.SetActive(false);
			this.HandleInvasionPOIChanged();
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.OnZoomOutMap = (Action)Delegate.Combine(instance.OnZoomOutMap, new Action(this.OnZoomOut));
			AdventureMapPanel instance2 = AdventureMapPanel.instance;
			instance2.OnZoomInMap = (Action)Delegate.Combine(instance2.OnZoomInMap, new Action(this.OnZoomIn));
			Singleton<GarrisonWrapper>.Instance.InvasionPOIChangedAction += this.HandleInvasionPOIChanged;
			this.OnZoomOut();
		}

		private void OnDestroy()
		{
			Singleton<GarrisonWrapper>.Instance.InvasionPOIChangedAction -= this.HandleInvasionPOIChanged;
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.OnZoomOutMap = (Action)Delegate.Remove(instance.OnZoomOutMap, new Action(this.OnZoomOut));
			AdventureMapPanel instance2 = AdventureMapPanel.instance;
			instance2.OnZoomInMap = (Action)Delegate.Remove(instance2.OnZoomInMap, new Action(this.OnZoomIn));
		}

		private void Update()
		{
			if (base.gameObject.activeSelf)
			{
				TimeSpan timeSpan = LegionfallData.GetCurrentInvasionExpirationTime() - GarrisonStatus.CurrentTime();
				if (timeSpan.TotalSeconds < 0.0)
				{
					base.gameObject.SetActive(false);
					return;
				}
				string text = StaticDB.GetString("ASSAULT_TIME_REMAINING", "[PH] %s REMAINING");
				Regex regex = new Regex(Regex.Escape("%.2d"));
				text = regex.Replace(text, "{0:D}", 1);
				text = regex.Replace(text, "{1,2:D2}", 1);
				this.m_timerText.text = string.Format(text, timeSpan.Hours, timeSpan.Minutes);
			}
		}

		private void OnZoomOut()
		{
			this.CollapseText();
		}

		private void OnZoomIn()
		{
			this.SpreadText();
		}

		private void FadeFactionCrest_Update(float alpha)
		{
			GameObject activeFactionObject = this.GetActiveCrest();
			Image image2 = activeFactionObject.GetComponentsInChildren<Image>().First((Image image) => image.gameObject != activeFactionObject);
			Color color = image2.color;
			color.a = alpha;
			image2.color = color;
		}

		private void FadeFactionCrest_Complete()
		{
			GameObject activeFactionObject = this.GetActiveCrest();
			Image image2 = activeFactionObject.GetComponentsInChildren<Image>().First((Image image) => image.gameObject != activeFactionObject);
			Color color = image2.color;
			color.a = this.m_crestAlpha;
			image2.color = color;
		}

		private void FadeCenterIcons(float alpha)
		{
			this.m_crestAlpha = alpha;
			float num = (this.m_crestAlpha != 0f) ? this.DELAY_FOR_THIRD_ANIMATION : 0f;
			GameObject activeFactionObject = this.GetActiveCrest();
			Image image2 = activeFactionObject.GetComponentsInChildren<Image>().First((Image image) => image.gameObject != activeFactionObject);
			iTween.StopByName(base.gameObject, "FadeFactionCrest");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FadeFactionCrest",
				"from",
				image2.color.a,
				"to",
				this.m_crestAlpha,
				"delay",
				num,
				"time",
				this.TIME_TO_ANIMATE,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"FadeFactionCrest_Update",
				"oncomplete",
				"FadeFactionCrest_Complete"
			}));
		}

		private void MoveHeaderText_Update(float offsetY)
		{
			Vector3 localPosition = this.m_headerText.transform.localPosition;
			localPosition.y = offsetY;
			this.m_headerText.transform.localPosition = localPosition;
		}

		private void MoveHeaderText_Complete()
		{
			Vector3 localPosition = this.m_headerText.transform.localPosition;
			localPosition.y = this.m_headerTextPosition;
			this.m_headerText.transform.localPosition = localPosition;
		}

		private void MoveTimerText_Update(float offsetY)
		{
			Vector3 localPosition = this.m_timerText.transform.localPosition;
			localPosition.y = offsetY;
			this.m_timerText.transform.localPosition = localPosition;
		}

		private void MoveTimerText_Complete()
		{
			Vector3 localPosition = this.m_timerText.transform.localPosition;
			localPosition.y = this.m_timerTextPosition;
			this.m_timerText.transform.localPosition = localPosition;
		}

		public void MoveText(float headerOffsetY, float timerOffsetY)
		{
			this.m_headerTextPosition = headerOffsetY;
			this.m_timerTextPosition = timerOffsetY;
			float num = (this.m_headerTextPosition != this.m_headerTextCollapsedPosition) ? 0f : this.DELAY_FOR_THIRD_ANIMATION;
			iTween.StopByName(base.gameObject, "MoveHeaderText");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"MoveHeaderText",
				"from",
				this.m_headerText.transform.localPosition.y,
				"to",
				this.m_headerTextPosition,
				"time",
				this.TIME_TO_ANIMATE,
				"delay",
				num,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"MoveHeaderText_Update",
				"oncomplete",
				"MoveHeaderText_Complete"
			}));
			iTween.StopByName(base.gameObject, "MoveTimerText");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"MoveTimerText",
				"from",
				this.m_timerText.transform.localPosition.y,
				"to",
				this.m_timerTextPosition,
				"time",
				this.TIME_TO_ANIMATE,
				"delay",
				this.DELAY_FOR_SECOND_ANIMATION,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"MoveTimerText_Update",
				"oncomplete",
				"MoveTimerText_Complete"
			}));
		}

		private void CollapseText()
		{
			if (LegionfallData.HasCurrentInvasionPOI())
			{
				this.FadeCenterIcons(0f);
				this.MoveText(this.m_headerTextCollapsedPosition, this.m_timerTextCollapsedPosition);
			}
		}

		private void SpreadText()
		{
			if (LegionfallData.HasCurrentInvasionPOI())
			{
				this.MoveText(this.m_headerTextSpreadPosition, this.m_timerTextSpreadPosition);
				this.FadeCenterIcons(1f);
			}
		}

		private void HandleInvasionPOIChanged()
		{
			if (LegionfallData.HasCurrentInvasionPOI())
			{
				base.gameObject.SetActive(true);
				this.m_headerText.text = ((!LegionfallData.GetCurrentInvasionPOI().IsHordeAssault()) ? StaticDB.GetString("ALLIANCE_ATTACKING", "[PH] ALLIANCE ATTACKING") : StaticDB.GetString("HORDE_ATTACKING", "[PH] HORDE ATTACKING"));
				this.GetActiveCrest().SetActive(true);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		private GameObject GetActiveCrest()
		{
			return (!LegionfallData.GetCurrentInvasionPOI().IsHordeAssault()) ? this.m_allianceBG : this.m_hordeBG;
		}

		public GameObject m_allianceBG;

		public GameObject m_hordeBG;

		public Text m_headerText;

		public Text m_timerText;

		public float m_headerTextSpreadPosition;

		public float m_headerTextCollapsedPosition;

		public float m_timerTextSpreadPosition;

		public float m_timerTextCollapsedPosition;

		private float m_headerTextPosition;

		private float m_timerTextPosition;

		private float m_crestAlpha;

		private float TIME_TO_ANIMATE = 0.4f;

		private float DELAY_FOR_SECOND_ANIMATION = 0.1f;

		private float DELAY_FOR_THIRD_ANIMATION = 0.2f;
	}
}
