using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CharacterViewPanel : MonoBehaviour
	{
		public int TopMargin { get; private set; }

		private void Start()
		{
			this.webView = base.GetComponentInChildren<CharacterWebView>(true);
			this.companionMultiPanel = Object.FindObjectOfType<CompanionMultiPanel>();
			Transform transform = this.companionMultiPanel.transform.Find("CompanionNavigationBar");
			float num = 0f;
			num += (transform.Find("CharacterImagePanel").transform as RectTransform).rect.height;
			this.TopMargin = Mathf.FloorToInt(num * transform.GetComponentInParent<Canvas>().scaleFactor);
			this.hamburgerMenuButton = transform.Find("HamburgerButtonHolder").Find("HamburgerMenuButton").GetComponent<Button>();
			this.backButton = Object.Instantiate<GameObject>(this.hamburgerMenuButton.gameObject, this.hamburgerMenuButton.gameObject.transform.parent, false);
			(this.backButton.GetComponent<Button>().targetGraphic as Image).sprite = this.backArrowSprite;
			this.backButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			this.backButton.GetComponent<Button>().onClick.AddListener(new UnityAction(this.DestroyPanel));
			this.backButton.SetActive(false);
			this.ShowWebView();
		}

		public void ShowWebView()
		{
			this.SetSpinnerActive(true);
			this.hamburgerMenuButton.interactable = false;
			this.hamburgerMenuButton.gameObject.SetActive(false);
			this.startTime = Time.timeSinceLevelLoad;
			this.isLoading = true;
		}

		private void Update()
		{
			if (Time.timeSinceLevelLoad > this.startTime + (float)this.timeoutSeconds && this.isLoading)
			{
				this.OnWebViewLoaded(false);
			}
		}

		public void SetSpinnerActive(bool active)
		{
			if (this.background != null)
			{
				this.background.gameObject.SetActive(active);
			}
			if (this.spinner != null)
			{
				this.spinner.SetActive(active);
			}
		}

		public void OnWebViewLoaded(bool success)
		{
			this.webView.SetWebViewVisible(success);
			if (success)
			{
				this.backButton.SetActive(true);
				this.hamburgerMenuButton.gameObject.SetActive(false);
			}
			else
			{
				this.DestroyPanel();
				Singleton<Login>.Instance.LoginUI.ShowGenericPopupFull(StaticDB.GetString(this.errorKey, "Error loading character"));
			}
			this.SetSpinnerActive(false);
			this.isLoading = false;
		}

		private void OnDestroy()
		{
			this.backButton.SetActive(false);
			this.hamburgerMenuButton.gameObject.SetActive(true);
			this.hamburgerMenuButton.interactable = true;
		}

		public void DestroyPanel()
		{
			Main.instance.m_backButtonManager.PopBackAction();
			Object.Destroy(base.gameObject);
			Object.Destroy(this.backButton.gameObject);
			this.isLoading = true;
		}

		public GameObject spinner;

		public Sprite backArrowSprite;

		public Image background;

		public string errorKey;

		public int timeoutSeconds;

		private CharacterWebView webView;

		private CompanionMultiPanel companionMultiPanel;

		private Button hamburgerMenuButton;

		private GameObject backButton;

		private float startTime;

		private bool isLoading;
	}
}
