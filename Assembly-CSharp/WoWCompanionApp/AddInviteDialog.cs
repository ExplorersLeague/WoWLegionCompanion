using System;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class AddInviteDialog : BaseDialog
	{
		public EventInviteResponseDialog EventDialog { get; set; }

		private void Update()
		{
			this.m_okButton.interactable = Calendar.CanSendInvite();
		}

		public void OnClickOK()
		{
			Calendar.EventInvite(this.m_inputField.text);
			this.EventDialog.UpdateInvitesList();
			this.CloseDialog();
		}

		public InputField m_inputField;

		public Button m_okButton;
	}
}
