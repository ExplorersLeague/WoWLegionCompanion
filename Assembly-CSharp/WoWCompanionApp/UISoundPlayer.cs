using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class UISoundPlayer : MonoBehaviour
	{
		public void PlaySound(string soundName)
		{
			Main.instance.m_UISound.PlayUISound(soundName);
		}
	}
}
