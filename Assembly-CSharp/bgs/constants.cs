using System;
using System.ComponentModel;

namespace bgs
{
	public class constants
	{
		public const ushort RouteToAnyUtil = 0;

		public const float ResubsribeAttemptDelaySeconds = 120f;

		public enum BNetState
		{
			BATTLE_NET_UNKNOWN,
			BATTLE_NET_LOGGING_IN,
			BATTLE_NET_TIMEOUT,
			BATTLE_NET_LOGIN_FAILED,
			BATTLE_NET_LOGGED_IN
		}

		public enum BnetRegion
		{
			REGION_UNINITIALIZED = -1,
			REGION_UNKNOWN,
			REGION_US,
			REGION_EU,
			REGION_KR,
			REGION_TW,
			REGION_CN,
			REGION_LIVE_VERIFICATION = 40,
			REGION_PTR_LOC,
			REGION_MSCHWEITZER_BN11 = 52,
			REGION_MSCHWEITZER_BN12,
			REGION_DEV = 60,
			REGION_PTR = 98
		}

		public enum MobileEnv
		{
			[Description("Development")]
			DEVELOPMENT,
			[Description("Production")]
			PRODUCTION
		}

		public enum RuntimeEnvironment
		{
			Mono,
			MSDotNet
		}
	}
}
