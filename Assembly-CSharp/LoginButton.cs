using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
	private void Start()
	{
		this.m_loginButtonNameText.font = GeneralHelpers.LoadStandardFont();
	}

	private void Update()
	{
	}

	public void LogIn()
	{
		SecurePlayerPrefs.SetString("WebToken", this.m_token, Main.uniqueIdentifier);
		Login.instance.StartCachedLogin(false, false);
	}

	public void DeleteMe()
	{
		for (int i = 0; i < 10; i++)
		{
			string @string = SecurePlayerPrefs.GetString("DevAccount" + i, Main.uniqueIdentifier);
			string string2 = SecurePlayerPrefs.GetString("DevToken" + i, Main.uniqueIdentifier);
			if (@string != null && @string == this.m_loginButtonNameText.text && (string2 != null || string2 == this.m_token))
			{
				SecurePlayerPrefs.DeleteKey("DevAccount" + i);
				SecurePlayerPrefs.DeleteKey("DevToken" + i);
				break;
			}
		}
		Object.DestroyImmediate(base.gameObject);
	}

	public void PlayClickSound()
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
	}

	public Text m_loginButtonNameText;

	public string m_mobileServerAddress;

	public int m_mobileServerPort;

	public string m_bnetAccount;

	public string m_characterID;

	public ulong m_virtualRealmAddress;

	public string m_wowAccount;

	public string m_token;
}
