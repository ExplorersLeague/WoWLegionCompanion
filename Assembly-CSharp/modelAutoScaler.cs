using System;
using UnityEngine;

public class modelAutoScaler : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.isPortrait)
		{
			float num = 0.75f / this.mainCamera.aspect;
			base.gameObject.transform.localScale = new Vector3(num, num, num);
		}
		else
		{
			float num2 = 1.33333337f / this.mainCamera.aspect;
			base.gameObject.transform.localScale = new Vector3(num2, num2, num2);
		}
	}

	public Camera mainCamera;

	public bool isPortrait;
}
