using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class MemberSettingsPanel : MonoBehaviour
	{
		private void Awake()
		{
			CommunityData.OnRosterRefresh += this.RefreshPage;
			Club.OnClubRemoved += new Club.ClubRemovedHandler(this.OnClubRemoved);
			Club.OnClubMemberRemoved += new Club.ClubMemberRemovedHandler(this.OnMemberRemoved);
		}

		private void OnDestroy()
		{
			CommunityData.OnRosterRefresh -= this.RefreshPage;
			Club.OnClubRemoved -= new Club.ClubRemovedHandler(this.OnClubRemoved);
			Club.OnClubMemberRemoved -= new Club.ClubMemberRemovedHandler(this.OnMemberRemoved);
		}

		public void RefreshPage(ulong clubID)
		{
			if (this.m_member.ClubID == clubID)
			{
				this.SetRoleToggleState();
				this.SetValidAssignableRoles();
			}
		}

		public void SetMemberInfo(CommunityMember member)
		{
			this.m_characterNameText.text = member.Name;
			this.m_kickButtonText.text = MobileClient.FormatString(StaticDB.GetString("COMMUNITIES_KICK_MEMBER", "[PH] Kick %s"), this.m_characterNameText.text);
			this.m_member = member;
			this.SetRoleToggleState();
			this.SetValidAssignableRoles();
			this.m_memberRole.onValueChanged.AddListener(delegate
			{
				this.SetMemberRole();
			});
			this.m_moderatorRole.onValueChanged.AddListener(delegate
			{
				this.SetModeratorRole();
			});
			this.m_leaderRole.onValueChanged.AddListener(delegate
			{
				this.SetLeaderRole();
			});
			this.m_ownerRole.onValueChanged.AddListener(delegate
			{
				this.SetOwnerRole();
			});
		}

		public void KickMember()
		{
			this.m_member.KickMember();
			base.GetComponent<BaseDialog>().CloseDialog();
		}

		private void SetMemberRole()
		{
			this.AssignRole(this.m_memberRole, 4);
		}

		private void SetModeratorRole()
		{
			this.AssignRole(this.m_moderatorRole, 3);
		}

		private void SetLeaderRole()
		{
			this.AssignRole(this.m_leaderRole, 2);
		}

		private void SetOwnerRole()
		{
			this.AssignRole(this.m_ownerRole, 1);
		}

		private void AssignRole(Toggle toggle, ClubRoleIdentifier newRole)
		{
			if (toggle.isOn)
			{
				this.m_member.AssignRole(newRole);
			}
		}

		private void SetRoleToggleState()
		{
			switch (this.m_member.Role)
			{
			case 1:
				this.m_ownerRole.isOn = true;
				break;
			case 2:
				this.m_leaderRole.isOn = true;
				break;
			case 3:
				this.m_moderatorRole.isOn = true;
				break;
			default:
				this.m_memberRole.isOn = true;
				break;
			}
		}

		private void SetValidAssignableRoles()
		{
			this.ResetOptionInteractability();
			List<ClubRoleIdentifier> assignableRoles = this.m_member.GetAssignableRoles();
			foreach (ClubRoleIdentifier interactabilityByRole in assignableRoles)
			{
				this.SetInteractabilityByRole(interactabilityByRole);
			}
			if (this.m_member.IsKickable())
			{
				this.m_kickButton.SetActive(true);
			}
		}

		private void SetInteractabilityByRole(ClubRoleIdentifier role)
		{
			switch (role)
			{
			case 1:
				this.m_ownerRole.interactable = true;
				break;
			case 2:
				this.m_leaderRole.interactable = true;
				break;
			case 3:
				this.m_moderatorRole.interactable = true;
				break;
			default:
				this.m_memberRole.interactable = true;
				break;
			}
		}

		private void ResetOptionInteractability()
		{
			this.m_kickButton.SetActive(false);
			this.m_memberRole.interactable = false;
			this.m_moderatorRole.interactable = false;
			this.m_leaderRole.interactable = false;
			this.m_ownerRole.interactable = false;
		}

		private void OnClubRemoved(Club.ClubRemovedEvent clubRemovedEvent)
		{
			if (clubRemovedEvent.ClubID == this.m_member.ClubID)
			{
				base.GetComponent<BaseDialog>().CloseDialog();
			}
		}

		private void OnMemberRemoved(Club.ClubMemberRemovedEvent memberRemovedEvent)
		{
			if (memberRemovedEvent.ClubID == this.m_member.ClubID && memberRemovedEvent.MemberID == this.m_member.MemberID)
			{
				base.GetComponent<BaseDialog>().CloseDialog();
			}
		}

		public Text m_characterNameText;

		public Text m_kickButtonText;

		public Toggle m_memberRole;

		public Toggle m_moderatorRole;

		public Toggle m_leaderRole;

		public Toggle m_ownerRole;

		public GameObject m_kickButton;

		private CommunityMember m_member;
	}
}
