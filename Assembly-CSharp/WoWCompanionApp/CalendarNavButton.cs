using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CalendarNavButton : CompanionNavButton
	{
		private void Start()
		{
			base.InitializeButtonState(true);
		}

		private void Update()
		{
			if (this.tensDigit != null && this.onesDigit != null && this.singleDigit != null)
			{
				int num = DateTime.Now.Day / 10;
				int num2 = DateTime.Now.Day % 10;
				this.tensDigit.sprite = this.digitTextures[num];
				this.onesDigit.sprite = this.digitTextures[num2];
				this.singleDigit.sprite = this.digitTextures[num2];
				this.tensDigit.gameObject.SetActive(num > 0);
				this.onesDigit.gameObject.SetActive(num > 0);
				this.singleDigit.gameObject.SetActive(num == 0);
			}
		}

		private void OnEnable()
		{
			Calendar.OnCalendarUpdatePendingInvites += new Calendar.CalendarUpdatePendingInvitesHandler(this.OnUpdatePendingInvites<Calendar.CalendarUpdatePendingInvitesEvent>);
		}

		private void OnDisable()
		{
			Calendar.OnCalendarUpdatePendingInvites -= new Calendar.CalendarUpdatePendingInvitesHandler(this.OnUpdatePendingInvites<Calendar.CalendarUpdatePendingInvitesEvent>);
		}

		private void OnUpdatePendingInvites<T>(T eventArgs)
		{
			this.UpdateNotificationState();
		}

		protected override void UpdateNotificationState()
		{
			if (this.notification != null)
			{
				this.notification.gameObject.SetActive(Calendar.GetNumPendingInvites() > 0u);
			}
		}

		protected override void UpdateSelectedState()
		{
			base.UpdateSelectedState();
			if (this.tensDigit != null && this.onesDigit != null && this.singleDigit != null && this.m_selectedImage != null && this.m_notSelectedImage != null)
			{
				this.tensDigit.transform.SetParent((!this.m_selectedImage.activeInHierarchy) ? this.m_notSelectedImage.transform : this.m_selectedImage.transform);
				this.onesDigit.transform.SetParent((!this.m_selectedImage.activeInHierarchy) ? this.m_notSelectedImage.transform : this.m_selectedImage.transform);
				this.singleDigit.transform.SetParent((!this.m_selectedImage.activeInHierarchy) ? this.m_notSelectedImage.transform : this.m_selectedImage.transform);
			}
		}

		public Image tensDigit;

		public Image onesDigit;

		public Image singleDigit;

		public Sprite[] digitTextures;

		public Image notification;
	}
}
