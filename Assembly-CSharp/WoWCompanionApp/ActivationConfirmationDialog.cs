using System;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class ActivationConfirmationDialog : BaseDialog
	{
		public void Start()
		{
		}

		public void Show(FollowerDetailView followerDetailView)
		{
			base.gameObject.SetActive(true);
			this.m_followerDetailView = followerDetailView;
			if (GarrisonStatus.Gold() < 250)
			{
				this.m_okButtonLabel.text = StaticDB.GetString("CANT_AFFORD", null);
				this.m_okButton.interactable = false;
			}
			else
			{
				this.m_okButton.interactable = true;
			}
			this.m_activationsRemainingText.text = GarrisonStatus.GetRemainingFollowerActivations().ToString();
			this.m_activationCostText.text = GarrisonStatus.GetFollowerActivationGoldCost().ToString();
		}

		public void ConfirmActivate()
		{
			this.m_followerDetailView.ActivateFollower();
			base.gameObject.SetActive(false);
		}

		public Text m_okButtonLabel;

		public Button m_okButton;

		public Text m_activationsRemainingText;

		public Text m_activationCostText;

		private FollowerDetailView m_followerDetailView;
	}
}
