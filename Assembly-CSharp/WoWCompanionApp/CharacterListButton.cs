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
			int? factionGroup = StaticDB.factionTemplateDB.GetFactionGroup((int)this.m_characterEntry.RaceID);
			if (this.m_backgroundImage != null && factionGroup != null)
			{
				if (((factionGroup == null) ? null : new int?(factionGroup.GetValueOrDefault() & 4)) != 0)
				{
					this.m_backgroundImage.sprite = this.m_hordeBackground;
				}
				else if (((factionGroup == null) ? null : new int?(factionGroup.GetValueOrDefault() & 2)) != 0)
				{
					this.m_backgroundImage.sprite = this.m_allianceBackground;
				}
				else
				{
					this.m_backgroundImage.sprite = this.m_neutralBackground;
				}
			}
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

		public Image m_backgroundImage;

		public Sprite m_allianceBackground;

		public Sprite m_hordeBackground;

		public Sprite m_neutralBackground;
	}
}
