using System;
using UnityEngine;

public class ResetScaleOnEnable : MonoBehaviour
{
	private void OnEnable()
	{
		base.transform.localScale = Vector3.one;
	}
}
