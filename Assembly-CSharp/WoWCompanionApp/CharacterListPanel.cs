using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CharacterListPanel : MonoBehaviour
	{
		private void Start()
		{
			this.m_titleText.font = FontLoader.LoadFancyFont();
			this.m_titleText.text = StaticDB.GetString("CHARACTER_SELECTION", null);
			if (this.m_cancelText)
			{
				this.m_cancelText.font = FontLoader.LoadStandardFont();
				this.m_cancelText.text = StaticDB.GetString("LOG_OUT", null);
			}
		}

		public Text m_titleText;

		public Text m_cancelText;

		public CharacterListView m_characterListView;
	}
}
