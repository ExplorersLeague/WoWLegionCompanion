using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class UnassignCombatAllyConfirmationDialog : MonoBehaviour
	{
		private void OnEnable()
		{
			Main.instance.m_UISound.Play_ShowGenericTooltip();
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void Start()
		{
			this.m_areYouSureLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_cancelButtonLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_okButtonLabel.font = GeneralHelpers.LoadStandardFont();
		}

		public void Show()
		{
			base.gameObject.SetActive(true);
			this.m_areYouSureLabel.text = StaticDB.GetString("ARE_YOU_SURE", null);
			this.m_cancelButtonLabel.text = StaticDB.GetString("NO", null);
			this.m_okButtonLabel.text = StaticDB.GetString("YES_UNASSIGN", "Yes, Unassign!");
		}

		public void ConfirmUnassign()
		{
			this.m_combatAllyListItem.UnassignCombatAlly();
			base.gameObject.SetActive(false);
		}

		public Text m_areYouSureLabel;

		public Text m_cancelButtonLabel;

		public Text m_okButtonLabel;

		public CombatAllyListItem m_combatAllyListItem;
	}
}
