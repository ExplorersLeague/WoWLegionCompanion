using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityInviteButton : MonoBehaviour
	{
		public void SetInviteForButton(CommunityPendingInvite pendingInvite)
		{
			this.m_communityInvite = pendingInvite;
			this.m_communityNameText.text = this.m_communityInvite.CommunityName;
		}

		public void AcceptInvite()
		{
			this.m_communityInvite.AcceptInvite();
		}

		public void DeclineInvite()
		{
			this.m_communityInvite.DeclineInvite();
		}

		public Text m_communityNameText;

		private CommunityPendingInvite m_communityInvite;
	}
}
