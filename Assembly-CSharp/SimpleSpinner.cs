using System;
using UnityEngine;

public class SimpleSpinner : MonoBehaviour
{
	private void Update()
	{
		base.transform.Rotate(0f, 0f, this.m_spinSpeed * Time.deltaTime);
	}

	public float m_spinSpeed;
}
