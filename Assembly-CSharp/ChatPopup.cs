using System;
using UnityEngine;
using UnityEngine.UI;

public class ChatPopup : MonoBehaviour
{
	private void Start()
	{
		TouchScreenKeyboard.hideInput = true;
	}

	private void Update()
	{
	}

	public void OnSendText()
	{
		if (this.textToSend.text.Length == 0)
		{
			return;
		}
		Main.instance.SendGuildChat(this.textToSend.text);
		this.textToSend.text = string.Empty;
		this.textToSend.Select();
		this.textToSend.ActivateInputField();
	}

	public void OnReceiveText(string sender, string text)
	{
		Text text2 = this.conversationText;
		string text3 = text2.text;
		text2.text = string.Concat(new string[]
		{
			text3,
			"[",
			sender,
			"]: ",
			text,
			"\n"
		});
	}

	public Text conversationText;

	public InputField textToSend;
}
