using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class AppSettingsDialog : MonoBehaviour
	{
		public void SyncWithOptions()
		{
			this.m_enableSFX.isOn = Main.instance.m_UISound.IsSFXEnabled();
			this.m_enableNotifications.isOn = Main.instance.m_enableNotifications;
		}

		private void Start()
		{
			this.SyncWithOptions();
			this.AdjustForNotch();
		}

		private void Update()
		{
			foreach (TiledRandomTexture tiledRandomTexture in base.GetComponentsInChildren<TiledRandomTexture>())
			{
				(tiledRandomTexture.transform as RectTransform).rect.height = 16f;
			}
		}

		private void OnEnable()
		{
			Main.instance.m_UISound.Play_ShowGenericTooltip();
			Main.instance.m_backButtonManager.PushBackAction(delegate
			{
				this.DestroyDialog();
			});
		}

		private void OnDisable()
		{
			Main.instance.m_backButtonManager.PopBackAction();
		}

		public void OnValueChanged_EnableSFX(bool isOn)
		{
			Main.instance.m_UISound.EnableSFX(isOn);
			Main.instance.m_UISound.Play_ButtonBlackClick();
			SecurePlayerPrefs.SetString("EnableSFX", isOn.ToString().ToLower(), Main.uniqueIdentifier);
		}

		public void OnValueChanged_EnableNotifications(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			Main.instance.m_enableNotifications = isOn;
			SecurePlayerPrefs.SetString("EnableNotifications", isOn.ToString().ToLower(), Main.uniqueIdentifier);
		}

		public void DestroyDialog()
		{
			Object.Destroy(base.gameObject);
		}

		private void AdjustForNotch()
		{
			if (Main.instance.IsIphoneX())
			{
				Vector2 sizeDelta = this.m_headerBar.sizeDelta;
				sizeDelta.y += 40f;
				this.m_headerBar.sizeDelta = sizeDelta;
			}
		}

		public Toggle m_enableSFX;

		public Toggle m_enableNotifications;

		public Dictionary<TiledRandomTexture, float> HeightDictionary = new Dictionary<TiledRandomTexture, float>();

		public RectTransform m_headerBar;
	}
}
