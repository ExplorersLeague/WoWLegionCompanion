using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class HamburgerMenu : MonoBehaviour
	{
		private void Start()
		{
			SlidingPanel slidingPanel = this.m_slidingPanel;
			slidingPanel.m_closedAction = (Action)Delegate.Combine(slidingPanel.m_closedAction, new Action(this.HidePanel));
			this.m_characterName.text = Singleton<CharacterData>.instance.CharacterName.ToUpper();
			this.m_viewCharacterObj.SetActive(!Singleton<Login>.instance.GetBnPortal().Equals("cn", StringComparison.OrdinalIgnoreCase));
			if (this.m_viewCharacterText != null)
			{
				this.m_viewCharacterText.text = StaticDB.GetString("SETTINGS_VIEW_CHARACTER", "[PH]VIEW CHARACTER").Replace("&s", Singleton<CharacterData>.instance.CharacterName.ToUpper());
			}
		}

		private void HidePanel()
		{
			base.gameObject.SetActive(false);
		}

		public void CloseMenu()
		{
			if (!this.m_slidingPanel.IsSliding())
			{
				this.m_slidingPanel.SlideOut();
				Main.instance.m_backButtonManager.PopBackAction();
				base.GetComponent<Image>().enabled = false;
				Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			}
		}

		public void PlayClickSound()
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
		}

		public void OpenMenu()
		{
			if (!this.m_slidingPanel.IsSliding())
			{
				base.gameObject.SetActive(true);
				this.m_slidingPanel.SlideIn();
				Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideHamburgerMenu, null);
				base.GetComponent<Image>().enabled = true;
				Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			}
		}

		public void OnLogoutButtonClicked()
		{
			Singleton<Login>.Instance.ReturnToCharacterList = false;
			Singleton<Login>.Instance.ReturnToRecentCharacter = false;
			Singleton<Login>.Instance.ReturnToTitleScene();
		}

		public void ShowCharacterList()
		{
			Singleton<Login>.Instance.ReturnToCharacterList = true;
			Singleton<Login>.Instance.ReturnToTitleScene();
		}

		public void ShowSettingsDialog()
		{
			Singleton<DialogFactory>.Instance.CreateAppSettingsDialog();
		}

		public void ShowCharacterPanel()
		{
			CharacterViewPanel panel = Singleton<DialogFactory>.Instance.CreateCharacterViewPanel();
			Main.instance.m_backButtonManager.PushBackAction(delegate
			{
				panel.DestroyPanel();
			});
		}

		public SlidingPanel m_slidingPanel;

		public Text m_characterName;

		public LocalizedText m_viewCharacterText;

		public GameObject m_viewCharacterObj;
	}
}
