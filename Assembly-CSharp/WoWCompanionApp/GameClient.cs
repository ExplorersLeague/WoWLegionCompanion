using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class GameClient : MonoBehaviour
	{
		private void Start()
		{
			if (BLPushManager.instance == null)
			{
				Object.Instantiate<GameObject>(this.pushManager);
			}
			GameClient.instance = this;
		}

		public void RegisterPushManager(string token, string locale)
		{
			BLPushManagerBuilder blpushManagerBuilder = ScriptableObject.CreateInstance<BLPushManagerBuilder>();
			if (Login.m_portal.ToLower() == "wow-dev")
			{
				blpushManagerBuilder.isDebug = true;
				blpushManagerBuilder.applicationName = "test.wowcompanion";
			}
			else
			{
				blpushManagerBuilder.isDebug = false;
				blpushManagerBuilder.applicationName = "wowcompanion";
			}
			blpushManagerBuilder.shouldRegisterwithBPNS = true;
			blpushManagerBuilder.region = "US";
			blpushManagerBuilder.locale = locale;
			blpushManagerBuilder.authToken = token;
			blpushManagerBuilder.authRegion = "US";
			blpushManagerBuilder.appAccountID = string.Empty;
			blpushManagerBuilder.senderId = "952133414280";
			blpushManagerBuilder.didReceiveRegistrationTokenDelegate = new DidReceiveRegistrationTokenDelegate(this.DidReceiveRegistrationTokenHandler);
			blpushManagerBuilder.didReceiveDeeplinkURLDelegate = new DidReceiveDeeplinkURLDelegate(this.DidReceiveDeeplinkURLDelegateHandler);
			BLPushManager.instance.InitWithBuilder(blpushManagerBuilder);
			BLPushManager.instance.RegisterForPushNotifications();
		}

		public void DidReceiveRegistrationTokenHandler(string deviceToken)
		{
			Debug.Log("DidReceiveRegistrationTokenHandler: device token " + deviceToken);
		}

		public void DidReceiveDeeplinkURLDelegateHandler(string url)
		{
			Debug.Log("DidReceiveDeeplinkURLDelegateHandler: url " + url);
		}

		public GameObject pushManager;

		public static GameClient instance;
	}
}
