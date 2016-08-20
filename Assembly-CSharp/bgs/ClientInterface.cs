using System;

namespace bgs
{
	public interface ClientInterface
	{
		string GetVersion();

		bool IsVersionInt();

		string GetUserAgent();

		string GetBasePersistentDataPath();

		string GetTemporaryCachePath();

		bool GetDisableConnectionMetering();

		constants.MobileEnv GetMobileEnvironment();

		string GetAuroraVersionName();

		string GetLocaleName();

		string GetPlatformName();

		constants.RuntimeEnvironment GetRuntimeEnvironment();

		IUrlDownloader GetUrlDownloader();
	}
}
