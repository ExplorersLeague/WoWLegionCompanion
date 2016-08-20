using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class BLPushManager : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void SetApplicationName(string applicationName);

	[DllImport("__Internal")]
	private static extern void InitializePushManagerBuilderUnity();

	[DllImport("__Internal")]
	private static extern void SetRegion(string region);

	[DllImport("__Internal")]
	private static extern void SetLocale(string locale);

	[DllImport("__Internal")]
	private static extern void SetAuthRegion(string authRegion);

	[DllImport("__Internal")]
	private static extern void SetAuthToken(string authToken);

	[DllImport("__Internal")]
	private static extern void SetAppAccountId(string appAccountId);

	[DllImport("__Internal")]
	private static extern void SetUnityPushEventHandler();

	[DllImport("__Internal")]
	private static extern void InitializePushManager();

	[DllImport("__Internal")]
	private static extern void RegisterForPushNotificationsIOS();

	[DllImport("__Internal")]
	private static extern void SetShouldRegisterWithBPNS(bool shouldRegisterWithBPNS);

	[DllImport("__Internal")]
	private static extern void SetIsDebug(bool isDebug);

	private void Awake()
	{
		if (BLPushManager.instance == null)
		{
			BLPushManager.instance = this;
		}
		else if (BLPushManager.instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		Object.DontDestroyOnLoad(base.gameObject);
		base.name = "BLPushManager";
	}

	public void InitWithBuilder(BLPushManagerBuilder builder)
	{
		BLPushHandler.builder = builder;
		if (Application.platform != 11)
		{
			if (Application.platform == 8)
			{
				BLPushManager.InitializePushManagerBuilderUnity();
				BLPushManager.SetApplicationName(builder.applicationName);
				BLPushManager.SetRegion(builder.region);
				BLPushManager.SetLocale(builder.locale);
				BLPushManager.SetAuthRegion(builder.authRegion);
				BLPushManager.SetAuthToken(builder.authToken);
				BLPushManager.SetAppAccountId(builder.appAccountID);
				BLPushManager.SetShouldRegisterWithBPNS(builder.shouldRegisterwithBPNS);
				BLPushManager.SetIsDebug(builder.isDebug);
				BLPushManager.SetUnityPushEventHandler();
				BLPushManager.InitializePushManager();
			}
		}
	}

	public void RegisterForPushNotificationsAndroid()
	{
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			this.currentContext = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		}
		using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.blizzard.pushlibrary.BlizzardPush"))
		{
			if (androidJavaClass2 != null)
			{
				BLPushManagerBuilder builder = BLPushHandler.builder;
				androidJavaClass2.CallStatic("initialize", new object[]
				{
					this.currentContext,
					builder.applicationName,
					builder.senderId,
					builder.region,
					builder.locale,
					builder.authRegion,
					builder.authToken,
					builder.appAccountID
				});
			}
		}
	}

	public void RegisterForPushNotifications()
	{
		if (Application.platform == 11)
		{
			this.RegisterForPushNotificationsAndroid();
		}
		else if (Application.platform == 8)
		{
			BLPushManager.RegisterForPushNotificationsIOS();
		}
	}

	public static BLPushManager instance;

	public BLPushHandler pushHandler;

	private AndroidJavaObject currentContext;
}
