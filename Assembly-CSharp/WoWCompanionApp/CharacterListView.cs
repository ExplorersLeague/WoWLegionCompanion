using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.JSONRealmList;

namespace WoWCompanionApp
{
	public class CharacterListView : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
			if (!this.m_initialized && Singleton<AssetBundleManager>.instance.IsInitialized())
			{
				this.m_initialized = true;
				this.SetLevelText();
			}
		}

		public void ClearList()
		{
			CharacterListButton[] componentsInChildren = this.charListContents.transform.GetComponentsInChildren<CharacterListButton>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.transform.SetParent(null);
				Object.Destroy(componentsInChildren[i].gameObject);
			}
		}

		private void SetLevelText()
		{
			if (CharacterListView.m_levelText == null)
			{
				CharacterListView.m_levelText = StaticDB.GetString("LEVEL", null);
			}
		}

		private void OnEnable()
		{
			this.m_initialized = false;
		}

		public void CharacterSelected()
		{
			this.ClearList();
			Singleton<Login>.instance.LoginUI.ShowConnectingPanel();
		}

		public void AddCharacterButton(JamJSONCharacterEntry charData, string subRegion, string realmName, bool online)
		{
			this.m_characterEntry = charData;
			GameObject gameObject = Object.Instantiate<GameObject>(this.charListItemPrefab);
			gameObject.transform.SetParent(this.charListContents.transform, false);
			CharacterListButton component = gameObject.GetComponent<CharacterListButton>();
			component.SetGUID(charData.PlayerGuid);
			component.m_characterEntry = charData;
			component.m_subRegion = subRegion;
			Sprite sprite = GeneralHelpers.LoadClassIcon((int)charData.ClassID);
			if (sprite != null)
			{
				component.m_characterClassIcon.sprite = sprite;
			}
			component.m_characterName.text = charData.Name;
			bool flag = online;
			if (!charData.HasMobileAccess)
			{
				component.m_subtext.text = StaticDB.GetString("REQUIRES_CLASS_HALL", null);
				component.m_subtext.color = Color.red;
				flag = false;
			}
			else if (realmName == "unknown")
			{
				component.m_subtext.text = string.Empty;
				flag = false;
			}
			else
			{
				if (online)
				{
					component.m_subtext.text = realmName;
				}
				else
				{
					component.m_subtext.text = realmName + " (" + StaticDB.GetString("OFFLINE", null) + ")";
				}
				component.m_subtext.color = Color.yellow;
			}
			component.m_subtext.gameObject.SetActive(true);
			if (!flag)
			{
				Button component2 = gameObject.GetComponent<Button>();
				component2.interactable = false;
				component.m_characterName.color = Color.grey;
				component.m_characterLevel.color = Color.grey;
			}
			int num = (int)charData.ExperienceLevel;
			if (num < 1)
			{
				num = 1;
			}
			component.m_characterLevel.text = GeneralHelpers.TextOrderString(CharacterListView.m_levelText, num.ToString());
		}

		public void SortCharacterList()
		{
			CharacterListButton[] componentsInChildren = this.charListContents.transform.GetComponentsInChildren<CharacterListButton>(true);
			List<CharacterListButton> list = new List<CharacterListButton>();
			foreach (CharacterListButton item in componentsInChildren)
			{
				list.Add(item);
			}
			CharacterListView.CharacterButtonComparer comparer = new CharacterListView.CharacterButtonComparer();
			list.Sort(comparer);
			for (int j = 0; j < list.Count; j++)
			{
				foreach (CharacterListButton characterListButton in componentsInChildren)
				{
					if (characterListButton.m_characterEntry.PlayerGuid == list[j].m_characterEntry.PlayerGuid)
					{
						characterListButton.transform.SetSiblingIndex(j);
						break;
					}
				}
			}
			for (int l = 0; l < list.Count; l++)
			{
				FancyEntrance component = list[l].GetComponent<FancyEntrance>();
				component.m_timeToDelayEntrance = this.m_listItemInitialEntranceDelay + this.m_listItemEntranceDelay * (float)l;
				component.Activate();
			}
		}

		public GameObject charListItemPrefab;

		public GameObject charListContents;

		public float m_listItemInitialEntranceDelay;

		public float m_listItemEntranceDelay;

		public JamJSONCharacterEntry m_characterEntry;

		private static string m_levelText;

		private bool m_initialized;

		private class CharacterButtonComparer : IComparer<CharacterListButton>
		{
			public int Compare(CharacterListButton char1, CharacterListButton char2)
			{
				Button component = char1.gameObject.GetComponent<Button>();
				Button component2 = char2.gameObject.GetComponent<Button>();
				if (component.interactable && !component2.interactable)
				{
					return -1;
				}
				if (component2.interactable && !component.interactable)
				{
					return 1;
				}
				if (char1.m_characterEntry.ExperienceLevel != char2.m_characterEntry.ExperienceLevel)
				{
					return (int)(char2.m_characterEntry.ExperienceLevel - char1.m_characterEntry.ExperienceLevel);
				}
				return string.Compare(char1.m_characterName.text, char2.m_characterName.text);
			}
		}
	}
}
