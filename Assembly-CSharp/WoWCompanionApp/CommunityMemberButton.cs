using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityMemberButton : MonoBehaviour
	{
		public void PopulateMemberInfo(CommunityMember member)
		{
			this.m_memberInfo = member;
			this.m_characterName.text = this.m_memberInfo.Name;
			this.m_classImage.sprite = GeneralHelpers.LoadClassIcon(this.m_memberInfo.Class);
			this.m_buttonText.SetNewStringKey(this.m_memberInfo.ConvertRoleToString());
			this.SetButtonState(this.m_memberInfo.GetAssignableRoles().Count > 0);
			this.SetRoleIconVisibility();
		}

		public void OpenMemberSettings()
		{
			GameObject gameObject = Main.instance.AddChildToLevel3Canvas(this.m_memberSettingsPrefab);
			MemberSettingsPanel component = gameObject.GetComponent<MemberSettingsPanel>();
			component.SetMemberInfo(this.m_memberInfo);
		}

		private void SetButtonState(bool state)
		{
			this.m_buttonFrame.SetActive(state);
			this.m_rankButton.interactable = state;
		}

		private void SetRoleIconVisibility()
		{
			ClubRoleIdentifier role = this.m_memberInfo.Role;
			this.m_moderatorImage.SetActive(role == 3);
			this.m_leaderImage.SetActive(role == 2 || role == 1);
		}

		public GameObject m_memberSettingsPrefab;

		public Text m_characterName;

		public Button m_rankButton;

		public GameObject m_buttonFrame;

		public LocalizedText m_buttonText;

		public Image m_classImage;

		public GameObject m_moderatorImage;

		public GameObject m_leaderImage;

		private CommunityMember m_memberInfo;
	}
}
