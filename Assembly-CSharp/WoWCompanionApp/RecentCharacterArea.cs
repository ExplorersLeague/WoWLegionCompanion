using System;
using UnityEngine;
using WowJamMessages;

namespace WoWCompanionApp
{
	public class RecentCharacterArea : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
		}

		public void SetRecentCharacter(int index, RecentCharacter recentChar)
		{
			if (index < 0 || index >= this.m_charButtons.Length)
			{
				Debug.Log("SetRecentCharacter: invalid index " + index);
				return;
			}
			if (recentChar != null)
			{
				this.m_charButtons[index].gameObject.SetActive(true);
				this.m_charButtons[index].SetRecentCharacter(recentChar);
			}
			else
			{
				this.m_charButtons[index].gameObject.SetActive(false);
			}
		}

		public RecentCharacterButton[] m_charButtons;
	}
}
