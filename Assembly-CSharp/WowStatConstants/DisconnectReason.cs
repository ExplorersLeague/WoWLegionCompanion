using System;

namespace WowStatConstants
{
	public enum DisconnectReason
	{
		None,
		ConnectionLost,
		TimeoutContactingServer,
		AppVersionOld,
		AppVersionNew,
		Generic,
		CharacterInWorld,
		CantEnterWorld,
		LoginDisabled,
		TrialNotAllowed,
		ConsumptionTimeNotAllowed,
		PingTimeout
	}
}
