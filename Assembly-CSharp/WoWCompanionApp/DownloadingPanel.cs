using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class DownloadingPanel : MonoBehaviour
	{
		private void Start()
		{
			string locale = Main.instance.GetLocale();
			switch (locale)
			{
			case "koKR":
				this.m_downloadText.text = "다운로드 중...";
				break;
			case "frFR":
				this.m_downloadText.text = "Téléchargement…";
				break;
			case "deDE":
				this.m_downloadText.text = "Lade herunter...";
				break;
			case "zhCN":
				this.m_downloadText.text = "下载中……";
				break;
			case "zhTW":
				this.m_downloadText.text = "下載中...";
				break;
			case "esES":
				this.m_downloadText.text = "Descargando...";
				break;
			case "esMX":
				this.m_downloadText.text = "Descargando...";
				break;
			case "ruRU":
				this.m_downloadText.text = "Загрузка...";
				break;
			case "ptBR":
				this.m_downloadText.text = "Baixando...";
				break;
			case "itIT":
				this.m_downloadText.text = "Download...";
				break;
			}
		}

		private void Update()
		{
			this.m_progressBarFillImage.fillAmount = Singleton<AssetBundleManager>.instance.GetDownloadProgress();
		}

		public Image m_progressBarFillImage;

		public Text m_downloadText;
	}
}
