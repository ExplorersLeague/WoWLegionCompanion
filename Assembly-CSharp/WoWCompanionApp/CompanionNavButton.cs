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

		public void PlayComingSoonEffect()
		{
			if (this.m_currentComingSoonEffect == null && this.m_comingSoonEffectPrefab != null && this.m_comingSoonPivot != null)
			{
				this.m_currentComingSoonEffect = Object.Instantiate<GameObject>(this.m_comingSoonEffectPrefab);
				this.m_currentComingSoonEffect.transform.SetParent(this.m_comingSoonPivot.transform, false);
				this.m_currentComingSoonEffect.transform.localPosition = Vector3.zero;
			}
		}

		public GameObject m_selectedImage;

		public GameObject m_notSelectedImage;

		public GameObject m_notificationImage;

		public GameObject m_comingSoonEffectPrefab;

		public GameObject m_comingSoonPivot;

		private GameObject m_currentComingSoonEffect;

		private bool m_isSelected;
	}
}
