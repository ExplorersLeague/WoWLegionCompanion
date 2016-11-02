using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using UnityEngine;

public class ExceptionCatcher : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
		this.RegisterExceptionReporter();
	}

	private void ShowCurrentException(string condition, string stackTrace, LogType type)
	{
		if (type == 4)
		{
			this.exceptionPanel.gameObject.SetActive(true);
			Debug.Log(condition + "\n" + stackTrace);
			this.exceptionPanel.m_exceptionText.text = condition + "\n" + stackTrace;
		}
	}

	private void OnEnable()
	{
		this.RegisterExceptionReporter();
	}

	private void OnDisable()
	{
		this.UnregisterExceptionReporter();
	}

	private void OnApplicationQuit()
	{
		this.UnregisterExceptionReporter();
	}

	public void RegisterExceptionReporter()
	{
		Application.logMessageReceived += new Application.LogCallback(this.ExceptionReporterCallback);
	}

	public void UnregisterExceptionReporter()
	{
		Application.logMessageReceived -= new Application.LogCallback(this.ExceptionReporterCallback);
	}

	private void ExceptionReporterCallback(string message, string stackTrace, LogType logType)
	{
		if (logType == 4)
		{
			string text = ExceptionCatcher.CreateHash(message + stackTrace);
			if (!ExceptionCatcher.AlreadySent(text))
			{
				base.StartCoroutine(this.SendExceptionReport(message, stackTrace, text));
				ExceptionCatcher.sentReports.Add(text);
				return;
			}
		}
	}

	private IEnumerator SendExceptionReport(string message, string stackTrace, string hash)
	{
		string report = ExceptionCatcher.BuildMarkup(message, stackTrace, hash);
		byte[] bytes = Encoding.UTF8.GetBytes(report);
		WWWForm form = new WWWForm();
		form.AddBinaryData("file", bytes, "ReportedIssue.xml", "application/octet-stream");
		using (WWW www = new WWW(this.SubmitURL, form))
		{
			yield return www;
		}
		Login.instance.BnQuit();
		Application.Quit();
		yield break;
	}

	private static string BuildMarkup(string title, string stackTrace, string hashBlock)
	{
		string text = ExceptionCatcher.CreateEscapedSGML(stackTrace);
		return string.Concat(new object[]
		{
			"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ReportedIssue xmlns=\"http://schemas.datacontract.org/2004/07/Inspector.Models\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\n\t<Summary>",
			title,
			"</Summary>\n\t<Assertion>",
			text,
			"</Assertion>\n\t<HashBlock>",
			hashBlock,
			"</HashBlock>\n\t<BuildNumber>",
			BuildNum.CodeBuildNum,
			"</BuildNumber>\n\t<Module>WoW Legion Companion</Module>\n\t<EnteredBy>0</EnteredBy>\n\t<IssueType>Exception</IssueType>\n\t<ProjectId>",
			292,
			"</ProjectId>\n\t<Metadata><NameValuePairs>\n\t\t<NameValuePair><Name>Build</Name><Value>",
			BuildNum.CodeBuildNum,
			"</Value></NameValuePair>\n\t\t<NameValuePair><Name>OS.Platform</Name><Value>",
			Application.platform,
			"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Unity.Version</Name><Value>",
			Application.unityVersion,
			"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Unity.Genuine</Name><Value>",
			Application.genuine,
			"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Locale</Name><Value>",
			Main.instance.GetLocale(),
			"</Value></NameValuePair>\n\t</NameValuePairs></Metadata>\n</ReportedIssue>\n"
		});
	}

	private static string CreateHash(string blob)
	{
		MD5 md5Hash = MD5.Create();
		return ExceptionCatcher.GetMd5Hash(md5Hash, blob);
	}

	private static string CreateEscapedSGML(string blob)
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement xmlElement = xmlDocument.CreateElement("root");
		xmlElement.InnerText = blob;
		return xmlElement.InnerXml;
	}

	private IPAddress ipAddress
	{
		get
		{
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress ipaddress in hostEntry.AddressList)
				{
					if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
					{
						return ipaddress;
					}
				}
			}
			catch (SocketException)
			{
			}
			catch (ArgumentException)
			{
			}
			return this.unknownAddress;
		}
	}

	private static string localTime
	{
		get
		{
			return DateTime.Now.ToString("F", CultureInfo.CreateSpecificCulture("en-US"));
		}
	}

	private string SubmitURL
	{
		get
		{
			return "http://iir.blizzard.com:3724/submit/" + 292;
		}
	}

	private static bool AlreadySent(string hash)
	{
		return ExceptionCatcher.sentReports.Contains(hash);
	}

	private static string GetMd5Hash(MD5 md5Hash, string input)
	{
		byte[] array = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
	{
		string md5Hash2 = ExceptionCatcher.GetMd5Hash(md5Hash, input);
		StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
		return ordinalIgnoreCase.Compare(md5Hash2, hash) == 0;
	}

	private const int WoWCompanionProjectID = 292;

	public ExceptionPanel exceptionPanel;

	private static readonly HashSet<string> sentReports = new HashSet<string>();

	private IPAddress unknownAddress = new IPAddress(new byte[4]);
}
