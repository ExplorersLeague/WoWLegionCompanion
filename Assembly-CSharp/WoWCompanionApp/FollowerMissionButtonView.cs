using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class FollowerMissionButtonView : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
		}

		public void ShowMissionList()
		{
			Main.instance.allPanels.ShowAdventureMap();
		}

		public Text followerMissionToggleButtonText;
	}
}
