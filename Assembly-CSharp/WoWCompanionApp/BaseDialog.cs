using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class BaseDialog : MonoBehaviour
	{
		public void OnEnable()
		{
			Main.instance.m_UISound.Play_ShowGenericTooltip();
			if (this.canvasLevel == LegacyCanvasLevel.Level2Canvas)
			{
				Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			}
			if (this.canvasLevel == LegacyCanvasLevel.Level3Canvas)
			{
				Main.instance.m_canvasBlurManager.AddBlurRef_Level2Canvas();
			}
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void OnDisable()
		{
			if (this.canvasLevel == LegacyCanvasLevel.Level2Canvas)
			{
				Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			}
			if (this.canvasLevel == LegacyCanvasLevel.Level3Canvas)
			{
				Main.instance.m_canvasBlurManager.RemoveBlurRef_Level2Canvas();
			}
			Main.instance.m_backButtonManager.PopBackAction();
		}

		public void CloseDialog()
		{
			Object.Destroy(base.gameObject);
		}

		public LegacyCanvasLevel canvasLevel;
	}
}
