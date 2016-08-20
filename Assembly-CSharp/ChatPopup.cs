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
		if (TouchScreenKeyboard.visible)
		{
			Vector3 vector;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(this.textToSend.gameObject.GetComponent<RectTransform>(), TouchScreenKeyboard.area.max, null, ref vector);
			base.transform.position = new Vector3(base.transform.position.x, vector.y, base.transform.position.z);
		}
		else
		{
			base.transform.localPosition = Vector3.zero;
		}
		TouchScreenKeyboard.hideInput = true;
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
