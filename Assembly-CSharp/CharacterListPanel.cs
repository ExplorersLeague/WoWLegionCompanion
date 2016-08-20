using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListPanel : MonoBehaviour
{
	private void Start()
	{
		this.m_titleText.font = GeneralHelpers.LoadFancyFont();
		this.m_titleText.text = StaticDB.GetString("CHARACTER_SELECTION", null);
		this.m_cancelText.font = GeneralHelpers.LoadStandardFont();
		this.m_cancelText.text = StaticDB.GetString("LOG_OUT", null);
	}

	private void Update()
	{
	}

	public Text m_titleText;

	public Text m_cancelText;

	public CharacterListView m_characterListView;
}
