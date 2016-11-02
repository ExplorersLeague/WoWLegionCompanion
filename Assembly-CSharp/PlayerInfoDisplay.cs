using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoDisplay : MonoBehaviour
{
	private void InitPlayerDisplay(int playerLevel)
	{
		this.m_characterName.text = GarrisonStatus.CharacterName();
		if (Main.instance.GetLocale() == "frFR")
		{
			this.m_characterClassName.text = string.Concat(new string[]
			{
				GarrisonStatus.CharacterClassName(),
				" ",
				StaticDB.GetString("LEVEL", null),
				" ",
				playerLevel.ToString()
			});
		}
		else
		{
			this.m_characterClassName.text = GeneralHelpers.TextOrderString(StaticDB.GetString("LEVEL", null), playerLevel.ToString()) + " " + GarrisonStatus.CharacterClassName();
		}
		this.m_characterListButton.text = StaticDB.GetString("CHARACTER_LIST", null);
		Sprite sprite = GeneralHelpers.LoadClassIcon(GarrisonStatus.CharacterClassID());
		if (sprite != null)
		{
			this.m_classIcon.sprite = sprite;
		}
	}

	private void OnEnable()
	{
		this.InitPlayerDisplay(GarrisonStatus.CharacterLevel());
		Main instance = Main.instance;
		instance.PlayerLeveledUpAction = (Action<int>)Delegate.Combine(instance.PlayerLeveledUpAction, new Action<int>(this.HandlePlayerLeveledUp));
	}

	private void OnDisable()
	{
		Main instance = Main.instance;
		instance.PlayerLeveledUpAction = (Action<int>)Delegate.Remove(instance.PlayerLeveledUpAction, new Action<int>(this.HandlePlayerLeveledUp));
	}

	private void HandlePlayerLeveledUp(int newLevel)
	{
		this.InitPlayerDisplay(newLevel);
	}

	public void ToggleRecentCharacterPanel()
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		this.m_recentCharactersPanel.SetActive(!this.m_recentCharactersPanel.activeSelf);
	}

	public void HideRecentCharacterPanel()
	{
		this.m_recentCharactersPanel.SetActive(false);
	}

	public Text m_characterName;

	public Text m_characterClassName;

	public Text m_characterListButton;

	public Image m_classIcon;

	public GameObject m_recentCharactersPanel;
}
