using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace WoWCompanionApp
{
	public class PendingInviteDialog : MonoBehaviour
	{
		private void Awake()
		{
			CommunityData.OnInviteRefresh += this.RefreshInviteList;
			this.RefreshInviteList();
		}

		private void OnDestroy()
		{
			CommunityData.OnInviteRefresh -= this.RefreshInviteList;
		}

		private void RefreshInviteList()
		{
			this.m_contentPane.DetachAllChildren();
			ReadOnlyCollection<CommunityPendingInvite> pendingInvites = CommunityData.Instance.GetPendingInvites();
			if (pendingInvites.Count == 0)
			{
				base.GetComponent<BaseDialog>().CloseDialog();
				return;
			}
			foreach (CommunityPendingInvite inviteForButton in pendingInvites)
			{
				GameObject gameObject = this.m_contentPane.AddAsChildObject(this.m_inviteButtonPrefab);
				gameObject.GetComponent<CommunityInviteButton>().SetInviteForButton(inviteForButton);
			}
		}

		public GameObject m_inviteButtonPrefab;

		public GameObject m_contentPane;
	}
}
