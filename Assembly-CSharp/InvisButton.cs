using System;
using UnityEngine;
using UnityEngine.UI;

public class InvisButton : MonoBehaviour
{
	public void OnClick()
	{
		this.buttonText.text = "Down";
	}

	public void OnRelease()
	{
		this.buttonText.text = "Up";
	}

	public Text buttonText;
}
