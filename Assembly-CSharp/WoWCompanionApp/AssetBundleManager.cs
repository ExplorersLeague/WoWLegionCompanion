using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace WoWCompanionApp
{
	public class AssetBundleManager : Singleton<AssetBundleManager>
	{
		public Version LatestVersion { get; set; }

		public bool ForceUpgrade { get; set; }

		public string AppStoreUrl { get; set; }

		public string AppStoreUrl_CN { get; set; }

		private void Start()
		{
			this.LatestVersion = new Version(0, 0, 0);
			this.ForceUpgrade = false;
			this.m_locale = MobileDeviceLocale.GetBestGuessForLocale();
			this.InitAssetBundleManager();
		}

		public bool IsInitialized()
		{
			return AssetBundleManager.s_initialized;
		}

		private void InitAssetBundleManager()
		{
			if (AssetBundleManager.s_initialized)
			{
				return;
			}
			base.StartCoroutine(this.InternalInitAssetBundleManager());
		}

		private IEnumerator InternalInitAssetBundleManager()
		{
			string dataErrorTitle = this.GetDataErrorTitleText();
			string dataErrorDescription = this.GetDataErrorDescriptionText();
			this.m_assetServerURL = this.GetRemoteAssetPath();
			bool redirect = true;
			HashSet<string> previousURLs = new HashSet<string>();
			while (redirect)
			{
				if (previousURLs.Contains(this.m_assetServerURL))
				{
					Debug.Log("Error: Caught in redirect loop searching for data bundles.");
					GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.DataErrorPopupDisabled));
					Singleton<Login>.instance.LoginUI.ShowGenericPopup(dataErrorTitle, dataErrorDescription);
					redirect = false;
					yield break;
				}
				previousURLs.Add(this.m_assetServerURL);
				Debug.Log("Checking for redirect.txt at: " + this.m_assetServerURL + "redirect.txt");
				using (WWW www = new WWW(this.m_assetServerURL + "redirect.txt"))
				{
					yield return www;
					if (www.error == null)
					{
						if (Uri.IsWellFormedUriString(www.text, UriKind.Absolute))
						{
							this.m_assetServerURL = www.text;
							if (!this.m_assetServerURL.EndsWith("/"))
							{
								this.m_assetServerURL += "/";
							}
							Debug.Log("Found redirect to " + this.m_assetServerURL);
						}
						else
						{
							redirect = false;
							Debug.Log("End of redirect chain due to non-URI content: " + www.text);
						}
					}
					else
					{
						redirect = false;
						Debug.Log("End of redirect chain");
					}
				}
			}
			string manifestURL = this.m_assetServerURL + this.m_platform;
			Debug.Log("Fetching manifest from " + manifestURL);
			using (WWW www2 = new WWW(manifestURL))
			{
				yield return www2;
				if (www2.error != null)
				{
					Debug.Log("Error: Could not get manifest bundle: " + www2.error);
					GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.DataErrorPopupDisabled));
					Singleton<Login>.instance.LoginUI.ShowGenericPopup(dataErrorTitle, dataErrorDescription);
					yield break;
				}
				AssetBundle assetBundle = www2.assetBundle;
				this.m_manifest = assetBundle.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
				if (this.m_manifest == null)
				{
					Debug.Log("Error: Could not load AssetBundleManifest from manifest bundle.");
					GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.DataErrorPopupDisabled));
					Singleton<Login>.instance.LoginUI.ShowGenericPopup(dataErrorTitle, dataErrorDescription);
					yield break;
				}
			}
			IEnumerable<string> bundlesToDownload = from bundle in this.m_manifest.GetAllAssetBundles()
			where bundle.IndexOf('.') == -1 || bundle.EndsWith(this.$this.m_locale, StringComparison.OrdinalIgnoreCase)
			select bundle;
			this.m_numDownloads = bundlesToDownload.Count<string>();
			this.m_numCompleteDownloads = 0;
			foreach (string bundleName in bundlesToDownload)
			{
				yield return base.StartCoroutine(this.LoadAssetBundle(bundleName));
			}
			IEnumerable<AssetBundleManager.BundleName> missing = from name in AssetBundleManager.BundleName.RequiredBundleNames
			where !this.$this.m_assetBundles.ContainsKey(name)
			select name;
			if (missing.Count<AssetBundleManager.BundleName>() > 0)
			{
				foreach (AssetBundleManager.BundleName bundleName2 in missing)
				{
					Debug.LogError("Required bundle is null: " + bundleName2);
				}
				GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.DataErrorPopupDisabled));
				Singleton<Login>.instance.LoginUI.ShowGenericPopup(dataErrorTitle, dataErrorDescription);
				yield break;
			}
			Singleton<StaticDB>.instance.InitDBs(this.m_assetBundles[AssetBundleManager.BundleName.NonLocalizedDBBundleName], this.m_assetBundles[AssetBundleManager.BundleName.LocalizedDBBundleName]);
			if (this.m_assetBundles[AssetBundleManager.BundleName.NonLocalizedDBBundleName] == null)
			{
				this.m_assetBundles.Remove(AssetBundleManager.BundleName.NonLocalizedDBBundleName);
			}
			else
			{
				this.m_assetBundles[AssetBundleManager.BundleName.NonLocalizedDBBundleName].Unload(true);
			}
			if (this.m_assetBundles[AssetBundleManager.BundleName.LocalizedDBBundleName] == null)
			{
				this.m_assetBundles.Remove(AssetBundleManager.BundleName.LocalizedDBBundleName);
			}
			else
			{
				this.m_assetBundles[AssetBundleManager.BundleName.LocalizedDBBundleName].Unload(true);
			}
			yield return base.StartCoroutine(this.FetchLatestVersion(this.m_assetServerURL + "update.txt"));
			AssetBundleManager.s_initialized = true;
			if (this.InitializedAction != null)
			{
				this.InitializedAction();
			}
			this.m_numCompleteDownloads = 0;
			this.m_numDownloads = 0;
			yield break;
		}

		private void DataErrorPopupDisabled()
		{
			GenericPopup.DisabledAction = (Action)Delegate.Remove(GenericPopup.DisabledAction, new Action(this.DataErrorPopupDisabled));
			Application.Quit();
		}

		private IEnumerator LoadAssetBundle(string fileName)
		{
			if (fileName != null)
			{
				while (!Caching.ready)
				{
					yield return null;
				}
				string url = this.m_assetServerURL + fileName;
				Hash128 hash = this.m_manifest.GetAssetBundleHash(fileName);
				if (!Caching.IsVersionCached(url, hash))
				{
					Debug.Log("File " + fileName + " not cached. Will now load new file " + fileName);
					if (Singleton<Login>.instance.LoginUI != null)
					{
						Singleton<Login>.instance.LoginUI.ShowDownloadingPanel(true);
					}
				}
				using (this.m_currentWebRequest = UnityWebRequest.GetAssetBundle(url, hash, 0u))
				{
					yield return this.m_currentWebRequest.SendWebRequest();
					if (this.m_currentWebRequest.isNetworkError || this.m_currentWebRequest.isHttpError)
					{
						Caching.ClearCache();
						Debug.Log("LoadAssetBundle: Error: " + this.m_currentWebRequest.error);
					}
					else
					{
						AssetBundle content = DownloadHandlerAssetBundle.GetContent(this.m_currentWebRequest);
						this.m_numCompleteDownloads++;
						if (content == null)
						{
							Caching.ClearCache();
							Debug.Log("LoadAssetBundle: null bundle: " + fileName);
						}
						else
						{
							if (fileName.EndsWith("." + this.m_locale, StringComparison.OrdinalIgnoreCase))
							{
								fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
							}
							this.m_assetBundles.Add(fileName, content);
						}
					}
				}
				this.m_currentWebRequest = null;
				yield break;
			}
			Debug.Log("LoadAssetBundle: Error, file identifier " + fileName + " is unknown.");
			yield break;
		}

		public static AssetBundle PortraitIcons
		{
			get
			{
				return Singleton<AssetBundleManager>.Instance.m_assetBundles[AssetBundleManager.BundleName.PortraitBundleName];
			}
		}

		public static AssetBundle Icons
		{
			get
			{
				return Singleton<AssetBundleManager>.Instance.m_assetBundles[AssetBundleManager.BundleName.IconBundleName];
			}
		}

		public static T LoadAsset<T>(AssetBundleManager.BundleName bundleName, string assetPath) where T : Object
		{
			return (!Singleton<AssetBundleManager>.Instance.m_assetBundles.ContainsKey(bundleName)) ? ((T)((object)null)) : Singleton<AssetBundleManager>.Instance.m_assetBundles[bundleName].LoadAsset<T>(assetPath);
		}

		public float GetDownloadProgress()
		{
			return ((float)this.m_numCompleteDownloads + ((this.m_currentWebRequest == null) ? 0f : this.m_currentWebRequest.downloadProgress)) / (float)this.m_numDownloads;
		}

		public bool IsDevAssetBundles()
		{
			return false;
		}

		private IEnumerator FetchLatestVersion(string url)
		{
			this.LatestVersion = new Version(0, 0, 0);
			this.ForceUpgrade = false;
			string versionText = null;
			using (WWW www = new WWW(url))
			{
				yield return www;
				if (www.error == null)
				{
					versionText = www.text;
				}
			}
			this.ParseVersionFile(versionText);
			yield break;
		}

		private bool ParseVersionFile(string versionText)
		{
			if (versionText == null)
			{
				return false;
			}
			char[] separator = new char[]
			{
				'\r',
				'\n'
			};
			string[] array = versionText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2)
			{
				return false;
			}
			try
			{
				this.LatestVersion = new Version(array[0]);
				this.ForceUpgrade = Convert.ToBoolean(array[1]);
			}
			catch (Exception)
			{
				return false;
			}
			if (array.Length >= 3)
			{
				this.AppStoreUrl = array[2];
			}
			else
			{
				this.AppStoreUrl = null;
			}
			if (array.Length >= 4)
			{
				this.AppStoreUrl_CN = array[3];
			}
			else
			{
				this.AppStoreUrl_CN = null;
			}
			return true;
		}

		public void UpdateVersion()
		{
			string text = this.m_assetServerURL + "update.txt";
			Debug.Log("Fetching latest version of " + text);
			this.LatestVersion = new Version();
			this.ForceUpgrade = false;
			string versionText = null;
			float timeSinceLevelLoad = Time.timeSinceLevelLoad;
			using (WWW www = new WWW(text))
			{
				while (!www.isDone && (double)Time.timeSinceLevelLoad < (double)timeSinceLevelLoad + 5.0)
				{
				}
				if (www.error == null)
				{
					versionText = www.text;
				}
			}
			this.ParseVersionFile(versionText);
		}

		private string GetDataErrorTitleText()
		{
			string locale = this.m_locale;
			switch (locale)
			{
			case "enUS":
				return "Data Error";
			case "koKR":
				return "데이터 오류";
			case "frFR":
				return "Erreur de données";
			case "deDE":
				return "Datenfehler";
			case "zhCN":
				return "数据错误";
			case "zhTW":
				return "資料錯誤";
			case "esES":
				return "Error de datos";
			case "esMX":
				return "Error de datos";
			case "ruRU":
				return "Ошибка в данных";
			case "ptBR":
				return "Erro de dados";
			case "itIT":
				return "Errore di caricamento dati";
			}
			return "Data Error";
		}

		private string GetDataErrorDescriptionText()
		{
			string locale = this.m_locale;
			switch (locale)
			{
			case "enUS":
				return "Unable to load data from device.";
			case "koKR":
				return "기기에서 데이터를 불러올 수 없습니다.\t";
			case "frFR":
				return "Impossible de charger les données depuis l’appareil.";
			case "deDE":
				return "Gerätedaten konnten nicht geladen werden.";
			case "zhCN":
				return "无法从设备中读取数据。";
			case "zhTW":
				return "無法從裝置上讀取資料。";
			case "esES":
				return "No se han podido cargar los datos del dispositivo.";
			case "esMX":
				return "No se pueden cargar los datos del dispositivo";
			case "ruRU":
				return "Невозможно загрузить данные с устройства.";
			case "ptBR":
				return "Não foi possível carregar os dados a partir do dispositivo.";
			case "itIT":
				return "Impossibile caricare i dati dal dispositivo.";
			}
			return "Unable to load data from device.";
		}

		private string GetRemoteAssetPath()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("http://").Append((!(Singleton<Login>.instance.GetBnPortal() == "cn")) ? this.m_assetServerIpAddress : this.m_assetServerIpAddress_CN).Append("/falcon/");
			stringBuilder.Append("d").Append(string.Format("{0:D5}", MobileBuild.GetBuildNum()));
			stringBuilder.Append("/").Append(this.m_assetBundleDirectory).Append("/");
			stringBuilder.Append(this.m_platform).Append("/");
			return stringBuilder.ToString();
		}

		private static bool s_initialized;

		private string m_assetServerIpAddress = "blzddist2-a.akamaihd.net";

		private string m_assetServerIpAddress_CN = "client02.pdl.wow.battlenet.com.cn";

		private Dictionary<AssetBundleManager.BundleName, AssetBundle> m_assetBundles = new Dictionary<AssetBundleManager.BundleName, AssetBundle>();

		private string m_assetServerURL;

		private AssetBundleManifest m_manifest;

		public Action InitializedAction;

		private UnityWebRequest m_currentWebRequest;

		private int m_numDownloads;

		private int m_numCompleteDownloads;

		private const string m_versionFile = "update.txt";

		private string m_assetBundleDirectory = "ab";

		private string m_platform = "a";

		private string m_locale;

		public sealed class BundleName
		{
			private BundleName(string name)
			{
				this.m_name = name;
			}

			public static implicit operator string(AssetBundleManager.BundleName bundleName)
			{
				return bundleName.m_name;
			}

			public static implicit operator AssetBundleManager.BundleName(string name)
			{
				return new AssetBundleManager.BundleName(name);
			}

			public static bool operator ==(AssetBundleManager.BundleName name1, AssetBundleManager.BundleName name2)
			{
				return name1.m_name == name2.m_name;
			}

			public static bool operator !=(AssetBundleManager.BundleName name1, AssetBundleManager.BundleName name2)
			{
				return name1.m_name != name2.m_name;
			}

			public override bool Equals(object obj)
			{
				return base.GetType() == obj.GetType() && this.m_name == (obj as AssetBundleManager.BundleName).m_name;
			}

			public bool Equals(AssetBundleManager.BundleName bundleName)
			{
				return this.m_name == bundleName.m_name;
			}

			public override int GetHashCode()
			{
				return this.m_name.GetHashCode();
			}

			public static AssetBundleManager.BundleName IconBundleName = new AssetBundleManager.BundleName("icn");

			public static AssetBundleManager.BundleName PortraitBundleName = new AssetBundleManager.BundleName("picn");

			public static AssetBundleManager.BundleName MainSceneBundleName = new AssetBundleManager.BundleName("main");

			public static AssetBundleManager.BundleName NonLocalizedDBBundleName = new AssetBundleManager.BundleName("staticdb-common");

			public static AssetBundleManager.BundleName LocalizedDBBundleName = new AssetBundleManager.BundleName("staticdb-local");

			public static AssetBundleManager.BundleName ConfigBundleName = new AssetBundleManager.BundleName("config");

			public static AssetBundleManager.BundleName[] RequiredBundleNames = new AssetBundleManager.BundleName[]
			{
				AssetBundleManager.BundleName.MainSceneBundleName,
				AssetBundleManager.BundleName.IconBundleName,
				AssetBundleManager.BundleName.PortraitBundleName,
				AssetBundleManager.BundleName.NonLocalizedDBBundleName,
				AssetBundleManager.BundleName.LocalizedDBBundleName,
				AssetBundleManager.BundleName.ConfigBundleName
			};

			private string m_name;
		}
	}
}
