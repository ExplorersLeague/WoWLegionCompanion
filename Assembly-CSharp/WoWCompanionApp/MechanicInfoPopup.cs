using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class MechanicInfoPopup : MonoBehaviour
	{
		private void OnEnable()
		{
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void OnDisable()
		{
			Main.instance.m_backButtonManager.PopBackAction();
		}

		public Image m_mechanicIcon;

		public Text m_mechanicName;

		public Text m_mechanicDescription;
	}
}
