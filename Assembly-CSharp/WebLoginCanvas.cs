using System;
using UnityEngine;

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
			Login.instance.GetLoginState(),
			" -------------------------------"
		}));
		if (pageState.Contains("STATE_AUTHENTICATOR"))
		{
			Debug.Log("WebViewDidFinishLoad: no action for authenticator state.");
		}
		else if (Login.instance.IsWebAuthState())
		{
			Login.instance.ShowWebAuthView();
		}
		else
		{
			Login.instance.CancelWebAuth();
			AllPanels.instance.HideWebAuthPanel();
			Debug.Log("WebViewDidFinishLoad: Did not show web auth view because not in web auth login state." + Login.instance.GetLoginState());
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
