using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class WebLoginCanvas : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
		}

		public void WebViewDidFinishLoad(string pageState)
		{
			Debug.Log(string.Concat(new object[]
			{
				"--------------------------- WebViewDidFinishLoad: ",
				pageState,
				", login state: ",
				Singleton<Login>.instance.GetLoginState(),
				" -------------------------------"
			}));
			if (pageState.Contains("STATE_AUTHENTICATOR"))
			{
				Debug.Log("WebViewDidFinishLoad: no action for authenticator state.");
			}
			else if (Singleton<Login>.instance.IsWebAuthState())
			{
				Singleton<Login>.instance.ShowWebAuthView();
			}
			else
			{
				Singleton<Login>.instance.CancelWebAuth();
				Debug.Log("WebViewDidFinishLoad: Did not show web auth view because not in web auth login state." + Singleton<Login>.instance.GetLoginState());
			}
		}

		public void WebViewBackButtonPressed(string empty)
		{
			Debug.Log("--------------------------- WebViewBackButtonPressed: " + empty + " -------------------------------");
		}

		public void WebViewReceivedToken(string token)
		{
			Debug.Log("--------------------------- WebViewReceivedToken: " + token + " -------------------------------");
		}
	}
}
