using System;
using UnityEngine;

public class FollowerModelSpinningBase : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnBeginDrag()
	{
		this.inititalYRotation = base.transform.localEulerAngles.y;
		if (Input.touchCount > 0)
		{
			this.initialTouchX = Input.GetTouch(0).position.x;
		}
		else
		{
			this.initialTouchX = Input.mousePosition.x;
		}
	}

	public void OnDrag()
	{
		float x;
		if (Input.touchCount > 0)
		{
			x = Input.GetTouch(0).position.x;
		}
		else
		{
			x = Input.mousePosition.x;
		}
		float num = (this.initialTouchX - x) / (float)Screen.width;
		num *= 2f;
		base.transform.localRotation = Quaternion.identity;
		base.transform.Rotate(0f, this.inititalYRotation + num * 360f, 0f, 1);
	}

	public float inititalYRotation;

	private float initialTouchX;
}
