using System;
using bnet.protocol;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class BnGameAccountButton : MonoBehaviour
	{
		public void SetInfo(EntityId gameAccount, string accountName, bool isBanned, bool isSuspended)
		{
			this.m_gameAccount = gameAccount;
			this.m_accountName = accountName;
			this.m_buttonText.text = this.m_accountName;
		}

		public void OnClick()
		{
			Singleton<Login>.instance.SelectGameAccount(this.m_gameAccount);
		}

		public void PlayClickSound()
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
		}

		public Text m_buttonText;

		private EntityId m_gameAccount;

		private string m_accountName;
	}
}
