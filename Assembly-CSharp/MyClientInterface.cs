using System;
using System.Collections.Generic;
using bgs;
using UnityEngine;

internal class MyClientInterface : ClientInterface
{
	private void SetCachePath(string cachePath)
	{
		this.m_temporaryCachePath = cachePath;
	}

	public string GetVersion()
	{
		return string.Empty;
	}

	public bool IsVersionInt()
	{
		return true;
	}

	public string GetBasePersistentDataPath()
	{
		return this.m_temporaryCachePath;
	}

	public constants.RuntimeEnvironment GetRuntimeEnvironment()
	{
		return constants.RuntimeEnvironment.Mono;
	}

	public string GetUserAgent()
	{
		return null;
	}

	public string GetTemporaryCachePath()
	{
		return this.m_temporaryCachePath;
	}

	public bool GetDisableConnectionMetering()
	{
		return true;
	}

	public constants.MobileEnv GetMobileEnvironment()
	{
		string text = Login.m_portal.ToLower();
		if (text != null)
		{
			if (MyClientInterface.<>f__switch$map5 == null)
			{
				MyClientInterface.<>f__switch$map5 = new Dictionary<string, int>(7)
				{
					{
						"us",
						0
					},
					{
						"eu",
						0
					},
					{
						"kr",
						0
					},
					{
						"cn",
						0
					},
					{
						"tw",
						0
					},
					{
						"beta",
						0
					},
					{
						"test",
						0
					}
				};
			}
			int num;
			if (MyClientInterface.<>f__switch$map5.TryGetValue(text, out num))
			{
				if (num == 0)
				{
					return constants.MobileEnv.PRODUCTION;
				}
			}
		}
		return constants.MobileEnv.DEVELOPMENT;
	}

	public string GetAuroraVersionName()
	{
		return "0";
	}

	public string GetLocaleName()
	{
		return Main.instance.GetLocale();
	}

	public string GetPlatformName()
	{
		return "And";
	}

	public IUrlDownloader GetUrlDownloader()
	{
		return Login.instance.m_urlDownloader;
	}

	public string m_temporaryCachePath = Application.persistentDataPath;
}
