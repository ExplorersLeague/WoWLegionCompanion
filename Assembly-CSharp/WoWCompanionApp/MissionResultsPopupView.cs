using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class MissionResultsPopupView : MonoBehaviour
	{
		public void OnEnable()
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideMissionResults, null);
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}
	}
}
