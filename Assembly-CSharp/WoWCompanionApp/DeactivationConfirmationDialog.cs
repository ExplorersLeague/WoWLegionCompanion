using System;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class DeactivationConfirmationDialog : BaseDialog
	{
		private void Start()
		{
		}

		public void Show(FollowerDetailView followerDetailView)
		{
			base.gameObject.SetActive(true);
			this.m_followerDetailView = followerDetailView;
			this.m_reactivationCostText.text = GarrisonStatus.GetFollowerActivationGoldCost().ToString();
		}

		public void ConfirmDeactivate()
		{
			this.m_followerDetailView.DeactivateFollower();
			base.gameObject.SetActive(false);
		}

		public Text m_reactivationCostText;

		private FollowerDetailView m_followerDetailView;
	}
}
