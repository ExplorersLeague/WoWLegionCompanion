using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
	public int LatestVersion { get; set; }

	public bool ForceUpgrade { get; set; }

	public string AppStoreUrl { get; set; }

	public string AppStoreUrl_CN { get; set; }

	private void Awake()
	{
		if (AssetBundleManager.s_instance == null)
		{
			AssetBundleManager.s_instance = this;
			this.m_manifest = new Dictionary<string, string>();
		}
		this.LatestVersion = 0;
		this.ForceUpgrade = false;
	}

	private void Start()
	{
		this.InitAssetBundleManager();
	}

	public static AssetBundleManager instance
	{
		get
		{
			return AssetBundleManager.s_instance;
		}
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
		string assetAddress = this.m_assetServerIpAddress;
		if (Login.instance.GetBnPortal() == "cn")
		{
			assetAddress = this.m_assetServerIpAddress_CN;
		}
		this.m_assetServerURL = string.Concat(new string[]
		{
			"http://",
			assetAddress,
			"/falcon/d",
			string.Format("{0:D5}", BuildNum.DataBuildNum),
			"/",
			this.m_assetBundleDirectory,
			"/"
		});
		this.m_assetServerURL = this.m_assetServerURL + this.m_platform + "/";
		string manifestURL = this.m_assetServerURL + this.m_platform + ".manifest";
		string manifestText = null;
		Debug.Log("Fetching manifest from " + manifestURL);
		using (WWW www = new WWW(manifestURL))
		{
			yield return www;
			if (www.error == null)
			{
				manifestText = www.text;
			}
			else if (www.error.StartsWith("java.net.UnknownHostException") || www.error.StartsWith("A server with the specified hostname could not be found"))
			{
				Debug.Log("Couldn't connect to asset bundle server at " + manifestURL);
			}
			else
			{
				Debug.Log("Error: " + www.error + ", while loading manifest: " + manifestURL);
			}
		}
		if (manifestText != null)
		{
			File.WriteAllText(Application.persistentDataPath + "/" + this.m_platform + ".manifest", manifestText);
		}
		else
		{
			Debug.Log("loading manifest from local storage");
			manifestText = File.ReadAllText(Application.persistentDataPath + "/" + this.m_platform + ".manifest");
			if (manifestText == null)
			{
				throw new Exception("Error: Could not get manifest.");
			}
		}
		this.BuildManifest(manifestText);
		string localeStaticIdentifier = Main.instance.GetLocale().ToLower();
		this.m_currentWWW = null;
		this.m_priorProgress = 0f;
		this.m_progressMultiplier = 0.45f;
		this.m_progressStartTime = Time.timeSinceLevelLoad;
		yield return base.StartCoroutine(this.LoadAssetBundle("icn", delegate(AssetBundle value)
		{
			this.<>f__this.m_iconsBundle = value;
		}));
		this.m_currentWWW = null;
		this.m_priorProgress = 0.45f;
		this.m_progressMultiplier = 0.45f;
		this.m_progressStartTime = Time.timeSinceLevelLoad;
		yield return base.StartCoroutine(this.LoadAssetBundle("picn", delegate(AssetBundle value)
		{
			this.<>f__this.m_portraitIconsBundle = value;
		}));
		AssetBundle genericStaticDB = null;
		AssetBundle localizedStaticDB = null;
		this.m_currentWWW = null;
		this.m_priorProgress = 0.9f;
		this.m_progressMultiplier = 0.05f;
		this.m_progressStartTime = Time.timeSinceLevelLoad;
		yield return base.StartCoroutine(this.LoadAssetBundle("gnrc", delegate(AssetBundle value)
		{
			this.<genericStaticDB>__5 = value;
		}));
		this.m_currentWWW = null;
		this.m_priorProgress = 0.95f;
		this.m_progressMultiplier = 0.05f;
		this.m_progressStartTime = Time.timeSinceLevelLoad;
		yield return base.StartCoroutine(this.LoadAssetBundle(localeStaticIdentifier, delegate(AssetBundle value)
		{
			this.<localizedStaticDB>__6 = value;
		}));
		StaticDB.instance.InitDBs(genericStaticDB, localizedStaticDB);
		if (genericStaticDB != null)
		{
			genericStaticDB.Unload(true);
		}
		if (localizedStaticDB != null)
		{
			localizedStaticDB.Unload(true);
		}
		yield return base.StartCoroutine(this.FetchLatestVersion(this.m_assetServerURL + "update.txt"));
		AssetBundleManager.s_initialized = true;
		if (this.InitializedAction != null)
		{
			this.InitializedAction();
		}
		yield break;
	}

	private IEnumerator InternalInitAssetBundleManagerLocal()
	{
		AllPanels.instance.SetConnectingPanelStatus("Loading...");
		AllPanels.instance.connectingPanel.m_cancelButton.gameObject.SetActive(false);
		string filePath = string.Concat(new string[]
		{
			this.m_assetBundleDirectory,
			"/",
			this.m_platform,
			"/",
			this.m_platform
		});
		TextAsset manifestText = Resources.Load<TextAsset>(filePath);
		if (manifestText == null)
		{
			throw new Exception("Could not load manifest at path " + filePath);
		}
		this.BuildManifest(manifestText.text);
		string localStaticIdentifier = "staticdb_" + Main.instance.GetLocale().ToLower();
		yield return base.StartCoroutine(this.LoadAssetBundleLocal("icons", delegate(AssetBundle value)
		{
			this.<>f__this.m_iconsBundle = value;
		}));
		yield return base.StartCoroutine(this.LoadAssetBundleLocal("portraiticons", delegate(AssetBundle value)
		{
			this.<>f__this.m_portraitIconsBundle = value;
		}));
		AssetBundle genericStaticDB = null;
		AssetBundle localizedStaticDB = null;
		yield return base.StartCoroutine(this.LoadAssetBundleLocal("staticdb_gnrc", delegate(AssetBundle value)
		{
			this.<genericStaticDB>__3 = value;
		}));
		yield return base.StartCoroutine(this.LoadAssetBundleLocal(localStaticIdentifier, delegate(AssetBundle value)
		{
			this.<localizedStaticDB>__4 = value;
		}));
		StaticDB.instance.InitDBs(genericStaticDB, localizedStaticDB);
		if (genericStaticDB != null)
		{
			genericStaticDB.Unload(true);
		}
		if (localizedStaticDB != null)
		{
			localizedStaticDB.Unload(true);
		}
		AssetBundleManager.s_initialized = true;
		if (this.InitializedAction != null)
		{
			this.InitializedAction();
		}
		yield break;
	}

	private void BuildManifest(string manifestText)
	{
		int num = 0;
		int num2;
		do
		{
			num2 = manifestText.IndexOf('\n', num);
			if (num2 >= 0)
			{
				string lineText = manifestText.Substring(num, num2 - num + 1).Trim();
				this.ParseManifestLine(lineText);
				num = num2 + 1;
			}
		}
		while (num2 > 0);
	}

	private void ParseManifestLine(string lineText)
	{
		int startIndex = 0;
		int num;
		do
		{
			string text = "Name: ";
			num = lineText.IndexOf(text, startIndex);
			if (num >= 0)
			{
				int num2 = num + text.Length;
				string text2 = lineText.Substring(num2, lineText.Length - num2).Trim();
				string key = text2.Substring(0, text2.Length - 33);
				this.m_manifest.Add(key, text2);
			}
			startIndex = num + 1;
		}
		while (num > 0);
	}

	private string GetBundleFileName(string fileIdentifier)
	{
		string result;
		if (this.m_manifest.TryGetValue(fileIdentifier, out result))
		{
			return result;
		}
		return null;
	}

	public IEnumerator LoadAssetBundle(string fileIdentifier, Action<AssetBundle> resultCallback)
	{
		string fileName = this.GetBundleFileName(fileIdentifier);
		if (fileName == null)
		{
			SecurePlayerPrefs.DeleteKey("locale");
			throw new Exception("LoadAssetBundle: Error, file identifier " + fileIdentifier + " is unknown.");
		}
		while (!Caching.ready)
		{
			yield return null;
		}
		string url = this.m_assetServerURL + fileName;
		if (!Caching.IsVersionCached(url, 0))
		{
			Debug.Log("File " + fileIdentifier + " not cached. Will now load new file " + fileName);
			if (!AllPanels.instance.IsShowingDownloadingPanel())
			{
				AllPanels.instance.ShowDownloadingPanel(true);
			}
		}
		WWW download = WWW.LoadFromCacheOrDownload(url, 0);
		this.m_currentWWW = download;
		yield return download;
		if (!string.IsNullOrEmpty(download.error))
		{
			Caching.CleanCache();
			throw new Exception("LoadAssetBundle: Error: " + download.error);
		}
		if (download.assetBundle == null)
		{
			Caching.CleanCache();
			throw new Exception("LoadAssetBundle: null bundle: " + fileName);
		}
		resultCallback(download.assetBundle);
		yield break;
	}

	public IEnumerator LoadAssetBundleLocal(string fileIdentifier, Action<AssetBundle> resultCallback)
	{
		string fileName = this.GetBundleFileName(fileIdentifier);
		string filePath = string.Concat(new string[]
		{
			this.m_assetBundleDirectory,
			"/",
			this.m_platform,
			"/",
			fileName
		});
		if (fileName == null)
		{
			SecurePlayerPrefs.DeleteKey("locale");
			throw new Exception("LoadAssetBundle: Error, file identifier " + fileIdentifier + " is unknown.");
		}
		ResourceRequest resourceRequest = Resources.LoadAsync<TextAsset>(filePath);
		while (!resourceRequest.isDone)
		{
			yield return 0;
		}
		TextAsset bundleText = resourceRequest.asset as TextAsset;
		if (bundleText == null)
		{
			throw new Exception("Unable to load asset bundle " + filePath);
		}
		AssetBundle bundle = AssetBundle.LoadFromMemory(bundleText.bytes);
		yield return bundle;
		resultCallback(bundle);
		yield break;
	}

	public static AssetBundle portraitIcons
	{
		get
		{
			return AssetBundleManager.instance.m_portraitIconsBundle;
		}
	}

	public static AssetBundle Icons
	{
		get
		{
			return AssetBundleManager.instance.m_iconsBundle;
		}
	}

	public float GetDownloadProgress()
	{
		float num = this.m_priorProgress;
		if (this.m_currentWWW != null && Time.timeSinceLevelLoad > this.m_progressStartTime + 1f)
		{
			num += this.m_currentWWW.progress * this.m_progressMultiplier;
		}
		return num;
	}

	public bool IsDevAssetBundles()
	{
		return false;
	}

	public IEnumerator FetchLatestVersion(string url)
	{
		this.LatestVersion = 0;
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
		if (versionText != null)
		{
			char[] separator = new char[]
			{
				'\r',
				'\n'
			};
			string[] array = versionText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			this.LatestVersion = Convert.ToInt32(array[0]);
			this.ForceUpgrade = Convert.ToBoolean(array[1]);
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
		return false;
	}

	public void UpdateVersion()
	{
		string text = this.m_assetServerIpAddress;
		if (Login.instance.GetBnPortal() == "cn")
		{
			text = this.m_assetServerIpAddress_CN;
		}
		string text2 = string.Concat(new string[]
		{
			"http://",
			text,
			"/falcon/d",
			string.Format("{0:D5}", BuildNum.DataBuildNum),
			"/",
			this.m_assetBundleDirectory,
			"/"
		});
		text2 = text2 + this.m_platform + "/update.txt";
		this.LatestVersion = 0;
		this.ForceUpgrade = false;
		string versionText = null;
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		using (WWW www = new WWW(text2))
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

	private const int HASH_LENGTH = 32;

	private const string m_versionFile = "update.txt";

	private static AssetBundleManager s_instance;

	private static bool s_initialized;

	private string m_assetServerIpAddress = "blzddist2-a.akamaihd.net";

	private string m_assetServerIpAddress_CN = "client02.pdl.wow.battlenet.com.cn";

	private AssetBundle m_portraitIconsBundle;

	private AssetBundle m_iconsBundle;

	public string m_devAssetServerURL;

	private string m_assetServerURL;

	private Dictionary<string, string> m_manifest;

	public Action InitializedAction;

	private WWW m_currentWWW;

	private float m_priorProgress;

	private float m_progressMultiplier;

	private float m_progressStartTime;

	private string m_assetBundleDirectory = "ab";

	private string m_platform = "a";
}
