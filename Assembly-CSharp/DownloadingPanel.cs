using System;
using UnityEngine;
using UnityEngine.UI;

public class DownloadingPanel : MonoBehaviour
{
	private void Start()
	{
		this.m_downloadText.font = GeneralHelpers.LoadStandardFont();
		string locale = Main.instance.GetLocale();
		string text = locale;
		switch (text)
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
		this.m_progressBarFillImage.fillAmount = AssetBundleManager.instance.GetDownloadProgress();
	}

	public Image m_progressBarFillImage;

	public Text m_downloadText;
}
