using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderCustomHelper : MonoBehaviour
{
	private void Start()
	{
		Slider component = base.GetComponent<Slider>();
		this.m_minText.text = string.Empty + component.minValue;
		this.m_maxText.text = string.Empty + component.maxValue;
		this.OnValueChanged(component.value);
	}

	public void OnValueChanged(float val)
	{
		this.m_titleText.text = string.Format("{0} ({1:F2})", this.m_baseTitleString, val);
	}

	public string m_baseTitleString;

	public Text m_titleText;

	public Text m_minText;

	public Text m_maxText;

	private Slider m_slider;
}
