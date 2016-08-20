using System;
using UnityEngine;
using UnityEngine.UI;

public class ExceptionPanel : MonoBehaviour
{
	public void OnDismiss()
	{
		base.gameObject.SetActive(false);
	}

	public void SetExceptionText(string text)
	{
		this.m_exceptionText.text = text;
	}

	public Text m_exceptionText;
}
