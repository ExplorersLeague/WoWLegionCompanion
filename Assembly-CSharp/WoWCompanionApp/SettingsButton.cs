using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class SettingsButton : MonoBehaviour
	{
		private void Start()
		{
		}

		public void OnClick()
		{
			Singleton<DialogFactory>.Instance.CreateAppSettingsDialog();
		}
	}
}
