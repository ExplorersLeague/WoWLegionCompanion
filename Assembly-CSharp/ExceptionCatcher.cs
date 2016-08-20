using System;
using System.IO;
using UnityEngine;

public class ExceptionCatcher : MonoBehaviour
{
	private void Awake()
	{
	}

	private void HandleLogMessage(string condition, string stackTrace, LogType type)
	{
		if (type == 4)
		{
			this.exceptionPanel.gameObject.SetActive(true);
			Debug.Log(condition + "\n" + stackTrace);
			FileStream fileStream = new FileStream(Application.persistentDataPath + "/exceptions.log", FileMode.Append);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.WriteLine(condition + "\n" + stackTrace + "\n\n");
			streamWriter.Close();
			fileStream.Close();
			this.exceptionPanel.m_exceptionText.text = condition + "\n" + stackTrace;
		}
	}

	public void ShowExceptionLog()
	{
		this.exceptionPanel.gameObject.SetActive(true);
		FileStream fileStream = new FileStream(Application.persistentDataPath + "/exceptions.log", FileMode.OpenOrCreate);
		StreamReader streamReader = new StreamReader(fileStream);
		string exceptionText = streamReader.ReadToEnd();
		streamReader.Close();
		fileStream.Close();
		this.exceptionPanel.SetExceptionText(exceptionText);
	}

	public void ClearLog()
	{
		FileStream fileStream = new FileStream(Application.persistentDataPath + "/exceptions.log", FileMode.Create);
		fileStream.Close();
		this.exceptionPanel.SetExceptionText(string.Empty);
	}

	public ExceptionPanel exceptionPanel;
}
