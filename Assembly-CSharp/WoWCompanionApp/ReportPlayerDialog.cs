using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class ReportPlayerDialog : MonoBehaviour
	{
		public void InitializeReportDialog(CommunityChatMessage message)
		{
			this.m_reportedMessage = message;
			this.m_headerText.text = MobileClient.FormatString(StaticDB.GetString("REPORT_PLAYER_FOR", "[PH]Report " + message.Author + ":"), message.Author);
		}

		private void Awake()
		{
			this.m_spammingToggle.onValueChanged.AddListener(delegate
			{
				this.AssignReason(this.m_spammingToggle, this.REPORT_SPAM_STRING);
			});
			this.m_nameToggle.onValueChanged.AddListener(delegate
			{
				this.AssignReason(this.m_nameToggle, this.REPORT_PLAYERNAME_STRING);
			});
			this.m_languageToggle.onValueChanged.AddListener(delegate
			{
				this.AssignReason(this.m_languageToggle, this.REPORT_LANGUAGE_STRING);
			});
			this.m_cheatingToggle.onValueChanged.AddListener(delegate
			{
				this.AssignReason(this.m_cheatingToggle, this.REPORT_CHEATING_STRING);
			});
			Image component = this.m_reportPlayerButton.GetComponent<Image>();
			component.material = new Material(this.m_grayscaleShader);
			component.material.SetFloat("_GrayscaleAmount", 1f);
			Text componentInChildren = this.m_reportPlayerButton.GetComponentInChildren<Text>();
			componentInChildren.color = Color.gray;
			MeshGradient component2 = componentInChildren.GetComponent<MeshGradient>();
			component2.enabled = false;
			Button component3 = this.m_reportPlayerButton.GetComponent<Button>();
			component3.interactable = false;
		}

		private void EnableReportButton()
		{
			Image component = this.m_reportPlayerButton.GetComponent<Image>();
			component.material.SetFloat("_GrayscaleAmount", 0f);
			Text componentInChildren = this.m_reportPlayerButton.GetComponentInChildren<Text>();
			componentInChildren.color = this.ACTIVE_TEXT_COLOR;
			MeshGradient component2 = componentInChildren.GetComponent<MeshGradient>();
			component2.enabled = true;
			Button component3 = this.m_reportPlayerButton.GetComponent<Button>();
			component3.interactable = true;
		}

		private void AssignReason(Toggle toggle, string reportReason)
		{
			if (toggle.isOn)
			{
				this.m_reasonForReport = reportReason;
				this.EnableReportButton();
			}
		}

		public void SendOffReport()
		{
			uint num = ReportSystem.InitiateReportPlayer(this.m_reasonForReport, new PlayerLocation?(this.m_reportedMessage.GetAsPlayerLocation()));
			ReportSystem.SendReportPlayer(num, this.m_inputField.text);
		}

		public Text m_headerText;

		public GameObject m_reportPlayerButton;

		public Toggle m_spammingToggle;

		public Toggle m_nameToggle;

		public Toggle m_languageToggle;

		public Toggle m_cheatingToggle;

		public InputField m_inputField;

		public Shader m_grayscaleShader;

		private Color ACTIVE_TEXT_COLOR = new Color(1f, 0.8588f, 0f, 1f);

		private CommunityChatMessage m_reportedMessage;

		private string m_reasonForReport;

		private readonly string REPORT_SPAM_STRING = "spam";

		private readonly string REPORT_LANGUAGE_STRING = "language";

		private readonly string REPORT_CHEATING_STRING = "cheater";

		private readonly string REPORT_PLAYERNAME_STRING = "badplayername";
	}
}
