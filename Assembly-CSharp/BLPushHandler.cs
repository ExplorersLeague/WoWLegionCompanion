using System;
using UnityEngine;

public class BLPushHandler : MonoBehaviour
{
	public void DidReceiveRegistrationToken(string deviceToken)
	{
		Debug.Log("unity " + deviceToken);
		BLPushHandler.builder.didReceiveRegistrationTokenDelegate(deviceToken);
	}

	public void DidReceiveDeeplinkURL(string url)
	{
		Debug.Log("unity " + url);
		BLPushHandler.builder.didReceiveDeeplinkURLDelegate(url);
	}

	public void DidFailToRegisterForRemoteNotificationsWithError(string errorDescription)
	{
		Debug.Log(errorDescription);
		BLPushHandler.builder.didFailToRegisterForRemoteNotificationsWithErrorDelegate(errorDescription);
	}

	public void DidFailToLogout(string errorDescription)
	{
		Debug.Log(errorDescription);
		BLPushHandler.builder.didFailToLogoutDelegate(errorDescription);
	}

	public static BLPushManagerBuilder builder;
}
