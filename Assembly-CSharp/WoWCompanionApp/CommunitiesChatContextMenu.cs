using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class CommunitiesChatContextMenu : MonoBehaviour
	{
		public void SetContextByMessage(CommunityChatMessage message)
		{
			this.m_message = message;
			this.m_destroyButton.SetActive(this.m_message.CanBeDestroyed());
			this.m_reportButton.SetActive(!this.m_message.CreatedBySelf() && ReportSystem.CanReportPlayer(this.m_message.GetAsPlayerLocation()));
			this.m_divider.SetActive(this.m_destroyButton.activeSelf && this.m_reportButton.activeSelf);
		}

		public void DeleteMessage()
		{
			this.m_message.DeleteMessage();
		}

		public void OpenReportPlayerDialog()
		{
			ReportPlayerDialog reportPlayerDialog = Singleton<DialogFactory>.instance.CreateReportPlayerDialog();
			reportPlayerDialog.InitializeReportDialog(this.m_message);
		}

		public GameObject m_destroyButton;

		public GameObject m_reportButton;

		public GameObject m_divider;

		private CommunityChatMessage m_message;
	}
}
