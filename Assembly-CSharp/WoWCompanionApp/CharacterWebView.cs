using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;

namespace WoWCompanionApp
{
	public class CharacterWebView : MonoBehaviour
	{
		private IEnumerator Start()
		{
			this.characterViewPanel = base.GetComponentInParent<CharacterViewPanel>();
			string locale = MobileDeviceLocale.GetBestGuessForLocale();
			if (Singleton<Login>.Instance.GetBnPortal() != "test")
			{
				this.Url = new StringBuilder().Append("https://worldofwarcraft.com/").Append(locale.Substring(0, 2).ToLower()).Append("-").Append(locale.Substring(2, 2).ToLower()).Append("/character/").Append(Singleton<Login>.Instance.GetBnPortal().ToLower()).Append("/").Append(Singleton<Login>.Instance.GetRealmName(Singleton<CharacterData>.Instance.VirtualRealmAddress).Replace(" ", "-").Replace("'", string.Empty).ToLower()).Append("/").Append(Singleton<CharacterData>.Instance.CharacterName.ToLower()).Append("/equipment").ToString();
			}
			else
			{
				this.Url = "https://worldofwarcraft.com/en-us/character/us/proudmoore/cahoots/equipment";
			}
			this.Url = Uri.EscapeUriString(this.Url);
			Debug.Log("WebView url: " + this.Url);
			this.webViewObject = base.gameObject.AddComponent<WebViewObject>();
			this.webViewObject.Init(delegate(string msg)
			{
				Debug.Log(string.Format("CallFromJS[{0}]", msg));
			}, false, string.Empty, delegate(string msg)
			{
				Debug.Log(string.Format("CallOnError[{0}]", msg));
				this.$this.characterViewPanel.OnWebViewLoaded(false);
			}, delegate(string msg)
			{
				Debug.Log(string.Format("CallOnHttpError[{0}]", msg));
				this.$this.characterViewPanel.OnWebViewLoaded(false);
			}, delegate(string msg)
			{
				Debug.Log(string.Format("CallOnLoaded[{0}]", msg));
				this.$this.characterViewPanel.OnWebViewLoaded(true);
				this.$this.webViewObject.EvaluateJS("Unity.call('ua=' + navigator.userAgent)");
			}, true, null);
			this.webViewObject.SetMargins(0, base.GetComponentInParent<CharacterViewPanel>().TopMargin, 0, 0);
			this.webViewObject.SetVisibility(false);
			if (this.Url.StartsWith("http"))
			{
				this.webViewObject.LoadURL(this.Url);
			}
			else
			{
				string[] exts = new string[]
				{
					".jpg",
					".js",
					".html"
				};
				foreach (string ext in exts)
				{
					string url = this.Url.Replace(".html", ext);
					string src = Path.Combine(Application.streamingAssetsPath, url);
					string dst = Path.Combine(Application.persistentDataPath, url);
					byte[] result = null;
					if (src.Contains("://"))
					{
						WWW www = new WWW(src);
						yield return www;
						result = www.bytes;
					}
					else
					{
						result = File.ReadAllBytes(src);
					}
					File.WriteAllBytes(dst, result);
					if (ext == ".html")
					{
						this.webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
						break;
					}
				}
			}
			yield break;
		}

		public void SetWebViewVisible(bool visible)
		{
			this.webViewObject.SetVisibility(visible);
		}

		private string Url = "https://worldofwarcraft.com/en-us/404";

		private WebViewObject webViewObject;

		private CharacterViewPanel characterViewPanel;
	}
}
