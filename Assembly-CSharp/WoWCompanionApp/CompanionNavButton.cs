using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class CompanionNavButton : MonoBehaviour
	{
		private void Start()
		{
			this.InitializeButtonState(false);
		}

		protected void InitializeButtonState(bool state)
		{
			this.m_isSelected = false;
			Main instance = Main.instance;
			instance.CompanionNavButtonSelectionAction = (Action<CompanionNavButton>)Delegate.Combine(instance.CompanionNavButtonSelectionAction, new Action<CompanionNavButton>(this.HandleCompanionNavButtonSelected));
			this.UpdateSelectedState();
			this.UpdateNotificationState();
		}

		private void HandleCompanionNavButtonSelected(CompanionNavButton navButton)
		{
			this.m_isSelected = (navButton == this);
			this.UpdateSelectedState();
		}

		public void SelectMe()
		{
			Main.instance.SelectCompanionNavButton(this);
		}

		private void UpdateSelectedState()
		{
			if (this.m_selectedImage && this.m_notSelectedImage)
			{
				this.m_selectedImage.SetActive(this.m_isSelected);
				this.m_notSelectedImage.SetActive(!this.m_isSelected);
			}
		}

		protected virtual void UpdateNotificationState()
		{
		}

		public GameObject m_selectedImage;

		public GameObject m_notSelectedImage;

		public GameObject m_notificationImage;

		private bool m_isSelected;
	}
}
