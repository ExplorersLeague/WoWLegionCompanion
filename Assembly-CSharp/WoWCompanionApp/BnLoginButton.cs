using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class BnLoginButton : MonoBehaviour
	{
		private void Start()
		{
			this.m_loginButtonNameText.font = GeneralHelpers.LoadStandardFont();
			this.m_numCharactersText.font = GeneralHelpers.LoadStandardFont();
		}

		public void SetInfo(ulong realmAddress, string realmName, string subRegion, int characterCount, bool enabled)
		{
			this.m_realmAddress = realmAddress;
			this.m_realmName = realmName;
			this.m_subRegion = subRegion;
			this.m_characterCount = characterCount;
			this.m_enabled = enabled;
			this.UpdateOnlineStatus();
		}

		private void UpdateOnlineStatus()
		{
			if (this.m_enabled)
			{
				this.m_loginButtonNameText.text = this.m_realmName;
				this.m_loginButtonNameText.color = BnLoginButton.s_enabledColor;
				this.m_numCharactersText.text = string.Empty + this.m_characterCount;
				this.m_numCharactersText.color = BnLoginButton.s_enabledCharacterCountColor;
			}
			else
			{
				this.m_loginButtonNameText.text = this.m_realmName + " (" + StaticDB.GetString("OFFLINE", null) + ")";
				this.m_loginButtonNameText.color = BnLoginButton.s_disabledColor;
				this.m_numCharactersText.text = string.Empty + this.m_characterCount;
				this.m_numCharactersText.color = BnLoginButton.s_disabledColor;
			}
			Button component = base.GetComponent<Button>();
			component.interactable = this.m_enabled;
			if (this.m_enabled)
			{
				this.m_realmStatusIcon.sprite = Resources.Load<Sprite>("NewLoginPanel/Realm_StatusGreen");
			}
			else
			{
				this.m_realmStatusIcon.sprite = Resources.Load<Sprite>("NewLoginPanel/Realm_StatusRed");
			}
		}

		public void OnClick()
		{
			Singleton<Login>.instance.SendRealmJoinRequest(this.m_realmName, this.m_realmAddress, this.m_subRegion);
		}

		public ulong GetRealmAddress()
		{
			return this.m_realmAddress;
		}

		public void SetCharacterCount(int characterCount)
		{
			this.m_characterCount = characterCount;
		}

		public void SetOnline(bool online)
		{
			this.m_enabled = online;
			this.UpdateOnlineStatus();
		}

		public void PlayClickSound()
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
		}

		public Text m_loginButtonNameText;

		public Text m_numCharactersText;

		public Image m_realmStatusIcon;

		private ulong m_realmAddress;

		private string m_realmName;

		private string m_subRegion;

		private int m_characterCount;

		private bool m_enabled;

		private static Color s_disabledColor = new Color(0.3f, 0.3f, 0.3f);

		private static Color s_enabledColor = new Color(1f, 1f, 1f);

		private static Color s_enabledCharacterCountColor = new Color(1f, 0.86f, 0f);
	}
}
