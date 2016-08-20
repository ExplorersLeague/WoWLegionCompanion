using System;
using UnityEngine;

public class ScaleToFill : MonoBehaviour
{
	private void Start()
	{
		RectTransform component = base.GetComponent<RectTransform>();
		this.nativeHeight = component.rect.height;
	}

	private void Update()
	{
		RectTransform component = base.transform.parent.gameObject.GetComponent<RectTransform>();
		float num = component.rect.height / this.nativeHeight;
		base.transform.localScale = new Vector3(num, num, num);
	}

	private float nativeHeight;
}
