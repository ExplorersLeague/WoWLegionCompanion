using System;
using UnityEngine;

public class LoadingSpinner : MonoBehaviour
{
	private void Update()
	{
		foreach (GameObject gameObject in this.m_objectsToSpin)
		{
			gameObject.transform.Rotate(0f, 0f, this.m_spinSpeed * Time.deltaTime);
		}
	}

	public GameObject[] m_objectsToSpin;

	public float m_spinSpeed;
}
