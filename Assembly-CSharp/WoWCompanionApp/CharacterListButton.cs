using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.JSONRealmList;

namespace WoWCompanionApp
{
	public class CharacterListButton : MonoBehaviour
	{
		private void Start()
		{
			this.m_characterName.font = GeneralHelpers.LoadStandardFont();
			this.m_characterLevel.font = GeneralHelpers.LoadStandardFont();
			this.m_subtext.font = GeneralHelpers.LoadStandardFont();
		}

		private void Update()
		{
		}

		public void SetGUID(string guid)
		{
		}

		public void CharacterSelected()
		{
			Singleton<Login>.instance.SelectCharacterNew(this.m_characterEntry, this.m_subRegion);
		}

		public void PlayClickSound()
		{
			Main.instance.m_UISound.Play_DefaultNavClick();
		}

		public Text m_characterName;

		public Text m_characterLevel;

		public Text m_subtext;

		public Image m_characterClassIcon;

		public JamJSONCharacterEntry m_characterEntry;

		public string m_subRegion;
	}
}
