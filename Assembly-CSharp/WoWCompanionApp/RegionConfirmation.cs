using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class RegionConfirmation : MonoBehaviour
	{
		private void OnEnable()
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}

		public void OnClickOkay()
		{
			Singleton<Login>.instance.SetRegionIndex();
		}

		public void OnClickCancel()
		{
			Singleton<Login>.instance.CancelRegionIndex();
		}
	}
}
