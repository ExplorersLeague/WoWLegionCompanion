using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class CommunityInviteButton : MonoBehaviour
	{
		public void SetInviteForButton(CommunityPendingInvite pendingInvite)
		{
			this.m_communityInvite = pendingInvite;
			this.m_headerText.text = MobileClient.FormatString(StaticDB.GetString("COMMUNITIES_INVITIED_BY", "COMMUNITIES_INVITIED_BY"), pendingInvite.Inviter);
			this.m_communityNameText.text = this.m_communityInvite.CommunityName;
			this.m_communityIcon.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, (int)((pendingInvite.AvatarId != 0u) ? pendingInvite.AvatarId : ((uint)StaticDB.communityIconDB.GetRecord(1).IconFileID)));
		}

		public void AcceptInvite()
		{
			this.m_communityInvite.AcceptInvite();
		}

		public void DeclineInvite()
		{
			this.m_communityInvite.DeclineInvite();
		}

		public void OpenCommunityDescriptionDialog()
		{
			Singleton<DialogFactory>.Instance.CreateCommunityDescriptionDialog(this.m_communityInvite);
		}

		public Text m_communityNameText;

		public Text m_headerText;

		private CommunityPendingInvite m_communityInvite;

		public Image m_communityIcon;
	}
}
