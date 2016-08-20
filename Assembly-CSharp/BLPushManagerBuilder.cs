using System;
using UnityEngine;

public class BLPushManagerBuilder : ScriptableObject
{
	public string region;

	public string locale;

	public string applicationName;

	public string authRegion;

	public string authToken;

	public string appAccountID;

	public bool shouldRegisterwithBPNS;

	public bool isDebug;

	public string senderId;

	public DidReceiveRegistrationTokenDelegate didReceiveRegistrationTokenDelegate;

	public DidReceiveDeeplinkURLDelegate didReceiveDeeplinkURLDelegate;

	public DidFailToRegisterForRemoteNotificationsWithErrorDelegate didFailToRegisterForRemoteNotificationsWithErrorDelegate;

	public DidFailToLogoutDelegate didFailToLogoutDelegate;
}
